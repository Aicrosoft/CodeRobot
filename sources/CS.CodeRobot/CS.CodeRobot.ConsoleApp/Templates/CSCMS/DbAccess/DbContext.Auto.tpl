using System.Data.Entity;
using {{pi.Namespace}}.Model.{{dbSetting.Name}};

//-------------------------------------------------------------------------------------------
// 以下代码为自动生成，请勿修改。
// autogeneration {{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}} powered by atwind@cszi.com 
//-------------------------------------------------------------------------------------------


namespace {{pi.Namespace}}.{{sub}}.{{dbSetting.Name}}
{

    partial class {{dbSetting.DbContextName}} : DbContext
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