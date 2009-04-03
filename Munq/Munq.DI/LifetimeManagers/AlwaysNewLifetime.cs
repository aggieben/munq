using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.DI.LifetimeManagers
{
    public class AlwaysNewLifetime : ILifetimeManager 
    {
        #region ILifetimeManager Members

        public object GetInstance(Container container, IRegistration reg)
        {
            return reg.CreateInstance(container);
        }

        #endregion
    }
}
