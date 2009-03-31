using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.DI
{
    public class Registration<TType> : IRegistration
    {
        internal Func<Container, TType> Factory;

        public Registration(Func<Container, TType> f)
        {
            Factory = f;
        }
    }

    public class RegistrationKey<TType> : IRegistrationKey
    {
        public override bool Equals(object obj)
        {
            return obj is RegistrationKey<TType>;
        }

        public override int GetHashCode()
        {
            return typeof(TType).GetHashCode();
        }
    }
}
