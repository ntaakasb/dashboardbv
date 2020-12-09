using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsCoreApplication.Libraries.Memcached
{
    public enum CacheMode
    {
        /// <summary>
        /// Add new data to cache
        /// </summary>
        Add,
        /// <summary>
        /// Add to cache, overwrite if data already exists
        /// </summary>
        Set
    }
}
