$set(modelClassName = table.Name)$set(clsDao = table.Name + model.Suffix)$set(configClass = table.Name + "Configuration")
using System.Data.Entity.ModelConfiguration;
using ${pi.Namespace}.Model.${dbSetting.Name};

namespace ${model.NameSpace}.${dbSetting.Name}
{
    /// <summary>
    /// $table.Name : $table.Description
    /// </summary>
    public partial class $clsDao
    {
       
    }


    
    internal class $configClass : EntityTypeConfiguration<$modelClassName>
    {
        public ${configClass}()
        {

            ToTable("$modelClassName")
            .HasKey(x => $helper.GetPks(table) )
            ;

            //HasKey(t => t.CId).Property(t => t.CId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //Property(t => t.CName).IsRequired().HasMaxLength(50);
            
        }
    }


}