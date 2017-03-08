using DatabaseSchemaReader.DataSchema;
using DotLiquid;

namespace CS.CodeRobot.TemplateEngine
{
    public class DatabaseTableDrop:Drop
    {
        public DatabaseTable Table { get; set; }


        public DatabaseTableDrop(DatabaseTable dt)
        {
            Table = dt;
        }
    }
}