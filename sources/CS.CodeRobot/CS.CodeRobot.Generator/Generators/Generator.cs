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

        private static readonly TemplateApp _templateApp = new TemplateApp();

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
            foreach (var setting in pi.DbConns)
            {
                
            }
            log.Warn(pfName);

            var rootTemplateFolders = FileHelper.GetFullPath(pi.TemplatesRootDirectory);
            var x = FileHelper.GetSubDictionary(rootTemplateFolders);

            Console.WriteLine(rootTemplateFolders);
            
            //var dbSchemaExplor = new DataSchemaExplor("Swap");
            //var dbSchema = dbSchemaExplor.ReadAll();
            //log.Debug(dbSchema.ToJsonByJc());
            var ai = new AssemblySetting("Model", pi);//assemblyInfo
            CreateAssemblyFile(pi, ai);



            Console.ReadLine();

        }



        /// <summary>
        /// 生成程序集信息
        /// </summary>
        /// <param name="pi"></param>
        /// <param name="ai"></param>
        public static void CreateAssemblyFile(ProjectMeta pi,AssemblySetting ai)
        {
            var fileName = $"{pi.TemplatesRootDirectory}AssemblyInfo.tpl";
            var result = _templateApp.Render(fileName,pi,ai );
            log.Debug(result);

            var asFile = $"{pi.GetOutputDirectory(ai.ModelLayer)}Properties\\AssemblyInfo.cs";
            FileHelper.Save(asFile, result);
            log.Info("Model 程序集信息写入完成");
        }
    }
}