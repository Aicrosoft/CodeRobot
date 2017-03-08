using System.Data.Entity;

namespace {{pi.Namespace}}.{{sub}}.{{dbSetting.Name}}
{
    internal partial class {{dbSetting.DbContextName}}
    {
        public {{dbSetting.DbContextName}}() : base(DbConfigHelper.GetDbConnection("{{dbSetting.DbConnName}}"), true)
        {
        }

    }
}