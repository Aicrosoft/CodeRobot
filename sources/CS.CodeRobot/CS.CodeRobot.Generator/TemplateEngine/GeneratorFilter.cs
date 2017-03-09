using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Xml;
using CS.CodeRobot.Generators;
using CS.Extension;
using CS.Logging;
using CS.Utils;
using DatabaseSchemaReader.DataSchema;
using DotLiquid;

namespace CS.CodeRobot.TemplateEngine
{
    public class GeneratorFilter
    {
        static GeneratorFilter()
        {
            //TemplateApp = new TemplateApp();
            CodeFiles = new List<CodeFile>();
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(GeneratorFilter));



        /// <summary>
        /// 当前项目信息
        /// </summary>
        public static ProjectMeta ProjectInfo { get; set; }

        /// <summary>
        /// 模板
        /// </summary>
        public static TemplateApp TemplateApp => Generator.TemplateApp;

        const string NameSpaceURI = "http://schemas.microsoft.com/developer/msbuild/2003";


        private static readonly List<CodeFile> CodeFiles;//所有的生成的实体类及其所在文件夹的信息

        public static void InitCodeFiles(string sub)
        {
            CodeFiles.Clear();
            log.Debug("清除所有的CodeFiles缓存信息");
        }

        public static void RenderTable(string dsName,string tbName)
        {
            log.Debug($"{dsName} - {tbName}");
        }

        /// <summary>
        /// 是否为根目下的文件
        /// </summary>
        /// <param name="sub"></param>
        /// <param name="templateFile"></param>
        /// <param name="clsName">模板对应的基本类名称，会自动加上.cs 或.Auto.cs</param>
        /// <param name="hasAuto">是否包含自动模板</param>
        /// <param name="dbSettingName"></param>
        public static void Render(string sub, string templateFile, string clsName = null, bool hasAuto = false, string dbSettingName = null)
        {
            //var pi = ProjectInfo;
            //var ai = new ModelMeta(sub, pi);//assemblyInfo

            //var absFile = $"{pi.GetTemplateDirectory(sub)}{templateFile}";
            //var className = clsName == null ? $"{templateFile.Replace(".tpl", "")}.cs" : $"{clsName}.cs";
            //var codeFile = new CodeFile { Name = className, Sub = dbSettingName };
            //var dbSetting = pi.DbConns.FirstOrDefault(x => x.Name == dbSettingName);
            //var result = TemplateApp.Render(absFile, new { sub,  pi, dbSetting });
            //var destFile = $"{pi.GetOutputDirectory(ai.Name, dbSettingName)}{className}";
            //FileHelper.Save(destFile, result);

            //if (hasAuto)
            //{
            //    codeFile.AutoName = codeFile.Name.Replace(".cs", ".Auto.cs");
            //    var absAutoFile = absFile.Replace(".tpl", ".Auto.tpl");
            //    result = TemplateApp.Render(absAutoFile, new { sub, pi, dbSetting });
            //    destFile = $"{pi.GetOutputDirectory(ai.Name, dbSettingName)}{codeFile.AutoName}";
            //    FileHelper.Save(destFile, result);
            //}

            //CodeFiles.Add(codeFile);
        }

        public static void Debug(DatabaseTable o)
        {
            var x = o.Name;
            log.Error(x);
        }

        
        /// <summary>
        /// 写的时候必须小写？
        /// </summary>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Substr(string value, int startIndex, int length = -1)
        {
            if (length >= 0)
                return value.Substring(startIndex, length);
            return value.Substring(startIndex);
        }
        
    }
}