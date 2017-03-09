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
        /// 项目的名字空间
        /// <remarks>
        /// 所有的子模板将以该名字空间为基础，如 CS.CMS .XXX
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


        /// <summary>
        /// 返回指定模型层模板所在的根目录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string GetTemplateDirectory(ModelMeta model)
        {
            var rePath = $"{TemplatesRootDirectory}{model.Name}/";
            return FileHelper.GetFullPath(rePath);
        }

        /// <summary>
        /// 获取某一目录下的相对路径文件的全路径
        /// </summary>
        /// <param name="relFileName"></param>
        /// <returns></returns>
        public string GetTemplateFullPath(string relFileName)
        {
            return FileHelper.GetFullPath($"{TemplatesRootDirectory}{relFileName}");
        }

        /// <summary>
        /// 返回指定模型层的输出根目录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string GetOutputDirectory(ModelMeta model)
        {
            //return GetOutputDirectory(modelLayer);
            return $"{OutputRootDirectory}{Namespace}.Respository\\{model.AssemblyName}\\";
        }

        /// <summary>
        /// 返回子目录（如果存在的话）
        /// </summary>
        /// <param name="model"></param>
        /// <param name="subFolder"></param>
        /// <returns></returns>
        public string GetOutputDirectory(ModelMeta model, string subFolder)
        {
            var path = $"{GetOutputDirectory(model)}{subFolder}\\".Replace("\\\\", "\\");
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