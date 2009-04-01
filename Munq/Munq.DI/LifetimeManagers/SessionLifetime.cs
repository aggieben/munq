using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace Munq.DI.LifetimeManagers
{
    public class SessionLifetime<TType> : ILifetimeManager<TType> where TType : class
    {
        HttpSessionStateBase _session = null;
        #region ILifetimeManager<TType> Members

        public TType GetInstance(Container container, Registration<TType> reg)
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

            TType instance = (TType)session[reg.ID.ToString()];
            if (instance == null)
            {
                instance = reg.Factory(container);
                session[reg.ID.ToString()] = instance;
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
