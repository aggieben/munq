#region Copyright Notice
// Copyright 2010 by Matthew Dennis
#endregion

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq.FluentTest;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Munq.FluentTest.UnitTests
{  
    /// <summary>
    ///This is a test class for IFluentTestTest and is intended
    ///to contain all IFluentTestTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IFluentTestCollectionAllItemsNotNullTest
    {

        #region  AllItemsNotNull
        [TestMethod]
        public void AllItemsNotNullPassedIfAllItemAreNotNull()
        {
            var testCollection = new List<object>(new object[]{"One", DateTime.Now, 1});
            Verify.That(testCollection).IsACollectionThat().AllItemsAreNotNull();          
        }
        
        [TestMethod]
        public void AllItemsNotNullFailsIfAnyItemIsNull()
        {
            var testCollection = new List<object>(new object[] { "One", null, "Three" });
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testCollection).IsACollectionThat().AllItemsAreNotNull()
            );
        }
        #endregion
    }
}
