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
        HttpSessionStateBase _session = null;
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
                session = _session;
            }

            object instance = session[reg.ID];
            if (instance == null)
            {
                instance = reg.CreateInstance(container);
                session[reg.ID] = instance;
            }
            return instance;
        }

        #endregion

        // only used for testing.  Has no effect when in web application
        public void SetContext(HttpContextBase context)
        {
            _session = context.Session;
        }
    }
}
