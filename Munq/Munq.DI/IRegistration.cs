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

        IRegistration WithLifetimeManager(ILifetimeManager manager);
    }

    public interface IRegistrationKey
    {
        bool Equals(object other);
        int GetHashCode();
    }
}
