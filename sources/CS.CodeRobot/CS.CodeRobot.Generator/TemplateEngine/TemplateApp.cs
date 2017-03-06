using System.Collections;
using System.Collections.Generic;
using DotLiquid;

namespace CS.CodeRobot.TemplateEngine
{
    public class TemplateApp
    {
        public TemplateApp()
        {
            Template.FileSystem = new DotliquidTemplateFileSystem();
            _templates = new Dictionary<string, Template>();
        }

        private readonly Dictionary<string, Template> _templates; 

        /// <summary>
        /// 相对模板项目目录的相对路径
        /// 如：AssemblyInfo.tpl  ,  /Model/Items/Model.Auto.tpl
        /// </summary>
        /// <param name="relFile"></param>
        /// <returns></returns>
        public Template GeTemplate(string relFile)
        {
            Template template = null;
            if (_templates.TryGetValue(relFile, out template))
                return template;

            //var path = $"~/Templates/CSCMS/AssemblyInfo.tpl";
            // 根据路径读取模板内容
            var templateStr = Template.FileSystem.ReadTemplateFile(new Context(), "'" + relFile + "'");
            template = Template.Parse(templateStr);
            if (template != null)
            {
                _templates[relFile] = template;
            }
            return template;
        }

        /// <summary>
        /// 渲染出结果
        /// </summary>
        /// <param name="relFile"></param>
        /// <param name="objects"></param>
        /// <returns></returns>
        public string Render(string relFile, params object[] objects)
        {
            var pms = Hash.FromAnonymousObject(new {objects});
            var template = GeTemplate(relFile);
            var result = template.Render(pms);
            return result;
        }


    }
}