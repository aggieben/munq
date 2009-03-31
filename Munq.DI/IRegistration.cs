using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.DI
{
    public interface IRegistration
    {
    }

    public interface IRegistrationKey
    {
        bool Equals(object obj);
        int GetHashCode();
    }
}
