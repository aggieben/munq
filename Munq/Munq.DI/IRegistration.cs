using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.DI
{
    public interface IRegistration<TType> where TType:class
    {
        IRegistration<TType> WithLifetimeManager(ILifetimeManager<TType> ltm);
    }

    public interface IRegistrationKey
    {
        bool Equals(object obj);
        int GetHashCode();
    }
}
