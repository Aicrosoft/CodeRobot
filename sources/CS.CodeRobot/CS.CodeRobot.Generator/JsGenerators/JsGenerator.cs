using System;
using System.CodeDom.Compiler;
using System.IO;
using CS.Logging;
using Microsoft.JScript;

namespace CS.CodeRobot.JsGenerators
{
    public class JsGenerator
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(JsGenerator));

        private static  App app ;

        public static void Run(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(@"请输入项目模板的文件夹名称。");
                Console.ReadLine();
            }
            app = new App(args[0]);
            var indexJs = $"{app.RootTemplateFullPath}index.js";
            RunJs(indexJs);

            foreach (var model in app.Models)
            {
                var js = $"{app.RootTemplateFullPath}{model}\\index.js";
                //RunJs(js);
            }
        }

        private static readonly JScriptCodeProvider JsCompiler = new JScriptCodeProvider();

        public static void RunJs(string filePath)
        {
            var opt = new CompilerParameters
            {
                GenerateExecutable = false,
                GenerateInMemory = true
            };
            //opt.ReferencedAssemblies.Add("System.dll");
            opt.ReferencedAssemblies.Add("CS.Utility.dll");
            opt.ReferencedAssemblies.Add("CS.CodeRobot.Generator.dll");
            var content = File.ReadAllText(filePath);
            var result = JsCompiler.CompileAssemblyFromSource(opt, content);
            if (result.Errors.Count > 0)
            {
                log.Error($"[Compile Error:{result.Errors.Count}]");
                foreach (var error in result.Errors)
                {
                    log.Error(error);
                }
                return;
            }
            var js = result.CompiledAssembly;
            dynamic instace = js.CreateInstance("JsProgram");
            //Action<string> x = DebugConsole.Error;
            Action<string> print = log.Debug;	 
            instace.main(app);
        }

    }
}