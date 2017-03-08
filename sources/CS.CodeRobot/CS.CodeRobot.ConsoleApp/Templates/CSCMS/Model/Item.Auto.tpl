@using CS.eRobot.Generator
@using SchemaExplorer
@{
	var p = @Model.Project;
	var tb = @Model.Table;
	var style = @Model.Style;
	var dbSetting = @Model.DbSetting;
}
using System;

//-------------------------------------------------------------------------------------------
// 以下代码为自动生成，请勿修改，可枚举且可为Null的字段会在首次生成时在可编辑的部分类中
// autogeneration @DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") powered by atwind@cszi.com 
//-------------------------------------------------------------------------------------------

namespace @CodeHelper.GetNamespace(p,style,dbSetting)
{
    /// <summary>
    /// @tb.Description
    /// </summary>
    [Serializable]
    partial class @tb.Name
    {
        @CodeHelper.GetAutoModelProperties(tb)
    }
    
}