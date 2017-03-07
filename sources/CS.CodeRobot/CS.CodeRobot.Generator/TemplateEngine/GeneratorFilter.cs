using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using CS.CodeRobot.Generators;
using CS.Extension;
using CS.Logging;
using CS.Utils;
using DotLiquid;

namespace CS.CodeRobot.TemplateEngine
{
    public class GeneratorFilter
    {
        static GeneratorFilter()
        {
            TemplateApp = new TemplateApp();
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
        public static TemplateApp TemplateApp { get; }

        const string NameSpaceURI = "http://schemas.microsoft.com/developer/msbuild/2003";


        private static readonly List<CodeFile> CodeFiles;//所有的生成的实体类及其所在文件夹的信息

        public static void InitCodeFiles(string sub)
        {
            CodeFiles.Clear();
            log.Debug("清除所有的CodeFiles缓存信息");
        }

        /// <summary>
        /// 是否为根目下的文件
        /// </summary>
        /// <param name="sub"></param>
        /// <param name="templateFile"></param>
        /// <param name="folderName"></param>
        public static void Render(string sub, string templateFile, string folderName = null)
        {
            var pi = ProjectInfo;
            var ai = new AssemblySetting(sub, pi);//assemblyInfo

            var absFile = $"{pi.GetTemplateDirectory(sub)}{templateFile}";
            var className = $"{templateFile.Replace(".tpl","")}.cs";
            var result = TemplateApp.Render(absFile, new { sub, pi });
            var destFile = $"{pi.GetOutputDirectory(ai.ModelLayer,folderName)}{className}";
            FileHelper.Save(destFile, result);
            CodeFiles.Add(new CodeFile() {Name = className});


          
        }

        /// <summary>
        /// 更新工程项目配置文件
        /// </summary>
        /// <param name="sub"></param>
        public static void UpdateProject(string sub)
        {
            var pi = ProjectInfo;
            var xmlDoc = new XmlDocument();
            //var ai = new AssemblySetting(sub, pi);//assemblyInfo
            var projectXml = $"{pi.GetOutputDirectory(sub)}{pi.GetAssemblyName(sub)}.csproj";
            xmlDoc.Load(projectXml);
            var nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("ns", NameSpaceURI);
            var assName = pi.GetAssemblyName(sub);
            xmlDoc.ChangeProjectInfo(assName, nsmgr);

            xmlDoc.ChangeAutoItems(nsmgr, CodeFiles);
            xmlDoc.Save(projectXml);
            log.Info($"{sub} 的project.csproj 工程文件更新完毕");
        }


        /// <summary>
        /// 生成项目文件
        /// </summary>
        /// <param name="sub"></param>
        public static void Projetcs(string sub)
        {
            var pi = ProjectInfo;
            //var xx = pi.GetTemplateDirectory(sub);
            var configFile = Directory.GetFiles($"{pi.GetTemplateDirectory(sub)}").FirstOrDefault(x => FileHelper.GetExtension(x) == ".csproj");
            if (string.IsNullOrWhiteSpace(configFile))
            {
                log.Error($"{sub}项目下的project.csproj文件模板丢失，请更正后重新执行生成功作。");
                return;
            }
            var proFile = $"{pi.GetOutputDirectory(sub)}{pi.GetAssemblyName(sub)}.csproj";
            FileHelper.Copy(configFile, proFile);
            var msg = $"{proFile} 项目文件生成成功";
            log.Info(msg);
        }


        /// <summary>
        /// 将所有的config文件拷至项目目录
        /// </summary>
        /// <param name="sub"></param>
        /// <returns></returns>
        public static void Configs(string sub)
        {
            var pi = ProjectInfo;
            //var xx = pi.GetTemplateDirectory(sub);
            var configFiles = Directory.GetFiles($"{pi.GetTemplateDirectory(sub)}").Where(x => FileHelper.GetExtension(x) == ".config").ToArray();
            if (configFiles.Length == 0)
            {
                log.Info("无config文件以生成"); 
                return;
            }
            FileHelper.Copy(configFiles, pi.GetOutputDirectory(sub));
            var msg = $"{sub} 下的 {string.Join(",",FileHelper.GetFileNames(configFiles))} 程序集的config生成成功";
            log.Info(msg);
            //return msg;
        }

        /// <summary>
        /// 生成程序集信息
        /// </summary>
        /// <param name="sub"></param>
        public static void Assembly(string sub)
        {
            var pi = ProjectInfo;
            var ai = new AssemblySetting(sub, pi);//assemblyInfo
            var fileName = $"{pi.TemplatesRootDirectory}AssemblyInfo.tpl";
            var result = TemplateApp.Render(fileName, new { pi, ai });
            //log.Debug(result);

            var asFile = $"{pi.GetOutputDirectory(ai.ModelLayer)}Properties\\AssemblyInfo.cs";
            FileHelper.Save(asFile, result);
            var msg = $"{sub} 程序集信息写入完成";
            log.Info(msg);
            //return msg;
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