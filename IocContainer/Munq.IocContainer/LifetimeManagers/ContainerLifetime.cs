namespace Munq.LifetimeManagers
{
    public class ContainerLifetime : ILifetimeManager
    {
        #region ILifetimeManager Members
        /// <summary>
        /// Gets the instance from the container instance cache, if available, otherwise creates a new
        /// instance and caches it.
        /// </summary>
        /// <param name="creator">The creator (registration) to create a new instance.</param>
        /// <returns>The instance.</returns>
        public object GetInstance(IInstanceCreator creator)
        {
            return creator.CreateInstance(ContainerCaching.InstanceCachedInContainer);
        }

        /// <summary>
        /// Invalidates the cached value.
        /// </summary>
        /// <param name="registration">The Registration which is having its value invalidated</param>
        public void InvalidateInstanceCache(IRegistration ireg)
        {
            var reg = ireg as Registration;
            if (reg != null)
                reg.Instance = null;
        }

        #endregion

    }
}
