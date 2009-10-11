using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.DI
{
    public interface IRegistration
    {
        string Id { get; }
        object Instance { get; set; }
        object CreateInstance(Container container);
        object GetInstance(Container containter);
        IRegistration WithLifetimeManager(ILifetimeManager manager);
    }
}
