using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using CS.CodeRobot.JnTemplateEngine;
using CS.Logging;
using CS.Utils;
using DatabaseSchemaReader.DataSchema;

namespace CS.CodeRobot.Generators
{
    public class Generator
    {

        static Generator()
        {
            CodeFiles = new List<CodeFile>();
        }

        const string NameSpaceURI = "http://schemas.microsoft.com/developer/msbuild/2003";
        private static readonly ILog log = LogManager.GetLogger(typeof(Generator));
        private static readonly List<CodeFile> CodeFiles;//所有的生成的实体类及其所在文件夹的信息


        public static void RenderDbContext(string tplFile, ModelMeta model, DbSetting dbSetting)
        {
            var codeFile = new CodeFile { Name = $"{dbSetting.DbContextName}.cs", Sub = dbSetting.Name, AutoName = $"{dbSetting.DbContextName}.Auto.cs" };
            RenderAutoItem(tplFile, model, dbSetting, codeFile);
        }

        public static void RenderTable(string tplFile, ModelMeta model, DbSetting dbSetting, DatabaseTable table)
        {
            var codeFile = new CodeFile { Name = $"{table.Name}{model.Suffix}.cs", Sub = dbSetting.Name, AutoName = $"{table.Name}{model.Suffix}.Auto.cs" };
            RenderAutoItem(tplFile, model, dbSetting, codeFile, table);
        }

        public static void Render(string tplFile, ModelMeta model)
        {
            var pi = App.Instance.Project;
            var codeFile = new CodeFile { Name = tplFile.Replace(".tpl", ".cs") };
            var absTplFile = $"{pi.GetTemplateDirectory(model)}{tplFile}";
            var clsAbsFile = $"{pi.GetOutputDirectory(model)}{codeFile.Name}";

            //手动代码
            var tpl = JnTemplateApp.CreateTemplate(absTplFile);
            tpl.Set("pi", pi);
            tpl.Set("model", model);
            var clsCode = tpl.Render();
            FileHelper.Save(clsAbsFile, clsCode, false);

            CodeFiles.Add(codeFile);
        }

        private static void RenderAutoItem(string tplFile, ModelMeta model, DbSetting dbSetting, CodeFile codeFile, DatabaseTable table = null)
        {
            var pi = App.Instance.Project;
            var absTplFile = $"{pi.GetTemplateDirectory(model)}{tplFile}";
            var absTplAutoFile = $"{pi.GetTemplateDirectory(model)}{tplFile.Replace(".tpl", ".Auto.tpl")}";
            var clsAbsFile = $"{pi.GetOutputDirectory(model, dbSetting.Name)}{codeFile.Name}";
            var clsAbsAutoFile = $"{pi.GetOutputDirectory(model, dbSetting.Name)}{codeFile.AutoName}";

            //自动代码
            var tpl = JnTemplateApp.CreateTemplate(absTplAutoFile);
            tpl.Set("pi", pi);
            tpl.Set("table", table);
            tpl.Set("model", model);
            tpl.Set("dbSetting", dbSetting);
            var clsAutoCode = tpl.Render();
            FileHelper.Save(clsAbsAutoFile, clsAutoCode, true);

            //手动代码
            tpl = JnTemplateApp.CreateTemplate(absTplFile);
            tpl.Set("pi", pi);
            tpl.Set("table", table);
            tpl.Set("model", model);
            tpl.Set("dbSetting", dbSetting);
            var clsCode = tpl.Render();
            FileHelper.Save(clsAbsFile, clsCode, false);

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