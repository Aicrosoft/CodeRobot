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

        static JnTemplateApp()
        {
            var conf = JinianNet.JNTemplate.Configuration.EngineConfig.CreateDefault();
            //禁止清除标签前后空白字符
            conf.StripWhiteSpace = false;

            //出错时是否抛出异常
            conf.ThrowExceptions = true;

            Engine.Configure(conf);
        }



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



    }
}