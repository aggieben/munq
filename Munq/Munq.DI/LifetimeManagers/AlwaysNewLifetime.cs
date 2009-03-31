using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.DI.LifetimeManagers
{
    public class AlwaysNewLifetime<TType> : ILifetimeManager<TType> where TType:class
    {
        #region ILifetimeManager<TType> Members

        public TType GetInstance(Container container, Registration<TType> reg)
        {
            return reg.Factory(container);
        }

        #endregion
    }
}
