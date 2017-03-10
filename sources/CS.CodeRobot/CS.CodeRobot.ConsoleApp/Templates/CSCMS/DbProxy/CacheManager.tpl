using System;

namespace ${model.Namespace}
{
    /// <summary>
    /// 缓存管理器
    /// </summary>
    public abstract class CacheManager
    {
        /// <summary>
        /// 取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract T Get<T>(string key);
        /// <summary>
        /// 设定缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        public abstract void Set<T>(string key, T t);
        /// <summary>
        /// 设定缓存及过期时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="key"></param>
        /// <param name="expire"></param>
        public abstract void Set<T>(T t, string key, DateTime expire);
        /// <summary>
        /// 设定缓存及有效时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="key"></param>
        /// <param name="validFor"></param>
        public abstract void Set<T>(T t, string key, TimeSpan validFor);
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        public abstract void Delete(string key);


        public T Get<T>(T t, Func<T, string> keyFunc)
        {
            return Get<T>(keyFunc(t));
        }

        public void Set<T>(T t, Func<T, string> keyFunc)
        {
            Set(keyFunc(t), t);
        }

        public void Set<T>(T t, Func<T, string> keyFunc, DateTime expire)
        {
            Set(t, keyFunc(t), expire);
        }

        public void Set<T>(T t, Func<T, string> keyFunc, TimeSpan validFor)
        {
            Set(t, keyFunc(t), validFor);
        }

        public void Delete<T>(T t, Func<T, string> keyFunc)
        {
            Delete(keyFunc(t));
        }
    }
}