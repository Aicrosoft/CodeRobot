using CS.Caching;
using CS.CodeRobot.Generators;
using JinianNet.JNTemplate;

namespace CS.CodeRobot.JnTemplateEngine
{
    /// <summary>
    /// JNT模板引擎
    /// </summary>
    public class JnTemplateApp
    {
        private static readonly TinyCache<ITemplate> Cache = new TinyCache<ITemplate>();


        public static Template CreateTemplate(string absFilePath)
        {
            var template = Cache.Get(absFilePath, x =>
            {
                var t = Engine.LoadTemplate(absFilePath);
                t.Context.TempData["helper"] = new MySqlDatabaseHelper();
                return t;
            });
            var tmp =  (Template)template;
            return tmp;
        }

        public static string Render(string absFilePath, dynamic obj)
        {
            var type = obj.GetType();
            
            



            return null;
        }



    }
}