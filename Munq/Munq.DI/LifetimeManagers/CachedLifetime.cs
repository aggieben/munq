using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace Munq.DI.LifetimeManagers
{
    public class CachedLifetime : ILifetimeManager 
    {
        private CacheDependency dependencies;
        private DateTime absoluteExpiration = Cache.NoAbsoluteExpiration;
        private TimeSpan slidingExpiration = Cache.NoSlidingExpiration;
        private CacheItemPriority priority = CacheItemPriority.Default;
        private CacheItemRemovedCallback onRemoveCallback;

        #region ILifetimeManager Members
        public object GetInstance(Container container, IRegistration reg)
        {
            System.Web.Caching.Cache cache;
            if (System.Web.HttpContext.Current != null)
            {
                cache = HttpContext.Current.Cache;
            }
            else
            {
                cache = HttpRuntime.Cache;
            }

            object instance = cache[reg.Id];
            if (instance == null)
            {
                instance = reg.CreateInstance(container);
                cache.Insert(
                    reg.Id, 
                    instance, 
                    this.dependencies, 
                    this.absoluteExpiration, 
                    this.slidingExpiration,
                    this.priority, 
                    this.onRemoveCallback);
            }

            return instance;
        }

        #endregion

        public CachedLifetime IsDependentOn(CacheDependency dependencies)
        {
            this.dependencies = dependencies;
            return this;
        }

        public CachedLifetime ExpiresOn(DateTime absoluteExpiration)
        {
            if (absoluteExpiration != Cache.NoAbsoluteExpiration)
            {
                this.slidingExpiration = Cache.NoSlidingExpiration;
            }

            this.absoluteExpiration = absoluteExpiration;
            return this;
        }
        
        public CachedLifetime ExpiresAfterNotAccessedFor(TimeSpan slidingExpiration)
        {
            if (slidingExpiration != Cache.NoSlidingExpiration)
            {
                this.absoluteExpiration = Cache.NoAbsoluteExpiration;
            }

            this.slidingExpiration = slidingExpiration;
            return this;
        }

        public CachedLifetime WithPriority(CacheItemPriority priority)
        {
            this.priority = priority;
            return this;
        }

        public CachedLifetime CallbackOnRemoval(CacheItemRemovedCallback onRemoveCallback)
        {
            this.onRemoveCallback = onRemoveCallback;
            return this;
        }
    }
}
