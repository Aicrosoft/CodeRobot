using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CS.Extension;
using DatabaseSchemaReader.DataSchema;

namespace CS.CodeRobot.Generators
{
    public class MySqlDatabaseHelper
    {

        public string ToDotNetType(DatabaseColumn column)
        {
            return column.ToDotNetType();
        }

        public bool IsNotEnumType(DatabaseColumn column)
        {
            return !column.IsEnumType();
        }

        public string GetPks(DatabaseTable table)
        {
            return table.GetPks();
        }

        public string GetPkQueryParams(DatabaseTable table)
        {
            return table.GetPkQueryParams();
        }

        public string GetQueryExpressionByPk(DatabaseTable table)
        {
            return table.GetQueryExpressionByPk();
        }

        public string GetProxyKeyHold(DatabaseTable table)
        {
            return table.GetProxyKeyHold();
        }

        public string GetProxyKey(DatabaseTable table)
        {
            return table.GetProxyKey();
        }

        public string GetProxyKey(DatabaseTable table, string modelParamName)
        {
            return table.GetProxyKey(modelParamName);
        }

        public string GetPkExpress(DatabaseTable table)
        {
            return table.GetPkExpress();
        }
        public string GetPkExpress(DatabaseTable table,string entityName)
        {
            return table.GetPkExpress(entityName);
        }


        /// <summary>
        /// 生成枚举类型
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public string CreateEnumTypes(DatabaseTable table)
        {
            var temp = @"
    /// <summary>
    /// 
    /// </summary>
    public enum {0}:{1}
    {{
        Unknown = 0,

    }}
";
            var sb = new StringBuilder();
            foreach (var column in table.Columns)
            {
                if (column.IsEnumType())
                {
                    sb.AppendFormat(temp, $"{column.Table.Name}{column.Name}Type", column.ToDotNetType());
                }
            }
            return sb.ToString();
        }


    }

    public static class MySqlDatabaseHelperExt
    {

        /// <summary>
        /// 获取主键表达式
        /// </summary>
        /// <param name="table"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public static string GetPkExpress(this DatabaseTable table, string entityName = null)
        {
            var pks = table.GetPrimaryKeyNames();
            var on = entityName == null ? "" : $"{entityName}.";
            return string.Join(" && ", pks.Select(pk => $"x.{pk} == {on}{pk}").ToArray());
        }

        /// <summary>
        /// 获得代理类的查询键
        /// </summary>
        /// <param name="table"></param>
        /// <param name="modelParamName">模型参数名，当名字不为null时会带上模型的参数名</param>
        /// <returns></returns>
        public static string GetProxyKey(this DatabaseTable table, string modelParamName = null)
        {
            var pks = table.GetPrimaryKeyNames();
            var ids =  string.Join(", ", string.IsNullOrWhiteSpace(modelParamName) ? pks.Select(x => x.ToLower()) : pks.Select(x => $"{modelParamName}.{x}"));
            return ids;
        }

        public static string GetProxyKeyHold(this DatabaseTable table)
        {
            var pks = table.GetPrimaryKeyNames();
            var list = new List<string> { $"{table.Name}" };
            var index = 0;
            foreach (var pk in pks)
            {
                list.Add($"{{{index}}}");
                index++;
            }
            return string.Join("_", list.ToArray());
        }


        public static string GetPks(this DatabaseTable table)
        {
            var pks = table.FindPrimaryKeys();
            return pks.Count == 1 ? $"{string.Join(" , ", pks.Select(pk => $"x.{pk.Name}").ToArray())}" : $"new {{ {string.Join(" , ", pks.Select(pk => $"x.{pk.Name}").ToArray())} }}";
        }

