using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.DI.LifetimeManagers
{
    public class ContainerLifetime : ILifetimeManager
    {
        #region ILifetimeManager Members

        public object GetInstance(Container container, IRegistration reg)
        {
            if (reg.Instance == null)
                reg.Instance = reg.CreateInstance(container);

            return reg.Instance;
        }

        #endregion
    }
}
