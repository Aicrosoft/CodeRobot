using System.Data.Entity;

namespace ${model.NameSpace}.${dbSetting.Name}
{
    internal partial class ${dbSetting.DbContextName}
    {
        public ${dbSetting.DbContextName}() : base(DbConfigHelper.GetDbConnection("${dbSetting.DbConnName}"), true)
        {
        }

    }
}