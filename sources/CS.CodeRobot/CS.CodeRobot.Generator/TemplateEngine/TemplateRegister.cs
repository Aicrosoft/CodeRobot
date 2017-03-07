using System;
using CS.CodeRobot.Generators;
using DotLiquid;

namespace CS.CodeRobot.TemplateEngine
{
    public class TemplateRegister
    {
        public static void Init()
        {
            TemplateInit();
            RegisterTypes();
            RegisterFilters();

        }

        public static void TemplateInit()
        {
            //Template.FileSystem = new DotliquidTemplateFileSystem();
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        public static void RegisterTypes()
        {
            //Template.RegisterSafeType(typeof(AssemblySetting), Hash.FromAnonymousObject);
            //Template.RegisterSafeType(typeof(DbSetting), Hash.FromAnonymousObject);
            //Template.RegisterSafeType(typeof(DateTime), Hash.FromAnonymousObject);
            RegisterTypes(new[] {
                typeof(AssemblySetting),
                typeof(DbSetting),
                typeof(ProjectMeta),
                //typeof(DateTime),

            });
        }

        public static void RegisterTypes(Type[] types)
        {
            foreach (var type in types)
            {
                Template.RegisterSafeType(type, Hash.FromAnonymousObject);
            }
        }

        /// <summary>
        /// 注册过滤器
        /// </summary>
        public static void RegisterFilters()
        {
            RegisterFilters(new[] {
                typeof(DateTimeFilter),
                //typeof(DemoFilter),
                typeof(GeneratorFilter),
                typeof(TemaplteFilter),

            });
        }


        public static void RegisterFilters(Type[] types)
        {
            //Template.RegisterFilter(typeof(DateTimeFilter));
            foreach (var type in types)
            {
                Template.RegisterFilter(type);
            }
        }

    }
}