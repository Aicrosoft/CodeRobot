using System;
using System.IO;
using System.Linq;
using CS.CodeRobot.TemplateEngine;
using CS.Logging;
using CS.Utils;

namespace CS.CodeRobot.Generators
{
    public class Generator
    {
        private static readonly ILog log = LogManager.GetLogger(typeof (Generator));


        public static void Run(string[] args)
        {
            //if (args.Length == 0)
            //{
            //    Console.WriteLine(@"请输入项目模板的文件夹名称。");
            //    Console.ReadLine();
            //}
            //var pfName = args[0];
            //var app = new App(pfName);
            //var pi = app.Project;
            //GeneratorFilter.ProjectInfo = pi;
            //var rootTemplateFolders = FileHelper.GetFullPath(pi.TemplatesRootDirectory);
            //var subs = FileHelper.GetSubFolderNames(rootTemplateFolders); //子模板目录
            //if (subs.Length == 0)
            //{
            //    log.Warn("子模板不存在，无法生成项目。");
            //    return;
            //}
            //var result = GeneratorFilter.TemplateApp.Render($"{rootTemplateFolders}index.tpl", new { subs,pi });
            //log.Debug(result);

            ////开始运行项目子模板
            //foreach (var sub in subs)
            //{
            //    result = GeneratorFilter.TemplateApp.Render($"{rootTemplateFolders}{sub}\\index.tpl", new {  sub, pi });
            //    log.Debug(result);
            //}

            ////Console.WriteLine(@"press any key to close.");
            ////Console.ReadKey();
        }

        static Generator()
        {
            TemplateApp = new TemplateApp();
        }

        /// <summary>
        /// 模板
        /// </summary>
        public static TemplateApp TemplateApp { get; }

        /// <summary>
        /// 生成项目文件
        /// </summary>
        /// <param name="model"></param>
        public static void CreateProjetc(ModelMeta model)
        {
            var app = App.Instance;
            var pi = app.Project;
            //var xx = pi.GetTemplateDirectory(sub);
            var configFile = Directory.GetFiles($"{pi.GetTemplateDirectory(model.Name)}").FirstOrDefault(x => FileHelper.GetExtension(x) == ".csproj");
            if (string.IsNullOrWhiteSpace(configFile))
            {
                log.Error($"{model.Name}项目下的project.csproj文件模板丢失，请更正后重新执行生成功作。");
                return;
            }
            var proFile = $"{pi.GetOutputDirectory(model.Name)}{model.AssemblyName}.csproj";
            FileHelper.Copy(configFile, proFile);
            var msg = $"{proFile} 项目文件生成成功";
            log.Info(msg);
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
            var configFiles = Directory.GetFiles($"{pi.GetTemplateDirectory(model.Name)}").Where(x => FileHelper.GetExtension(x) == ".config").ToArray();
            if (configFiles.Length == 0)
            {
                log.Info("无config文件以生成");
                return;
            }
            FileHelper.Copy(configFiles, pi.GetOutputDirectory(model.Name));
            var msg = $"{model.Name} 下的 {string.Join(",", FileHelper.GetFileNames(configFiles))} 程序集的config生成成功";
            log.Info(msg);
            //return msg;
        }

        /// <summary>
        /// 生成程序集信息
        /// </summary>
        /// <param name="model"></param>
        public static void CreateAssembly(ModelMeta model)
        {
            var app = App.Instance;
            var fileName = $"{app.Project.TemplatesRootDirectory}AssemblyInfo.tpl";
            var pi = app.Project;
            var ai = model;//app.Models.FirstOrDefault(x => x.Name == model);
            var result = TemplateApp.Render(fileName, new { pi, ai });
            //log.Debug(result);
            var asFile = $"{pi.GetOutputDirectory(ai.Name)}Properties\\AssemblyInfo.cs";
            FileHelper.Save(asFile, result);
            var msg = $"{model} 程序集信息写入完成";
            log.Info(msg);
            //return msg;
        }



    }
}