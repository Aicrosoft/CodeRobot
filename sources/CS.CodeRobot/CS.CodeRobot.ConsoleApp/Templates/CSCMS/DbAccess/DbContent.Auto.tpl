@using CS.eRobot.Generator
@using SchemaExplorer
@{
	var p = @Model.Project;
	var tbs = @Model.Tables;
	var style = @Model.Style;
	var dbSetting = @Model.DbSetting;
}
using System.Data.Entity;
using @CodeHelper.GetNamespace(p,ModelStyleType.Model,dbSetting);

//-------------------------------------------------------------------------------------------
// 以下代码为自动生成，请勿修改。
// autogeneration @DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") powered by atwind@cszi.com 
//-------------------------------------------------------------------------------------------


namespace @CodeHelper.GetNamespace(p,style,dbSetting)
{

    partial class @dbSetting.DbContextName : DbContext
    {

        @CodeHelper.GetEfMapTables(tbs)


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //各自的配置都分散到不同的模型配置中
            @CodeHelper.GetEntityConfigurations(tbs)


            base.OnModelCreating(modelBuilder);
        }
    }
}