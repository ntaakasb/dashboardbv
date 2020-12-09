using System;
using System.Configuration;
using System.Net;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;

namespace OsCoreApplication.Libraries.Memcached
{
    public class CachedAccess : BaseCache, ICached
    {
        private bool _disposed = false;
        private readonly MemcachedClient _cached;
        
        public CachedAccess()
        {
            var ipServer = ConfigurationManager.AppSettings["CachedIp"] ?? "127.0.0.1";
            var config = new MemcachedClientConfiguration();
            config.Servers.Add(new IPEndPoint(IPAddress.Parse(ipServer), 11211));
            _cached = new MemcachedClient(config);
        }

        /// <summary>
        /// Retreives data from cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public override T Get<T>(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            var obj = _cached.Get<T>(key);
            return obj;
        }

        /// <summary>
        /// Inserts an item into the cached with a key cached to references its location 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheMode"></param>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expired"></param>
        public override void Set<T>(CacheMode cacheMode, string key, T obj, TimeSpan expired)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            switch (cacheMode)
            {
                case CacheMode.Add:
                    _cached.Store(StoreMode.Add, key, obj, expired);
                    break;
                case CacheMode.Set:
                    _cached.Store(StoreMode.Set, key, obj, expired);
                        break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cacheMode), cacheMode, null);
            }
        }
        /// <summary>
        /// Remove data from cache
        /// </summary>
        /// <param name="key"></param>
        public override void RemoveCached(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            _cached.Remove(key);
        }

        /// <summary>
        /// Remove all data from cache
        /// </summary>
        public override void RemoveAllCached() => _cached.FlushAll();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    _cached?.Dispose();
            }
            _disposed = true;
        }
    }
}

