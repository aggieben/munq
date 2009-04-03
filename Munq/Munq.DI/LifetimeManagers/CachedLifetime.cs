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
	    CacheDependency _dependencies = null;
	    DateTime _absoluteExpiration = Cache.NoAbsoluteExpiration;
	    TimeSpan _slidingExpiration = Cache.NoSlidingExpiration;
	    CacheItemPriority _priority = CacheItemPriority.Default;
	    CacheItemRemovedCallback _onRemoveCallback = null;

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

            object instance = cache[reg.ID];
            if (instance == null)
            {
                instance = reg.CreateInstance(container);
                cache.Insert(reg.ID, instance,_dependencies, _absoluteExpiration, _slidingExpiration, _priority, _onRemoveCallback);
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
                _slidingExpiration = Cache.NoSlidingExpiration;

            _absoluteExpiration = absoluteExpiration;
            return this;
        }
        
        public CachedLifetime ExpiresAfterNotAcessedFor(TimeSpan slidingExpiration)
        {
            if (slidingExpiration != Cache.NoSlidingExpiration)
                _absoluteExpiration = Cache.NoAbsoluteExpiration;

            _slidingExpiration = slidingExpiration;
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
