using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.DI
{
    public interface IRegistration
    {
        string Id { get; }
        IRegistration WithLifetimeManager(ILifetimeManager manager);
    }
}
