using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.DI
{
    public interface ILifetimeManager<TType> where TType : class
    {
        TType GetInstance(Container container, Registration<TType> reg);
    }
}
