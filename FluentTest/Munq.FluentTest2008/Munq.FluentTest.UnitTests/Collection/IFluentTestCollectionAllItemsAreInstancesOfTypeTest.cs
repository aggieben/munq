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
    public class IFluentTestCollectionAllItemsAreInstancesOfTypeTest
    {

        #region  AllItemsAreInstancesOfType
        [TestMethod]
        public void AllItemsAreInstancesOfTypePassedIfAllItemAreOfSpecifiedType()
        {
            var testCollection = new List<object>(new object[]{"One", "Two", "Three"});
            Verify.That(testCollection).IsACollectionThat().AllItemsAreInstancesOfType(typeof(string));          
        }
        
        [TestMethod]
        public void AllItemsAreInstancesOfTypeFailsIfAnyItemIsNotOfSpecifiedType()
        {
            var testCollection = new List<object>(new object[] { "One", DateTime.Today, "Three" });
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testCollection).IsACollectionThat().AllItemsAreInstancesOfType(typeof(string))
            ).AndHasAMessageThat().Contains("] should be an instance of type [String]");
        }
        
        [TestMethod]
        public void AllItemsAreInstancesOfTypePassesIfSpecifiedTypeIsNullAndAllItemsNull()
        {
            var testCollection = new List<object>(new object[] { null, null, null});
            Verify.That(testCollection).IsACollectionThat().AllItemsAreInstancesOfType(null);
        }

        [TestMethod]
        public void AllItemsAreInstancesOfTypeFailsIfSpecifiedTypeIsNullAndAllItemsNotNull()
        {
            var testCollection = new List<object>(new object[] { null, null, 1 });
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () =>Verify.That(testCollection).IsACollectionThat().AllItemsAreInstancesOfType(null)
            ).AndHasAMessageThat().Contains("should be an instance of type");
        }
        #endregion
    }
}
