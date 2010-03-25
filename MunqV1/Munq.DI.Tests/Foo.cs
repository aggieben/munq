using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.DI.Tests
{
    public interface IFoo
    {
        string Name { get; }
    }

    public class Foo1: IFoo
    {
        #region IFoo Members

        public string Name
        {
            get { return "Foo1"; }
        }

        #endregion
    }

    public class Foo2 : IFoo
    {
        #region IFoo Members

        public string Name
        {
            get { return "Foo2"; }
        }

        #endregion
    }

    public interface IBar
    {
    }

    public class Bar1 : IBar
    {
    }

    public class Bar2 : IBar
    {
    }
}
