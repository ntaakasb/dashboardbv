using System;

namespace OsCoreApplication.Libraries.Memcached
{
    public interface ICached :IDisposable
    {
        /// <summary>
        /// Retreives data from cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// Inserts an item into the cached with a key cached to references its location 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheMode"></param>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expired"></param>
        void Set<T>(CacheMode cacheMode,string key, T obj, TimeSpan expired);

        /// <summary>
        /// Remove data from cache
        /// </summary>
        /// <param name="key"></param>
        void RemoveCached(string key);

        /// <summary>
        /// Remove all data from cache
        /// </summary>
        void RemoveAllCached();
    }
}
