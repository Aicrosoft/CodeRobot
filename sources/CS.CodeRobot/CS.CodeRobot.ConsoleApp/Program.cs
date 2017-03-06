using System;
using CS.CodeRobot.JsGenerators;

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
