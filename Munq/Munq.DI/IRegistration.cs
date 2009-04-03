using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.DI
{
    public interface IRegistration
    {
        string ID { get; }
        object Instance { get; set; }
        object CreateInstance(Container container);

        IRegistration WithLifetimeManager(ILifetimeManager ltm);
    }

    public interface IRegistrationKey
    {
        bool Equals(object obj);
        int GetHashCode();
    }
}
