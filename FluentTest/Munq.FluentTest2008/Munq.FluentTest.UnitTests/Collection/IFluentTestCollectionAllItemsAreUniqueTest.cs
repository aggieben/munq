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
    public class IFluentTestCollectionAllItemsAreUniqueTest
    {
        #region  AllItemsAreUnique
        [TestMethod]
        public void AllItemsAreUniquePassesIfAllItemsAreUnique()
        {
            var testCollection = new List<object>(new object[]{"One", DateTime.Now, 1});
            Verify.That(testCollection).IsACollectionThat().AllItemsAreUnique();          
        }
        
        [TestMethod]
        public void AllItemsAreUniqueFailsIfAnyIsDuplicated()
        {
            var testCollection = new List<object>(new object[] { "One", "Three", "Three" });
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testCollection).IsACollectionThat().AllItemsAreUnique()
            );
        }
        #endregion
    }
}
