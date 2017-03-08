using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Xml.Serialization;
using CS.CodeRobot.SchemaEngine;
using CS.CodeRobot.TemplateEngine;
using CS.Extension;
using CS.Storage;
using CS.Utils;
using DatabaseSchemaReader.DataSchema;

namespace CS.CodeRobot.Generators
{
    /// <summary>
    /// 自动生成代码相关设定
    /// </summary>
    public class ProjectMeta
    {

        public ProjectMeta()
        {
            DbConns = new List<DbSetting>();
        }


        /// <summary>
        /// 当前项目关联的所有连接名称
        /// </summary>
        [XmlArray]
        public List<DbSetting> DbConns { get; set; }

        /// <summary>
        /// 模板所在的根目录
        /// </summary>
        [XmlIgnore]
        public string TemplatesRootDirectory { get; set; }


        /// <summary>
        /// 输出根目录
        /// </summary>
        public string OutputRootDirectory { get; set; }

        /// <summary>
        /// 项目名称，与模板目录下的项目模板文件夹名称相同
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 名字空间
        /// <remarks>
        /// 将和不同模型名称组成完整的namespace和程序集名称
        /// </remarks>
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// 公司
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 版权
        /// </summary>
        public string Copyright { get; set; }

        #region 便捷属性 




        public string GetAssemblyName(string modelLayer)
        {
            return $"{Namespace}.{modelLayer}";
        }

        /// <summary>
        /// 返回指定模型层模板所在的根目录
        /// </summary>
        /// <param name="modelLayer"></param>
        /// <returns></returns>
        public string GetTemplateDirectory(string modelLayer)
        {
            //return GetTemplateDirectory(modelLayer);
            var rePath = $"{TemplatesRootDirectory}{modelLayer}/";
            return FileHelper.GetFullPath(rePath);
        }
        /// <summary>
        /// 返回指定模型层的输出根目录
        /// </summary>
        /// <param name="modelLayer"></param>
        /// <returns></returns>
        public string GetOutputDirectory(string modelLayer)
        {
            //return GetOutputDirectory(modelLayer);
            return $"{OutputRootDirectory}{Namespace}.Respository\\{Namespace}.{modelLayer}\\";
        }

        /// <summary>
        /// 返回子目录（如果存在的话）
        /// </summary>
        /// <param name="modelLayer"></param>
        /// <param name="subFolder"></param>
        /// <returns></returns>
        public string GetOutputDirectory(string modelLayer, string subFolder)
        {
            var path = $"{GetOutputDirectory(modelLayer)}{subFolder}\\".Replace("\\\\", "\\");
            return path;
        }

        //string GetTemplateDirectory(string assemblyName)
        //{
        //    return $"{TemplatesRootDirectory}{assemblyName}\\";
        //}

        //string GetOutputDirectory(string assmeblyName)
        //{
        //    return $"{OutputRootDirectory}{Namespace}.Respository\\{Namespace}.{assmeblyName}\\";
        //}


        #endregion

        /// <summary>
        /// 检验配置文件的正确性
        /// </summary>
        /// <returns></returns>
        public bool Valid()
        {
            if (TemplatesRootDirectory.IsNullOrWhiteSpace() || TemplatesRootDirectory.LastIndexOf(@"\", StringComparison.Ordinal) < 0) throw new ConfigurationErrorsException(@"模板根目录必须以 \ 结束。");
            if (OutputRootDirectory.IsNullOrWhiteSpace() || OutputRootDirectory.LastIndexOf(@"\", StringComparison.Ordinal) < 0) throw new ConfigurationErrorsException(@"输出根目录必须以 \ 结束。");


            return true;
        }

    }
   

}