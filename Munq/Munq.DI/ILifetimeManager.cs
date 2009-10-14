using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.DI
{
    public interface ILifetimeManager
    {
        object	GetInstance(Container container, Registration reg);
    }
}
