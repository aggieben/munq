using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace Munq.DI.LifetimeManagers
{
    public class RequestLifetime : ILifetimeManager
    {
        HttpContextBase _context = null;
        #region ILifetimeManage Members

        public object GetInstance(Container container, IRegistration reg)
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

            object instance = context.Items[reg.ID];
            if (instance == null)
            {
                instance = reg.CreateInstance(container);
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
