using System;
using CS.CodeRobot.Generators;
using CS.CodeRobot.TemplateEngine;
using CS.Storage;
using CS.Utils;

namespace CS.CodeRobot
{
    public class App
    {

        private const string TemplatesFolder = @"Templates\{0}\";

        /// <summary>
        /// 生成或读取工程信息
        /// </summary>
        public ProjectMeta ProjectInfo { get; private set; }

        public App()
        {
            TemplateRegister.Init();
        }


        public App(string templateFolderName):this()
        {
            if (string.IsNullOrWhiteSpace(templateFolderName)) throw new ArgumentException("请输入Templates下的工程文件夹名称。");
            var projectMetaFile = $"{string.Format(TemplatesFolder, templateFolderName)}project.xml";
            //生成项目文件示例
            var projectFile = AppHelper.GetFileFullName(projectMetaFile);
            //DebugConsole.Error(projectFile);
            var p1 = XmlStorage.Load<ProjectMeta>(projectFile);
            //Debug.Green(p1.ToJsonByJc());
            p1.TemplatesRootDirectory = $"~/Templates/{templateFolderName}/";

            ProjectInfo =  p1;
        }

    }
}
