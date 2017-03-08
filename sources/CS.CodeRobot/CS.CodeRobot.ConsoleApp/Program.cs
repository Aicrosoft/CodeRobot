using System;
using CS.CodeRobot.Generators;
using CS.CodeRobot.JsGenerators;

namespace CS.CodeRobot
{
    class Program
    {
        static void Main(string[] args)
        {

            //DotLiquidDemo.Run();

            //DotLiquidDemo.RunExt();

            //DataSchemaDemo.Run();

            //DotLiquidDemo.RunCMS();

            //Generator.Run(args);

            //JsSample.Run();

            JsGenerator.Run(args);

            Console.WriteLine(@"press any key to close.");
            Console.ReadKey();
        }
    }
}
