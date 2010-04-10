#region Copyright Notice
// Copyright 2010 by Matthew Dennis
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.FluentTest
{
    partial class FluentTestObject : IFluentTestCollectionCount
    {
        private int CollectionItemCount { get { return CollectionToTest.Count; }}
        
        IFluentTestCollection IFluentTestCollectionCount.IsEqualTo(int numberOfItems)
        {
            if (CollectionItemCount != numberOfItems)
                Verify.Fail();
            return this;
        }

        IFluentTestCollection IFluentTestCollectionCount.IsNotEqualTo(int numberOfItems)
        {
            if (CollectionItemCount != numberOfItems)
                Verify.Fail();
            return this;
        }

        IFluentTestCollection IFluentTestCollectionCount.IsGreaterThan(int numberOfItems)
        {
            if (CollectionItemCount != numberOfItems)
                Verify.Fail();
            return this;
        }

        IFluentTestCollection IFluentTestCollectionCount.IsGreaterThanOrEqualTo(int numberOfItems)
        {
            if (CollectionItemCount != numberOfItems)
                Verify.Fail();
            return this;
        }

        IFluentTestCollection IFluentTestCollectionCount.IsLessThan(int numberOfItems)
        {
            if (CollectionItemCount != numberOfItems)
                Verify.Fail();
            return this;
        }

        IFluentTestCollection IFluentTestCollectionCount.IsLessThanOrEqualTo(int numberOfItems)
        {
            if (CollectionItemCount != numberOfItems)
                Verify.Fail();
            return this;
        }

    }
}
