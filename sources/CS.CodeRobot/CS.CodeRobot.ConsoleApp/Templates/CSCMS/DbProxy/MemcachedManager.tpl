using System;
using Enyim.Caching;
using Enyim.Caching.Memcached;

namespace ${model.Namespace}
{
    /// <summary>
    /// MemcachedManager
    /// </summary>
    public class MemcachedManager : CacheManager
    {

        private static readonly MemcachedClient _mc; //TODO:是否改为池化对象？

        static MemcachedManager()
        {
            _mc = new MemcachedClient("memcached");
        }

        public override T Get<T>(string key)
        {
            var value = _mc.Get(key);
            if (value is T)
                return (T)value;
            return default(T);
        }

        public override void Set<T>(string key, T t)
        {
            _mc.Store(StoreMode.Set, key, t);
        }

        public override void Set<T>(T t, string key, DateTime expire)
        {
            _mc.Store(StoreMode.Set, key, t, expire);
        }
        public override void Set<T>(T t, string key, TimeSpan validFor)
        {
            _mc.Store(StoreMode.Set, key, t, validFor);
        }

        public override void Delete(string key)
        {
            _mc.Remove(key);
        }
    }
}