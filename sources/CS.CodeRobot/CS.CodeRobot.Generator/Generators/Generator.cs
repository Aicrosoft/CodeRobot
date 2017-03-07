using System;
using System.IO;
using CS.CodeRobot.SchemaEngine;
using CS.CodeRobot.TemplateEngine;
using CS.Extension;
using CS.Logging;
using CS.Utils;
using DotLiquid;

namespace CS.CodeRobot.Generators
{
    public class Generator
    {
        private static readonly ILog log = LogManager.GetLogger(typeof (Generator));

        private static readonly TemplateApp _templateApp = GeneratorFilter.TemplateApp;

        public static void Run(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(@"请输入项目模板的文件夹名称。");
                Console.ReadLine();
            }
            var pfName = args[0];
            var app = new App(pfName);
            var pi = app.ProjectInfo;
            GeneratorFilter.ProjectInfo = pi;
            var rootTemplateFolders = FileHelper.GetFullPath(pi.TemplatesRootDirectory);
            var subs = FileHelper.GetSubFolderNames(rootTemplateFolders); //子模板目录
            if (subs.Length == 0)
            {
                log.Warn("子模板不存在，无法生成项目。");
                return;
            }
            var result = _templateApp.Render($"{rootTemplateFolders}index.tpl", new { subs,pi });
            log.Info(result);

            //开始运行项目子模板
            foreach (var sub in subs)
            {
                result = _templateApp.Render($"{rootTemplateFolders}{sub}//index.tpl", new {  sub, pi });
                log.Info(result);
            }

            //foreach (var sub in subs)
            //{
            //    //Console.WriteLine(rootTemplateFolders);
            //    //var dbSchemaExplor = new DataSchemaExplor("Swap");
            //    //var dbSchema = dbSchemaExplor.ReadAll();
            //    //log.Debug(dbSchema.ToJsonByJc());
            //    var ai = new AssemblySetting(sub, pi);//assemblyInfo
            //    CreateAssemblyFile(pi, ai);
            //}

            Console.WriteLine(@"press any key to close.");
            Console.ReadKey();
        }
       
    }
}