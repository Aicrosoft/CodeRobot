using System;
using System.Runtime.CompilerServices;
using CS.CodeRobot.Generators;
using CS.CodeRobot.JsGenerators;
using CS.CodeRobot.TemplateEngine;
using JinianNet.JNTemplate;

namespace CS.CodeRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            //var a = new {a = new DbSetting(), pi = new ProjectMeta()};
            //JntTemplateApp.Render("x", a);

            JsGenerator.Run(args);

//            var tmp = @"$a.Name



//    $a.Age
//";

//            var t = (Template) JinianNet.JNTemplate.Engine.CreateTemplate(tmp);
//            t.Set("a", new { Name = "tester", Age = 15 });
//            var rst = t.Render();



            Console.WriteLine(@"press any key to close.");
            Console.ReadKey();
        }
    }
}
