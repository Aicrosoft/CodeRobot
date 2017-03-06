using System.Data.Entity;
using ${pi.Namespace}.Model.${dbSetting.Name};

//-------------------------------------------------------------------------------------------
// 以下代码为自动生成，请勿修改。
// autogeneration ${model.Now.ToString("yyyy-MM-dd HH:mm:ss") } powered by atwind@cszi.com 
//-------------------------------------------------------------------------------------------

namespace ${model.NameSpace}.${dbSetting.Name}
{

    partial class ${dbSetting.DbContextName} : DbContext
    {

	
		$foreach(tb in dbSetting.Tables) 
		/// <summary>
        /// $tb.Name : $tb.Description
        /// </summary>
        public virtual IDbSet<$tb.Name> $tb.Name { get; set; }
		$end
		


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //各自的配置都分散到不同的模型配置中

			$foreach(tb in dbSetting.Tables) 						
			modelBuilder.Configurations.Add(new ${tb.Name}Configuration());
			$end

            base.OnModelCreating(modelBuilder);
			
        }
    }
}