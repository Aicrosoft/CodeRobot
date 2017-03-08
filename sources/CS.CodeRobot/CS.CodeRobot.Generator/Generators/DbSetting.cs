using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using CS.CodeRobot.SchemaEngine;
using CS.Extension;
using DatabaseSchemaReader.DataSchema;

namespace CS.CodeRobot.Generators
{
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

        private List<DatabaseTable> _tbs;

        /// <summary>
        /// 数据库Schema信息
        /// </summary>
        [XmlIgnore]
        public List<DatabaseTable> Tables
        {
            get
            {
                if (_tbs != null) return _tbs;
                var dbApp = new DataSchemaApp(DbConnName);
                _tbs = dbApp.ReadTables().ToList();
                return _tbs;
            }
        }

        //private DatabaseTableDrop[] _tbs;

        ///// <summary>
        ///// 数据库Schema信息
        ///// </summary>
        //[XmlIgnore]
        //public DatabaseTableDrop[] Tables
        //{
        //    get
        //    {
        //        if (_tbs != null) return _tbs;
        //        var dbApp = new DataSchemaApp(DbConnName);
        //        var tbs = dbApp.ReadTables();
        //        _tbs = tbs.Select(tb => new DatabaseTableDrop(tb)).ToArray();
        //        return _tbs;
        //    }
        //}

        //[XmlIgnore]
        //public string[] TableNames
        //{
        //    get { return Tables.Select(x => x.Table.Name).ToArray(); }
        //}


    }
}