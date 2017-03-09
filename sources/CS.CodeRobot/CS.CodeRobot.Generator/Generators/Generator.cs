using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using CS.CodeRobot.JnTemplateEngine;
using CS.CodeRobot.TemplateEngine;
using CS.Logging;
using CS.Utils;
using DatabaseSchemaReader.DataSchema;
using DotLiquid;

namespace CS.CodeRobot.Generators
{
    public class Generator
    {


        //public static void Run(string[] args)
        //{
        //    if (args.Length == 0)
        //    {
        //        Console.WriteLine(@"请输入项目模板的文件夹名称。");
        //        Console.ReadLine();
        //    }
        //    var pfName = args[0];
        //    var app = new App(pfName);
        //    var pi = app.Project;
        //    GeneratorFilter.ProjectInfo = pi;
        //    var rootTemplateFolders = FileHelper.GetFullPath(pi.TemplatesRootDirectory);
        //    var subs = FileHelper.GetSubFolderNames(rootTemplateFolders); //子模板目录
        //    if (subs.Length == 0)
        //    {
        //        log.Warn("子模板不存在，无法生成项目。");
        //        return;
        //    }
        //    var result = GeneratorFilter.TemplateApp.Render($"{rootTemplateFolders}index.tpl", new { subs, pi });
        //    log.Debug(result);

        //    //开始运行项目子模板
        //    foreach (var sub in subs)
        //    {
        //        result = GeneratorFilter.TemplateApp.Render($"{rootTemplateFolders}{sub}\\index.tpl", new { sub, pi });
        //        log.Debug(result);
        //    }

        //    //Console.WriteLine(@"press any key to close.");
        //    //Console.ReadKey();
        //}



        static Generator()
        {
            TemplateApp = new TemplateApp();
            CodeFiles = new List<CodeFile>();
        }

        const string NameSpaceURI = "http://schemas.microsoft.com/developer/msbuild/2003";
        private static readonly ILog log = LogManager.GetLogger(typeof(Generator));
        private static readonly List<CodeFile> CodeFiles;//所有的生成的实体类及其所在文件夹的信息

        /// <summary>
        /// 模板
        /// </summary>
        public static TemplateApp TemplateApp { get; }



        public static void RenderTable(string tplFile, DatabaseTable table, ModelMeta model, DbSetting dbSetting)
        {
            var pi = App.Instance.Project;
            var clsName = $"{table.Name}{model.Suffix}.cs";
            var clsAutoName = $"{table.Name}{model.Suffix}.Auto.cs";
            var codeFile = new CodeFile { Name = clsName, Sub = dbSetting.Name, AutoName = clsAutoName };
            var absTplFile = $"{pi.GetTemplateDirectory(model)}{tplFile}";
            var absTplAutoFile = $"{pi.GetTemplateDirectory(model)}{tplFile.Replace(".tpl", ".Auto.tpl")}";
            var clsAbsFile = $"{pi.GetOutputDirectory(model, dbSetting.Name)}{clsName}";
            var clsAbsAutoFile = $"{pi.GetOutputDirectory(model, dbSetting.Name)}{clsAutoName}";

            //自动代码
            var tpl = JnTemplateApp.CreateTemplate(absTplAutoFile);
            tpl.Set("pi",pi);
            tpl.Set("table", table);
            tpl.Set("model",model);
            tpl.Set("dbSetting",dbSetting);
            var clsAutoCode = tpl.Render();
            FileHelper.Save(clsAbsAutoFile,clsAutoCode,true);

            //手动代码
            tpl = JnTemplateApp.CreateTemplate(absTplFile);
            tpl.Set("pi", pi);
            tpl.Set("table", table);
            tpl.Set("model", model);
            tpl.Set("dbSetting", dbSetting);
            var clsCode = tpl.Render();
            FileHelper.Save(clsAbsFile, clsCode, true);


            CodeFiles.Add(codeFile);
        }


        /// <summary>
        /// 更新工程项目配置文件
        /// </summary>
        /// <param name="model"></param>
        public static void UpdateProject(ModelMeta model)
        {
            var app = App.Instance;
            var pi = app.Project;
            var xmlDoc = new XmlDocument();
            var projectXml = $"{pi.GetOutputDirectory(model)}{model.AssemblyName}.csproj";
            xmlDoc.Load(projectXml);
            var nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("ns", NameSpaceURI);
            xmlDoc.ChangeProjectInfo(model.AssemblyName, nsmgr);

            xmlDoc.ChangeAutoItems(nsmgr, CodeFiles);
            xmlDoc.Save(projectXml);
            log.Info($"{model.AssemblyName} 的project.csproj 工程文件更新完毕");
        }

        public static void InitCodeFiles()
        {
            CodeFiles.Clear();
            log.Debug("清除CodeFiles缓存信息完成");
        }

        /// <summary>
        /// 生成项目文件
        /// </summary>
        /// <param name="model"></param>
        public static void CreateProjetc(ModelMeta model)
        {
            var app = App.Instance;
            var pi = app.Project;
            //var xx = pi.GetTemplateDirectory(sub);
            var configFile = Directory.GetFiles($"{pi.GetTemplateDirectory(model)}").FirstOrDefault(x => FileHelper.GetExtension(x) == ".csproj");
            if (string.IsNullOrWhiteSpace(configFile))
            {
                log.Error($"{model.Name}项目下的project.csproj文件模板丢失，请更正后重新执行生成功作。");
                return;
            }
            var proFile = $"{pi.GetOutputDirectory(model)}{model.AssemblyName}.csproj";
            FileHelper.Copy(configFile, proFile);
            log.Debug($"{proFile} 项目文件生成成功");
        }


        /// <summary>
        /// 将所有的config文件拷至项目目录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static void CreateConfigs(ModelMeta model)
        {
            var app = App.Instance;
            var pi = app.Project;
            //var xx = pi.GetTemplateDirectory(sub);
            var configFiles = Directory.GetFiles($"{pi.GetTemplateDirectory(model)}").Where(x => FileHelper.GetExtension(x) == ".config").ToArray();
            if (configFiles.Length == 0)
            {
                log.Error("无config文件以生成");
                return;
            }
            FileHelper.Copy(configFiles, pi.GetOutputDirectory(model));
            log.Debug($"{model.Name} 下的 {string.Join(",", FileHelper.GetFileNames(configFiles))} 程序集的config生成成功");
            //return msg;
        }

        /// <summary>
        /// 生成程序集信息
        /// </summary>
        /// <param name="model"></param>
        public static void CreateAssembly(ModelMeta model)
        {
            var app = App.Instance;
            var fileName = app.Project.GetTemplateFullPath("AssemblyInfo.tpl");
            //log.Debug(result);
            var template = JnTemplateApp.CreateTemplate(fileName);
            template.Set("model", model);
            var result = template.Render();
            var asFile = $"{app.Project.GetOutputDirectory(model)}Properties\\AssemblyInfo.cs";
            FileHelper.Save(asFile, result);
            log.Debug($"{model} 程序集信息写入完成");
            //return msg;
        }



    }
}