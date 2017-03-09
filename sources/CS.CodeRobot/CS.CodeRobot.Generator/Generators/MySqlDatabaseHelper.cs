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
            return table.Indexes.FirstOrDefault(x => x.IndexType == "PRIMARY")?.Columns;
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