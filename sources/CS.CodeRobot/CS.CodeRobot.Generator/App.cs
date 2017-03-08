using System;
using System.Collections.Generic;
using System.Linq;
using CS.CodeRobot.Generators;
using CS.CodeRobot.TemplateEngine;
using CS.Logging;
using CS.Storage;
using CS.Utils;

namespace CS.CodeRobot
{
    public class App
    {
        private readonly ILog log = LogManager.GetLogger(typeof(Generator));

        private const string TemplatesFolder = @"Templates\{0}\";

        /// <summary>
        /// 静态实例
        /// </summary>
        public static App Instance { get; set; }

        /// <summary>
        /// 生成或读取工程信息
        /// </summary>
        public ProjectMeta Project { get; private set; }

        public string RootTemplateFullPath { get; set; }

        ///// <summary>
        ///// 所有模板子目录名称
        ///// </summary>
        //public List<string> Subs { get; set; }
        /// <summary>
        /// 所有子模板对应的程序集设定信息
        /// </summary>
        public List<ModelMeta> Models { get; set; }


        public App()
        {
            Instance = this;
            TemplateRegister.Init();
        }

        /// <summary>
        /// 传入项目所在目录名称
        /// </summary>
        /// <param name="projectFolderName"></param>
        public App(string projectFolderName) : this()
        {
            if (string.IsNullOrWhiteSpace(projectFolderName)) throw new ArgumentException("请输入Templates下的工程文件夹名称。");
            var projectMetaFile = $"{string.Format(TemplatesFolder, projectFolderName)}project.xml";
            //生成项目文件示例
            var projectFile = AppHelper.GetFileFullName(projectMetaFile);
            //DebugConsole.Error(projectFile);
            Project = XmlStorage.Load<ProjectMeta>(projectFile);
            //Debug.Green(p1.ToJsonByJc());
            Project.TemplatesRootDirectory = $"~/Templates/{projectFolderName}/";
            RootTemplateFullPath = FileHelper.GetFullPath(Project.TemplatesRootDirectory);
            var subs = FileHelper.GetSubFolderNames(RootTemplateFullPath).ToList(); //子模板目录
            if (subs.Count == 0)
                log.Error("子模板不存在，无法生成项目。");
            Models = new List<ModelMeta>();
            foreach (var sub in subs)
            {
                Models.Add(new ModelMeta(sub, Project));
            }
        }





    }
}
