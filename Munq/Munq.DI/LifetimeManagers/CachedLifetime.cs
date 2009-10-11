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
        private CacheDependency          _dependencies;
        private DateTime                 _absoluteExpiration = Cache.NoAbsoluteExpiration;
        private TimeSpan                 _slidingExpiration  = Cache.NoSlidingExpiration;
        private CacheItemPriority        _priority           = CacheItemPriority.Default;
        private CacheItemRemovedCallback _onRemoveCallback;

        #region ILifetimeManager Members
        public object GetInstance(Container container, IRegistration reg)
        {
            System.Web.Caching.Cache cache = HttpRuntime.Cache;

            object instance = cache[reg.Id];
            if (instance == null)
            {
                instance   = reg.CreateInstance(container);
                cache.Insert(reg.Id, instance, _dependencies, _absoluteExpiration, 
                                _slidingExpiration, _priority, _onRemoveCallback);
            }

            return instance;
        }

        #endregion

        public CachedLifetime IsDependentOn(CacheDependency dependencies)
        {
            _dependencies = dependencies;
            return this;
        }

        public CachedLifetime ExpiresOn(DateTime absoluteExpiration)
        {
            if (absoluteExpiration != Cache.NoAbsoluteExpiration)
                _slidingExpiration  = Cache.NoSlidingExpiration;

            _absoluteExpiration     = absoluteExpiration;
            return this;
        }
        
        public CachedLifetime ExpiresAfterNotAccessedFor(TimeSpan slidingExpiration)
        {
            if (slidingExpiration  != Cache.NoSlidingExpiration)
                _absoluteExpiration = Cache.NoAbsoluteExpiration;

            _slidingExpiration      = slidingExpiration;
            return this;
        }

        public CachedLifetime WithPriority(CacheItemPriority priority)
        {
            _priority = priority;
            return this;
        }

        public CachedLifetime CallbackOnRemoval(CacheItemRemovedCallback onRemoveCallback)
        {
            _onRemoveCallback = onRemoveCallback;
            return this;
        }
    }
}
