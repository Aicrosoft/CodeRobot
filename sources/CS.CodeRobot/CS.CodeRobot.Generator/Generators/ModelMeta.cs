using System;
using System.Xml.Serialization;

namespace CS.CodeRobot.Generators
{

    /// <summary>
    /// 程序集模型信息 配置
    /// </summary>
    //[Serializable]
    public class ModelMeta
    {
        public ModelMeta(string name, ProjectMeta meta = null)
        {
            Name = name;
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
        /// 模型层类型(即SubName)
        /// </summary>
        public string Name { get; set; }

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
        public string NameSpace => $"{RootNamespace}.{Name}";

        public string AssemblyName => $"{RootAssemblyName}.{Name}";
        /// <summary>
        /// 产品信息
        /// </summary>
        public string AssemblyProduct => $"{RootAssemblyName}.{Name}";

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
}