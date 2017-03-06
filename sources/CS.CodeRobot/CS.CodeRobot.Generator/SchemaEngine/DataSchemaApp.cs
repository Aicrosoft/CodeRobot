using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using DatabaseSchemaReader;
using DatabaseSchemaReader.DataSchema;

namespace CS.CodeRobot.SchemaEngine
{
    public class DataSchemaApp
    {
        public DataSchemaApp(string connName)
        {
            var connSettings = ConfigurationManager.ConnectionStrings[connName];
            _databaseReader = new DatabaseReader(connSettings.ConnectionString, connSettings.ProviderName);
        }

        readonly DatabaseReader _databaseReader;

        /// <summary>
        /// 返回所有表
        /// </summary>
        public IList<DatabaseTable> AllTables => _databaseReader.AllTables();

        public DatabaseSchema ReadAll()
        {
            var all = _databaseReader.ReadAll();
            all.StoredProcedures.Clear(); //不要存储过程的信息
            all.Functions.Clear(); //不要所有的function
            all.Users.Clear(); //不要所有的用户信息
            all.Views.Clear(); //不要所有的视频信息
            all.DataTypes.Clear(); //不需要全部显示，只在表中表达即可


            return all;
        }

    }
}