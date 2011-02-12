using System.Web;

namespace Munq.LifetimeManagers
{
    public class SessionLifetime : ILifetimeManager
    {
        private HttpSessionStateBase testSession;
		private object _lock = new object();

        /// <summary>
        /// Gets the Session.
        /// </summary>
        private HttpSessionStateBase Session
        {
            get
            {
                HttpSessionStateBase session = (HttpContext.Current != null)
                                ? new HttpSessionStateWrapper(HttpContext.Current.Session)
                                : testSession;
                return session;
            }
        }

        #region ILifetimeManager Members
        /// <summary>
        /// Gets the instance from the Session, if available, otherwise creates a new
        /// instance and stores in the Session.
        /// </summary>
        /// <param name="creator">The creator (registration) to create a new instance.</param>
        /// <returns>The instance.</returns>
        public object GetInstance(IRegistration creator)
        {
            object instance = Session[creator.Key];
            if (instance == null)
            {
				lock (_lock)
				{
					instance = Session[creator.Key];
					if (instance == null)
					{
						instance = creator.CreateInstance();
						Session[creator.Key] = instance;
					}
                }
            }

            return instance;
        }

        /// <summary>
        /// Invalidates the cached value.
        /// </summary>
        /// <param name="registration">The Registration which is having its value invalidated</param>
        public void InvalidateInstanceCache(IRegistration registration)
        {
            Session.Remove(registration.Key);
        }

        #endregion

        // only used for testing.  Has no effect when in web application
        public void SetContext(HttpContextBase context)
        {
            testSession = context.Session;
        }
    }
}
