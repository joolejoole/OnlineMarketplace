using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace JoJo.Service
{
    public class Cache
    {
        public static Models.Total CacheFunction(string name, IEnumerable<Models.Total> obj)
        {
            ObjectCache cache = MemoryCache.Default;
            if (obj != null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddDays(30);
                cache.Set(name, obj, policy);
            }
            return (Models.Total)obj;
        }
    }
}
