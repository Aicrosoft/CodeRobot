using ${pi.Namespace}.Model.${dbSetting.Name};
$set(clsName=table.Name+model.Suffix)

namespace ${model.NameSpace}.${dbSetting.Name}
{
    /// <summary>
    /// ${table.Name}
    /// <remarks>
    /// ${table.Description}
    /// </remarks>
    /// createtime : ${model.Now.ToString("yyyy-MM-dd HH:mm:ss") }
    /// </summary>
    public sealed partial class $clsName
    {
        //public ${clsName}()
        //{
        //    _cache = new MemcachedManager();
        //}

		//private readonly CacheManager _cache;

        //private readonly string KeyTemplate = "$keyTemplate";

        //public new $table.Name Get(${helper.GetPkQueryParams(table)})
        //{
        //    var key = string.Format(KeyTemplate,$helper.GetProxyKey(table));
        //    var o = _cache.Get<$table.Name>(key);
        //    if (o != null) return o;
        //    o = base.Get($helper.GetProxyKey(table));
        //    _cache.Set(key, o);
        //    return o;
        //}

        //public new int Delete($table.Name o)
        //{
        //    var key = string.Format(KeyTemplate, $helper.GetProxyKey(table,"o"));
        //    _cache.Delete(key);
        //    var val = base.Delete(o);
        //    return val;
        //}

        //public new int Update($table.Name o)
        //{
        //    var key = string.Format(KeyTemplate, $helper.GetProxyKey(table,"o"));
        //    _cache.Delete(key);
        //    var result = base.Update(o);
        //    return result;
        //}

        //public void ClearCache(${helper.GetPkQueryParams(table)})
        //{
        //    var key = string.Format(KeyTemplate, $helper.GetProxyKey(table));
        //    _cache.Delete(key);
        //}
    }

}