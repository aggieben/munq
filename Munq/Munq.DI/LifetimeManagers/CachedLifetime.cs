using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace Munq.DI.LifetimeManagers
{
    public class CachedLifetime<TType> : ILifetimeManager<TType> where TType : class
    {
        #region ILifetimeManager<TType> Members

        public TType GetInstance(Container container, Registration<TType> reg)
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

            TType instance = (TType)cache[reg.ID];
            if (instance == null)
            {
                instance = reg.Factory(container);
                cache[reg.ID] = instance;
            }
            return instance;
        }

        #endregion

    }
}
