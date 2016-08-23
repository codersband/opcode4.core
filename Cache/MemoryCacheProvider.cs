using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using opcode4.core.Model.Interfaces.Cache;

namespace opcode4.core.Cache
{
    public class MemoryCacheProvider : ICacheProvider
    {
        private static readonly Lazy<ObjectCache> CacheObject = new Lazy<ObjectCache>(()=>MemoryCache.Default);

        public ObjectCache DefaultCache => CacheObject.Value;

        public void Add(string key, object value)
        {
            DefaultCache.Add(key, value, new CacheItemPolicy{Priority = CacheItemPriority.NotRemovable});
        }

        public void Add(string key, object value, TimeSpan validFor)
        {
            DefaultCache.Add(key, value, DateTime.Now + validFor);
        }

        public void Add(string key, object value, DateTime expiredAt)
        {
            DefaultCache.Add(key, value, expiredAt);
        }

        public void Set(string key, object value, DateTime expiredAt)
        {
            DefaultCache.Set(key, value, expiredAt);
        }

        public void Set(string key, object value, TimeSpan validFor)
        {
            DefaultCache.Set(key, value, DateTime.Now + validFor);
        }

        public void Add(string key, object value, string filePath)
        {
            var policy = new CacheItemPolicy
                {
                    Priority = CacheItemPriority.NotRemovable
                };
            policy.ChangeMonitors.Add(new HostFileChangeMonitor(new List<string>{filePath}));
            DefaultCache.Add(key, value, policy);
        }
        
        public void Add(string key, object value, DateTime expiredAt, string filePath)
        {
            var policy = new CacheItemPolicy
                {
                    Priority = CacheItemPriority.Default,
                    AbsoluteExpiration = expiredAt
                };
            policy.ChangeMonitors.Add(new HostFileChangeMonitor(new List<string> { filePath }));
            DefaultCache.Add(key, value, policy);
        }

        public void Touch(string key, DateTime expiredAt)
        {
            var a = DefaultCache.Get(key);
            if (a != null)
                DefaultCache.Set(key, a, expiredAt);
        }

        public void Touch(string key, TimeSpan nextExpiration)
        {
            var a = DefaultCache.Get(key);
            if (a != null)
                DefaultCache.Set(key, a, DateTime.Now + nextExpiration);
        }

        public T Get<T>(string key)
        {
            try
            {
                return (T)DefaultCache.Get(key);
            }
            catch
            { 
                return default (T);
            }
            
        }

        public object this[string key]
        {
            get { return DefaultCache[key]; }
            set { DefaultCache[key] = value; }
        }

        public void Remove(string key)
        {
            DefaultCache.Remove(key);
        }

        public void Dispose(){}
    }

    //callback = new CacheEntryRemovedCallback(this.MyCachedItemRemovedCallback);CacheEntryUpdateCallback;OnChangedCallback
    //policy.RemovedCallback = callback; 
}
