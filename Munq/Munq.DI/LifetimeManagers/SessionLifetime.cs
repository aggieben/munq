using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace Munq.DI.LifetimeManagers
{
    public class SessionLifetime : ILifetimeManager
    {
        private HttpSessionStateBase testSession;
        #region ILifetimeManager Members

        public object GetInstance(Container container, IRegistration reg)
        {
            HttpSessionStateBase session;
            if (System.Web.HttpContext.Current != null)
            {
                session = new HttpSessionStateWrapper(HttpContext.Current.Session);
            }
            else
            {
                session = this.testSession;
            }

            object instance = session[reg.Id];
            if (instance == null)
            {
                instance = reg.CreateInstance(container);
                session[reg.Id] = instance;
            }

            return instance;
        }

        #endregion

        // only used for testing.  Has no effect when in web application
        public void SetContext(HttpContextBase context)
        {
            this.testSession = context.Session;
        }
    }
}
