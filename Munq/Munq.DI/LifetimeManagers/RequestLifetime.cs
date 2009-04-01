using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace Munq.DI.LifetimeManagers
{
    public class RequestLifetime<TType> : ILifetimeManager<TType> where TType : class
    {
        HttpContextBase _context = null;
        #region ILifetimeManager<TType> Members

        public TType GetInstance(Container container, Registration<TType> reg)
        {
            HttpContextBase context;
            if (System.Web.HttpContext.Current != null)
            {
                context = new HttpContextWrapper(HttpContext.Current);
            }
            else
            {
                context = _context;
            }

            TType instance = (TType)context.Items[reg.ID];
            if (instance == null)
            {
                instance = reg.Factory(container);
                context.Items[reg.ID] = instance;
            }
            return instance;
        }

        #endregion

        // only used for testing.  Has no effect when in web application
        public void SetContext(HttpContextBase context)
        {
            _context = context;
        }
    }
}
