using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS.CodeRobot.TemplateEngine;
using CS.Extension;
using CS.Logging;
using DotLiquid;

namespace CS.CodeRobot
{
    public class DotLiquidDemo
    {

        public static void RunCMS()
        {
            var log = LogManager.GetLogger(typeof (DotLiquidDemo));
            //log.Debug("this is a Debug");
            //log.Info("this is a Info");
            //log.Warn("this is a Warn");
            //log.Error("this is a Error");
            //log.Fatal("this is a Fatal");


            //var p = App.GetProject("CSCMS");
            //log.Debug(p.ToJsonByJc());




            Console.ReadLine();
            return;





            // 在原有的代码下添加,否则将不能显示描绘的对象
            Template.RegisterSafeType(typeof(ExampleViewModel), Hash.FromAnonymousObject);
            // 在原有的代码下添加
            Template.FileSystem = new DotliquidTemplateFileSystem();
            var path = $"~/Templates/CSCMS/app.tpl";
            //var path = $"~/Templates/demo.tpl";
            // 根据路径读取模板内容
            var templateStr = Template.FileSystem.ReadTemplateFile(new Context(), "'" + path + "'");
            // 解析模板，这里可以缓存Parse出来的对象，但是为了简单这里就略去了
            var template = Template.Parse(templateStr);
            // 描画模板
            var m = new ExampleViewModel();
            m.Age = 31;
            m.Name = "atwind zhou";
            var pms = Hash.FromAnonymousObject(new { m });
            var result = template.Render(pms);
            // 返回描画出来的内容
            // return Content(result, "text/html");
            Console.WriteLine(result);


            Console.ReadLine();
        }

        public static void RunExt()
        {
            Console.WriteLine("debug");
            

            // 在原有的代码下添加,否则将不能显示描绘的对象
            Template.RegisterSafeType(typeof(ExampleViewModel), Hash.FromAnonymousObject);
            // 在原有的代码下添加
            Template.FileSystem = new DotliquidTemplateFileSystem();
            var path = $"~/Templates/HelloExtends.tpl";
            //var path = $"~/Templates/demo.tpl";
            // 根据路径读取模板内容
            var templateStr = Template.FileSystem.ReadTemplateFile(new Context(), "'" + path + "'");
            // 解析模板，这里可以缓存Parse出来的对象，但是为了简单这里就略去了
            var template = Template.Parse(templateStr);
            // 描画模板
            var m = new ExampleViewModel();
            m.Age = 31;
            m.Name = "atwind zhou";
            var pms = Hash.FromAnonymousObject(new { m });
            var result = template.Render(pms);
            // 返回描画出来的内容
            // return Content(result, "text/html");
            Console.WriteLine(result);


            Console.ReadLine();
        }


        public static void Run()
        {
            //Liquid.UseRubyDateFormat = true;

            Console.WriteLine("debug");
            //var template = Template.Parse("tmp");
            //Template.RegisterFilter(typeof(TextFilter));


            //    Template template = Template.Parse(" {{ '*hi*' | textilize }} ");
            //template.Render(filters: new[] { typeof(TextFilter) }); // => "<b>*hi*</b>" 



            //var template = Template.Parse("Hello, {{ name }}!");
            //var result = template.Render(Hash.FromAnonymousObject(new { name = "World" }));
            //Console.WriteLine(result);

            // 在原有的代码下添加,否则将不能显示描绘的对象
            Template.RegisterSafeType(typeof(ExampleViewModel), Hash.FromAnonymousObject);
            // 在原有的代码下添加
            Template.FileSystem = new DotliquidTemplateFileSystem();
            var path = $"~/Templates/demo.tpl";
            //var path = $"~/Templates/demo.tpl";
            // 根据路径读取模板内容
            var templateStr = Template.FileSystem.ReadTemplateFile(new Context(), "'" + path + "'");
            // 解析模板，这里可以缓存Parse出来的对象，但是为了简单这里就略去了
            var template = Template.Parse(templateStr);
            // 描画模板
            var m = new ExampleViewModel();
            m.Age = 31;
            m.Name = "atwind zhou";
            var pms = Hash.FromAnonymousObject(new { m });
            var result = template.Render(pms);
            // 返回描画出来的内容
            // return Content(result, "text/html");
            Console.WriteLine(result);


            //var template2 = Template.Parse("Name: {{ m2.Name }}, Age: {{ m2.Age }}");
            //var m2 = new ExampleViewModel() { Name = "john", Age = 35 };
            //var result2 = template2.Render(Hash.FromAnonymousObject(new { m2 }));
            //Console.WriteLine(result2);

            Console.ReadLine();
        }

        public static class TextFilter
        {
            public static string Textilize(string input)
            {
                return $"hi-{input}-end";
            }
        }

    }

    public class ExampleViewModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }


}