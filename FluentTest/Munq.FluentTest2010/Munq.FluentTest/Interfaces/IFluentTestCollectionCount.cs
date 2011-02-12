#region Copyright Notice
// Copyright 2010 by Matthew Dennis
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.FluentTest
{
    public interface IFluentTestCollectionCount
    {
        IFluentTestCollection IsEqualTo(int numberOfItems);
        IFluentTestCollection IsNotEqualTo(int numberOfItems);
        IFluentTestCollection IsGreaterThan(int numberOfItems);
        IFluentTestCollection IsGreaterThanOrEqualTo(int numberOfItems);
        IFluentTestCollection IsLessThan(int numberOfItems);
        IFluentTestCollection IsLessThanOrEqualTo(int numberOfItems);
    }
}
