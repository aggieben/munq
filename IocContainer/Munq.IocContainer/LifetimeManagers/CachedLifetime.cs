using System;
using System.Web;
using System.Web.Caching;

namespace Munq.LifetimeManagers
{
    public class CachedLifetime : ILifetimeManager 
    {
        private CacheDependency          _dependencies;
        private DateTime                 _absoluteExpiration = Cache.NoAbsoluteExpiration;
        private TimeSpan                 _slidingExpiration  = Cache.NoSlidingExpiration;
        private CacheItemPriority        _priority           = CacheItemPriority.Default;
        private CacheItemRemovedCallback _onRemoveCallback;

        #region ILifetimeManager Members
        /// <summary>
        /// Gets the instance from cache, if available, otherwise creates a new
        /// instance and caches it.
        /// </summary>
        /// <param name="creator">The creator (registration) to create a new instance.</param>
        /// <returns>The instance.</returns>
        public object GetInstance(IInstanceCreator creator)
        {
            Cache cache = HttpRuntime.Cache;

            object instance = cache[creator.Key];
            if (instance == null)
            {
                instance = creator.CreateInstance(ContainerCaching.InstanceNotCachedInContainer);
                
                cache.Insert(creator.Key, instance, _dependencies, _absoluteExpiration,
                                _slidingExpiration, _priority, _onRemoveCallback);
            }

            return instance;
        }

        /// <summary>
        /// Invalidates the cached value.
        /// </summary>
        /// <param name="registration">The Registration which is having its value invalidated</param>
        public void InvalidateInstanceCache(IRegistration registration)
        {
            Cache cache = HttpRuntime.Cache;
            cache.Remove(registration.Key);
        }

        #endregion
 
        /// <summary>
        /// Sets the Cache Dependencies for this LifetimeManager.
        /// </summary>
        /// <param name="dependencies">The CacheDependencies.</param>
        /// <returns>The CachedLifetime (allows chaining).</returns>
        public CachedLifetime IsDependentOn(CacheDependency dependencies)
        {
            _dependencies = dependencies;
            return this;
        }

        /// <summary>
        /// Sets the absolute time when the cached value expires.
        /// </summary>
        /// <param name="absoluteExpiration">The date/time when the item expires.</param>
        /// <returns>The CachedLifetime (allows chaining).</returns>
        public CachedLifetime ExpiresOn(DateTime absoluteExpiration)
        {
            if (absoluteExpiration != Cache.NoAbsoluteExpiration)
                _slidingExpiration  = Cache.NoSlidingExpiration;

            _absoluteExpiration     = absoluteExpiration;
            return this;
        }
        
        /// <summary>
        /// Sets the duration the cached item will remain valid.
        /// </summary>
        /// <param name="slidingExpiration">The duration.</param>
        /// <returns>The CachedLifetime (allows chaining).</returns>
        public CachedLifetime ExpiresAfterNotAccessedFor(TimeSpan slidingExpiration)
        {
            if (slidingExpiration  != Cache.NoSlidingExpiration)
                _absoluteExpiration = Cache.NoAbsoluteExpiration;

            _slidingExpiration      = slidingExpiration;
            return this;
        }

        /// <summary>
        /// Sets the priority of the item in the cache.
        /// </summary>
        /// <param name="priority">The priority.</param>
        /// <returns>The CachedLifetime (allows chaining).</returns>

        public CachedLifetime WithPriority(CacheItemPriority priority)
        {
            _priority = priority;
            return this;
        }

        /// <summary>
        /// Sets a callback method for when an item is removed (expires).
        /// </summary>
        /// <param name="onRemoveCallback">The callback method.</param>
        /// <returns>The CachedLifetime (allows chaining).</returns>
        public CachedLifetime CallbackOnRemoval(CacheItemRemovedCallback onRemoveCallback)
        {
            _onRemoveCallback = onRemoveCallback;
            return this;
        }
    }
}
