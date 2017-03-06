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

        public static void RegisterTypes()
        {
            //Template.RegisterSafeType(typeof(AssemblySetting), Hash.FromAnonymousObject);
            //Template.RegisterSafeType(typeof(DbSetting), Hash.FromAnonymousObject);
            //Template.RegisterSafeType(typeof(DateTime), Hash.FromAnonymousObject);
            RegisterTypes(new[] {
                typeof(AssemblySetting),
                typeof(DbSetting),
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


        public static void RegisterFilters()
        {
            RegisterFilters(new[] {
                typeof(DateTimeFilter),
                typeof(DemoFilter),

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