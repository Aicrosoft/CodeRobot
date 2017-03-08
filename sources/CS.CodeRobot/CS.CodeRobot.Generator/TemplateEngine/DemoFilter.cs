using DatabaseSchemaReader.DataSchema;
using DotLiquid;

namespace CS.CodeRobot.TemplateEngine
{
    public class DataTable : Drop
    {
        public DatabaseTable DbTable { get; set; }

        public DataTable(DatabaseTable dt)
        {
            DbTable = dt;
        }
    }

}