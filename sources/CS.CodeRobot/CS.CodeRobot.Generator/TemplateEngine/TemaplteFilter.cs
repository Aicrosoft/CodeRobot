using CS.CodeRobot.Generators;

namespace CS.CodeRobot.TemplateEngine
{
    /// <summary>
    /// 模板里的相关方法
    /// </summary>
    public class TemaplteFilter
    {

        //        public static IEncodedString GetEfMapTables(TableSchemaCollection tables)
        //        {
        //            var sb = new StringBuilder();
        //            var outP = @"
        //        /// <summary>
        //        /// {1}
        //        /// </summary>
        //        public virtual IDbSet<{0}> {0} {{ get; set; }}
        //";
        //            foreach (var table in tables)
        //            {
        //                sb.AppendFormat(outP, table.Name, table.Description);
        //            }
        //            return new RawString(sb.ToString());
        //        }

        //        public static IEncodedString GetEntityConfigurations(TableSchemaCollection tables)
        //        {
        //            var sb = new StringBuilder();
        //            var outP = @"
        //            modelBuilder.Configurations.Add(new {0}Configuration());
        //";
        //            foreach (var table in tables)
        //            {
        //                sb.AppendFormat(outP, table.Name);
        //            }
        //            return new RawString(sb.ToString());
        //        }

        //        public static string GetPks(TableSchema table)
        //        {
        //            var pks = table.PrimaryKey.MemberColumns;
        //            return pks.Count == 1 ? $"{string.Join(" , ", pks.Select(pk => $"x.{pk.Name}").ToArray())}" : $"new {{ {string.Join(" , ", pks.Select(pk => $"x.{pk.Name}").ToArray())} }}";
        //        }

        //        /// <summary>
        //        /// 获得代理类的查询键
        //        /// </summary>
        //        /// <param name="table"></param>
        //        /// <param name="modelName">当名字不为null时会带上模型的参数名</param>
        //        /// <returns></returns>
        //        public static string GetProxyKey(TableSchema table,string modelName = null)
        //        {
        //            var pks = table.PrimaryKey.MemberColumns;
        //            return string.Join(", ", modelName == null ? pks.Select(x => x.Name.ToLower()) : pks.Select(x => $"{modelName}.{x.Name}"));
        //        }

        //        /// <summary>
        //        /// 
        //        /// </summary>
        //        /// <param name="table"></param>
        //        /// <returns></returns>
        //        public static string GetProxyKeyHold(TableSchema table)
        //        {
        //            var pks = table.PrimaryKey.MemberColumns;
        //            var list = new List<string> {$"{table.Name}"};
        //            var index = 0;
        //            foreach (var pk in pks)
        //            {
        //                list.Add($"{{{index}}}");
        //                index++;
        //            }
        //            return string.Join("_", list.ToArray());
        //        }


        //        public static IEncodedString GetByPredicate(TableSchema table,string paramName)
        //        {
        //            return new RawString($"db.{table.Name}.Where({paramName})");
        //        }

        //        /// <summary>
        //        /// 
        //        /// </summary>
        //        /// <param name="table"></param>
        //        /// <returns></returns>
        //        public static IEncodedString GetEntityByPk(TableSchema table)
        //        {
        //            var pks = table.PrimaryKey.MemberColumns; 
        //            var rst =  $"db.{table.Name}.Where(x => {string.Join(" && ", pks.Select(pk => $"x.{pk.Name} == {pk.Name.ToLower()}").ToArray())}).FirstOrDefault()";
        //            return new RawString(rst);
        //        }

        //        /// <summary>
        //        /// 获取主键表达式
        //        /// </summary>
        //        /// <param name="table"></param>
        //        /// <param name="entityName"></param>
        //        /// <returns></returns>
        //        public static IEncodedString GetPkExpress(TableSchema table, string entityName = null)
        //        {
        //            var pks = table.PrimaryKey.MemberColumns;
        //            var on = entityName == null?"":$"{entityName}.";
        //            return new RawString(string.Join(" && ", pks.Select(pk => $"x.{pk.Name} == {on}{pk.Name}").ToArray()));
        //        }

        //        /// <summary>
        //        /// 
        //        /// </summary>
        //        /// <param name="table"></param>
        //        /// <returns></returns>
        //        public static IEncodedString GetEntityPkParams(TableSchema table)
        //        {
        //            if (!table.HasPrimaryKey) throw new OperationCanceledException($"{table.Name}没有主键，请先设定主键后再进行生成。");
        //            var pks = table.PrimaryKey.MemberColumns;
        //            var rst =  string.Join(",", (from pk in pks select pk into col where col != null select $"{col.ToCSharpType()} {col.Name.ToLower()}").ToArray());
        //            return new RawString(rst);
        //        }

        ///// <summary>
        ///// 当前的名字空间
        ///// </summary>
        ///// <param name="sub"></param>
        ///// <param name="project"></param>
        ///// <param name="dbSetting"></param>
        ///// <returns></returns>
        //public static string Namespace(string sub)
        //{
        //    var project = GeneratorFilter.ProjectInfo;
            
        //    return dbSetting == null ? $"{project.Namespace}.{sub}" : $"{project.Namespace}.{sub}.{dbSetting.Name}";
        //}



        //        /// <summary>
        //        /// 生成枚举类型
        //        /// </summary>
        //        /// <param name="table"></param>
        //        /// <returns></returns>
        //        public static IEncodedString CreateEnumTypes(TableSchema table)
        //        {
        //            var temp = @"
        //    /// <summary>
        //    /// 
        //    /// </summary>
        //    public enum {0}:{1}
        //    {{
        //        Unknown = 0,

        //    }}
        //";
        //            var sb = new StringBuilder();
        //            foreach (var column in table.Columns)
        //            {
        //                if (column.IsEnumType())
        //                {
        //                    sb.AppendFormat(temp, $"{column.Table.Name}{column.Name}Type",column.ToCSharpType());
        //                }
        //            }
        //            return new RawString(sb.ToString());
        //        }

        //        /// <summary>
        //        /// 获取模型的非全自动生成的属性
        //        /// </summary>
        //        /// <param name="table"></param>
        //        /// <returns></returns>
        //        public static IEncodedString GetModelProperties(TableSchema table)
        //        {
        //            var sb = new StringBuilder();
        //            foreach (var column in table.Columns)
        //            {
        //                if (column.IsEnumType())
        //                    sb.Append(GetModelProperty(column, true));
        //            }
        //            return new RawString(sb.ToString());
        //        }

        //        /// <summary>
        //        /// 获取模型对象全自动属性
        //        /// </summary>
        //        /// <param name="table"></param>
        //        /// <returns></returns>
        //        public static IEncodedString GetAutoModelProperties(TableSchema table)
        //        {
        //            var sb = new StringBuilder();
        //            foreach (var column in table.Columns)
        //            {
        //                if (!column.IsEnumType())
        //                    sb.Append(GetModelProperty(column));
        //            }
        //            return new RawString(sb.ToString());
        //        }

        //        /// <summary>
        //        /// 返回列转变后的属性
        //        /// </summary>
        //        /// <param name="col"></param>
        //        /// <param name="isEnumType">自定枚举类型</param>
        //        /// <returns></returns>
        //        public static string GetModelProperty(ColumnSchema col, bool isEnumType = false)
        //        {

        //            return $@"
        //        /// <summary>
        //        /// {col.Description}
        //        /// </summary>
        //        public {col.ToProperty(isEnumType)}" + @"{ get; set; }
        //";

        //        }



        //public IEncodedString Raw(string rawString)
        //{
        //    return new RawString(rawString);
        //}

    }
}