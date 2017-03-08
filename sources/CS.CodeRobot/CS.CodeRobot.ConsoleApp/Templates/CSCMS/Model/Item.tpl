@using CS.eRobot.Generator
@using SchemaExplorer
@{
	var p = @Model.Project;
	var tb = @Model.Table;
	var style = @Model.Style;
	var dbSetting = @Model.DbSetting;
}
namespace @CodeHelper.GetNamespace(p,style,dbSetting)
{

{{ tbd | debug }}
    /// <summary>
    /// @tb.Description
    /// createtime : @DateTime.Now.ToString()
    /// </summary>
    public partial class {{ tbd.Table.Name }}
    {
        @CodeHelper.GetModelProperties(tb)
    }

    @CodeHelper.CreateEnumTypes(tb)
}