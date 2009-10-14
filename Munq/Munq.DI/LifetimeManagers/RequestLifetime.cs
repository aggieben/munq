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
        private HttpContextBase testContext;
        #region ILifetimeManage Members

        public object GetInstance(Container container, Registration reg)
        {
            HttpContextBase context =(System.Web.HttpContext.Current != null)
                ? new HttpContextWrapper(HttpContext.Current)
                : this.testContext;

            object instance = context.Items[reg.Id];
            if (instance == null)
            {
                instance = reg.CreateInstance(container);
                context.Items[reg.Id] = instance;
            }

            return instance;
        }

        #endregion

        // only used for testing.  Has no effect when in web application
        public void SetContext(HttpContextBase context)
        {
            this.testContext = context;
        }
    }
}
