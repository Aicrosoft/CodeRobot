using System;
using System.CodeDom.Compiler;
using Microsoft.JScript;

namespace CS.CodeRobot
{
    public class JsDemo
    {
        public static void Run(string[] args)
        {
            //ScriptManager.RegisterStartupScript(Page, GetType(), "clicktest", "clicktest()", true);


            //ClientScript.RegisterStartupScript(this.GetType(), "clear", "<script>test()</script>");

              string Source = @"
package Test 
{ 
  class HelloWorld 
  { 
    function Hello(name) { return ""Hello, "" + name; }

    

  }
}";

        var provider = new JScriptCodeProvider();
            var compiler = provider.CreateCompiler();
            var parameters = new CompilerParameters { GenerateInMemory = true };
            var results = compiler.CompileAssemblyFromSource(parameters, Source);
            var assembly = results.CompiledAssembly;
            dynamic instance = Activator.CreateInstance(assembly.GetType("Test.HelloWorld"));
            var result = instance.Hello("World");
            Console.WriteLine("Result: {0}", result);



        }
    }
}