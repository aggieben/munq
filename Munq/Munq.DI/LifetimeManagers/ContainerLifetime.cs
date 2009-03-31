using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.DI.LifetimeManagers
{
    public class ContainerLifetime<TType> : ILifetimeManager<TType> where TType:class
    {
        #region ILifetimeManager<TType> Members

        public TType GetInstance(Container container, Registration<TType> reg)
        {
            if (reg.Instance == null)
            {
                reg.Instance = reg.Factory(container);
            }
            return reg.Instance;
        }

        #endregion
    }
}
