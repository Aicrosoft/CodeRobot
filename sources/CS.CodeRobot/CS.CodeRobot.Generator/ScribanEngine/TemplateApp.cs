using System.IO;
using CS.Caching;
using CS.Utils;
using Scriban;

namespace CS.CodeRobot.ScribanEngine
{
    public class TemplateApp
    {
        private static readonly TinyCache<Template> Cache = new TinyCache<Template>();

        public static string Render(string tplFile,dynamic obj)
        {
            var tmp = Template.Parse(File.ReadAllText(tplFile));
            var rst = tmp.Render(obj);
            return rst;
        }

    }
}