using System;
using System.CodeDom.Compiler;
using System.IO;
using Microsoft.JScript;

namespace CS.CodeRobot.JsGenerators
{
    public class JsCompiler
    {
        private readonly static JScriptCodeProvider compiler;

        static JsCompiler()
        {
            compiler = new JScriptCodeProvider();
            //var opt = new CompilerParameters
            //{
            //    //opt.ReferencedAssemblies.Add("System.dll");
            //    GenerateExecutable = false,
            //    GenerateInMemory = true
            //};
        }

        

        public static void RunJs(string filePath)
        {
            var opt = new CompilerParameters
            {
                GenerateExecutable = false,
                GenerateInMemory = true
            };
            var content = File.ReadAllText(filePath);
            var result = compiler.CompileAssemblyFromSource(opt, content);
            if (result.Errors.Count > 0)
            {
                Console.WriteLine("Compile Error");
                return;
            }
            var js = result.CompiledAssembly;
            dynamic jsProg = js.CreateInstance("JsProgram");
            jsProg.fk(new JsDk() { Name = "tester" });
        }
    }
}