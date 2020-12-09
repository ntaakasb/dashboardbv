using System;

namespace OsCoreApplication.Libraries.Memcached
{
    public abstract class BaseCache
    {
        public abstract T Get<T>(string strKey);
        public abstract void Set<T>(CacheMode cacheMode,string strKey, T obj, TimeSpan expired);
        public abstract void RemoveCached(string key);
        public abstract void RemoveAllCached();
    }
}
