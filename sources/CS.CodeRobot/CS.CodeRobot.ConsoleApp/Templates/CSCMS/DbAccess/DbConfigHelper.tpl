using System.Configuration;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace ${model.Namespace}
{
    public class DbConfigHelper
    {
        /// <summary>
        /// 获取数据库连接实例
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static DbConnection GetDbConnection(string dbName)
        {
            var dbSet = GetDbSetting(dbName);
            if (dbSet == null) throw new ConfigurationErrorsException($"dbName={dbName}的数据连接配置不存在，或者不可用。");
            DbConnection conn = null;
            switch (dbSet.ProviderName)
            {
                case "MySql.Data.MySqlClient":
                    conn = new MySqlConnection(dbSet.ConnectionString);
                    break;
                default:
                    throw new ConfigurationErrorsException(
                        $"dbName={dbName}的ProviderName={dbSet.ProviderName}，无法实例化该种类型的连接。");
            }
            return conn;
        }

        /// <summary>
        /// 获取Db配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static ConnectionStringSettings GetDbSetting(string key)
        {
            return ConfigurationManager.ConnectionStrings[key];
        }
    }
}