        public static string GetPkQueryParams(this DatabaseTable table)
        {
            if (!table.HasPrimaryKey()) throw new OperationCanceledException($"{table.Name}没有主键，请先设定主键后再进行生成。");
            var pks = table.FindPrimaryKeys();
            var rst = string.Join(",", (from pk in pks select pk into col where col != null select $"{col.ToDotNetType()} {col.Name.ToLower()}").ToArray());
            return rst;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GetQueryExpressionByPk(this DatabaseTable table)
        {
            var pks = table.FindPrimaryKeys();
            var rst = $"x => {string.Join(" && ", pks.Select(pk => $"x.{pk.Name} == {pk.Name.ToLower()}").ToArray())}";
            return rst;
        }

        /// <summary>
        /// 是否是枚举类型
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static bool IsEnumType(this DatabaseColumn column)
        {
            //当不为空且是可枚举类型时
            return column.Nullable && !column.IsPrimaryKey() && Regex.IsMatch(column.ToDotNetType(), "int|short|long|uint|ushort|ulong", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public static bool IsPrimaryKey(this DatabaseColumn column)
        {
            return column.Table.FindPrimaryKeys().Any(x => x.Name == column.Name);
        }

        public static List<DatabaseColumn> FindPrimaryKeys(this DatabaseTable table)
        {
            var indexCols=  table.GetPrimaryKeyNames();
            return table.Columns.Where(x => indexCols.Contains(x.Name)).ToList();
        }

        public static string[] GetPrimaryKeyNames(this DatabaseTable table)
        {
           return table.Indexes.FirstOrDefault(x => x.IndexType == "PRIMARY")?.Columns.Select(x => x.Name).ToArray();
        }

        public static bool HasPrimaryKey(this DatabaseTable table)
        {
            return table.FindPrimaryKeys().Count > 0;
        }



        #region MySqlDatabase Type Convert

        public static string ToDotNetType(this DatabaseColumn column)
        {
            switch (column.DbDataType.ToLower())
            {
                case "nvarchar":
                case "nchar":
                case "ntext":
                case "varchar":
                case "char":
                case "text":
                case "longtext":
                    return "string";
                case "datetime":
                case "timestamp":
                case "smalldatetime":
                    return $"DateTime{column.Nullable.If("?")}";
                case "tinyint":
                    return "byte";
                case "smallint":
                    return " short";
                case "bigint":
                    return "long";
                case "binary":
                case "image":
                case "varbinary":
                    return "byte[]";
                case "int":
                    return "int";
                case "bit":
                    return "bool";
                case "decimal":
                case "money":
                case "numeric":
                case "smallmoney":
                    return "decimal";
                case "float":
                    return "double";
                case "Variant":
                    return "object";
                case "uniqueidentifier":
                    return "Guid";


                default:
                    return column.DbDataType;
            }


        }

        /*

real 　　　　　　　　　System.Single 
         
         */


        //// 获取指定列对应的C#数据类型
        //public static string GetCSharpVariableType(ColumnSchema column)
        //{
        //    if (column.Name.EndsWith("TypeCode")) return column.Name;

        //    switch (column.DataType)
        //    {
        //        case DbType.AnsiString: return "string";
        //        case DbType.AnsiStringFixedLength: return "string";
        //        case DbType.Binary: return "byte[]";
        //        case DbType.Boolean: return "bool";
        //        case DbType.Byte: return "byte";
        //        case DbType.Currency: return "decimal";
        //        case DbType.Date: return "DateTime";
        //        case DbType.DateTime: return "DateTime";
        //        case DbType.Decimal: return "decimal";
        //        case DbType.Double: return "double";
        //        case DbType.Guid: return "Guid";
        //        case DbType.Int16: return "short";
        //        case DbType.Int32: return "int";
        //        case DbType.Int64: return "long";
        //        case DbType.Object: return "object";
        //        case DbType.SByte: return "sbyte";
        //        case DbType.Single: return "float";
        //        case DbType.String: return "string";
        //        case DbType.StringFixedLength: return "string";
        //        case DbType.Time: return "TimeSpan";
        //        case DbType.UInt16: return "ushort";
        //        case DbType.UInt32: return "uint";
        //        case DbType.UInt64: return "ulong";
        //        case DbType.VarNumeric: return "decimal";
        //        default:
        //            {
        //                return "__UNKNOWN__" + column.NativeType;
        //            }
        //    }
        //}

        #endregion

    }

}