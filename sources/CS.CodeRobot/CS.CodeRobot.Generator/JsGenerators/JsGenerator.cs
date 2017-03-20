using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using CS.Extension;
using CS.Logging;
using CS.Utils;
using Microsoft.JScript;

namespace CS.CodeRobot.JsGenerators
{
    public class JsGenerator
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(JsGenerator));

        private static App _app;

        public static void Run(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(@"请输入项目模板的文件夹名称。");
                Console.ReadLine();
            }
            _app = new App(args[0]);

            var jsFile = $"{_app.RootTemplateFullPath}index.js";
            var jsObj = CreateJsInstance(jsFile);
            jsObj.main(_app);

            foreach (var model in _app.Models)
            {
                //if(model.Name != "DbProxy") continue;
                jsFile = $"{_app.RootTemplateFullPath}{model.Name}\\index.js";
                jsObj = CreateJsInstance(jsFile);
                jsObj.main(_app, model);
            }
        }



        #region CreateJsInstance   by JsCompiler 

        private static readonly JScriptCodeProvider JsCompiler = new JScriptCodeProvider();

        public static dynamic CreateJsInstance(string jsFilePath, string jsClassName = "JsProgram")
        {
            var opt = new CompilerParameters
            {
                GenerateExecutable = false,
                GenerateInMemory = true
            };
            //opt.ReferencedAssemblies.Add("System.dll");
            //Assembly.LoadFile(FileHelper.GetFullPath("~/CS.Utility.dll"));
            //Assembly.LoadFile(FileHelper.GetFullPath("~/CS.CodeRobot.Generator.dll"));
            opt.ReferencedAssemblies.Add(typeof(AppHelper).Assembly.Location);
            opt.ReferencedAssemblies.Add(typeof(JsGenerator).Assembly.Location); //非GAC必须全路径
            var content = File.ReadAllText(jsFilePath);
            var result = JsCompiler.CompileAssemblyFromSource(opt, content);
            if (result.Errors.Count > 0)
            {
                log.Error($"[Compile Error:{result.Errors.Count}]");
                foreach (var error in result.Errors)
                    log.Error(error);

                return null;
            }
            var js = result.CompiledAssembly;
            dynamic instace = js.CreateInstance(jsClassName);
            return instace;
        }

        #endregion



    }
}