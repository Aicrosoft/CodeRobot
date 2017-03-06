using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml.Serialization;
using CS.Extension;
using CS.Storage;
using CS.Utils;

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
            return $"{TemplatesRootDirectory}{modelLayer}\\";
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

    public enum ModelStyleType
    {
        /// <summary>
        /// Model层
        /// </summary>
        Model,

        /// <summary>
        /// Db访问层
        /// </summary>
        DbAccess,

        /// <summary>
        /// Db访问代理（缓存层）
        /// </summary>
        DbProxy,

    }

    /// <summary>
    /// 程序集配置
    /// </summary>
    //[Serializable]
    public class AssemblySetting
    {
        public AssemblySetting(string modelLayer, ProjectMeta meta = null)
        {
            ModelLayer = modelLayer;
            RootNamespace = meta?.Namespace ?? "CS";
            RootAssemblyName = meta?.Namespace ?? "CS";
            Guid = System.Guid.NewGuid().ToString();
            AssemblyCompany = meta?.Company ?? "Chaso";
            AssemblyCopyright = meta?.Copyright ?? "cszi.com";
            var now = DateTime.Now;
            Now = now;
            //Year = now.Year.ToString();
            AutoVersion = $"{now:yyyy}.{now:MMdd}.{now:HHmm}";
        }
        /// <summary>
        /// 模型层类型
        /// </summary>
        public string ModelLayer { get; set; }

        ///// <summary>
        ///// 当前程序集名称
        ///// <remarks>
        ///// 如 CS.DataAccess. "Name"  中的 Name
        ///// </remarks>
        ///// </summary>
        //[XmlIgnore]
        //public string Name { get; set; }
        /// <summary>
        /// 名字空间全名
        /// </summary>
        [XmlIgnore]
        public string NameSpace => $"{RootNamespace}.{ModelLayer}";

        public string AssemblyName => $"{RootAssemblyName}.{ModelLayer}";
        /// <summary>
        /// 产品信息
        /// </summary>
        public string AssemblyProduct => $"{RootAssemblyName}.{ModelLayer}";

        /// <summary>
        /// 基础名字空间
        /// </summary>
        [XmlAttribute]
        public string RootNamespace { get; set; }

        /// <summary>
        /// 基础程序集名
        /// </summary>
        [XmlAttribute]
        public string RootAssemblyName { get; set; }

        /// <summary>
        /// 公司信息
        /// </summary>
        [XmlAttribute]
        public string AssemblyCompany { get; set; }

        /// <summary>
        /// 版本信息
        /// </summary>
        [XmlAttribute]
        public string AssemblyCopyright { get; set; }
        /// <summary>
        /// 商标
        /// </summary>
        [XmlAttribute]
        public string AssemblyTrademark => "CSWare";

        /// <summary>
        /// GUID
        /// </summary>
        [XmlIgnore]
        public string Guid { get; set; }
        /// <summary>
        /// 自动版本号
        /// </summary>
        [XmlIgnore]
        public string AutoVersion { get; set; }

        ///// <summary>
        ///// 当前时间
        ///// </summary>
        //[XmlAttribute]
        //public string Year { get; set; }

        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime Now { get; set; }

    }

    /// <summary>
    /// 数据源配置
    /// </summary>
    [Serializable]
    public class DbSetting
    {
        /// <summary>
        /// 连接名称
        /// </summary>
        [XmlAttribute]
        public string DbConnName { get; set; }
        /// <summary>
        /// 连接的库对应的名字空间名，目的是为了避免出现不同的库可能会有同名的表
        /// <remarks>
        /// 如 CS.Data.AccessLayer.{Name} 
        /// </remarks>
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }

        private string _dbContextName;
        /// <summary>
        /// EF对应的DbContext名字
        /// </summary>
        [XmlAttribute]
        public string DbContextName
        {
            get
            {
                return _dbContextName.IsNullOrWhiteSpace() ? $"{Name}DbContext" : _dbContextName;
            }
            set { _dbContextName = value; }
        }


    }

}