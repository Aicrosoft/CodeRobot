@using CS.eRobot.Generator
@using SchemaExplorer
@{
	var p = @Model.Project;
	var tbs = @Model.Tables;
	var style = @Model.Style;
	var dbSetting = @Model.DbSetting;
    var ctor = string.Format("{0}()",@dbSetting.DbContentName);
}
using System.Data.Entity;

namespace  {{sub | namespace}} Namespace(p,style,dbSetting)
{
    internal partial class @dbSetting.DbContentName
    {
        public @ctor : base(DbConfigHelper.GetDbConnection("@dbSetting.DbConnName"), true)
        {
        }

    }
}