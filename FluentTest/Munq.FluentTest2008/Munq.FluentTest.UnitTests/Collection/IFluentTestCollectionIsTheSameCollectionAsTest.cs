using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq.FluentTest;
using System.Collections.Generic;

namespace Munq.FluentTest.UnitTests
{  
    /// <summary>
    ///This is a test class for IFluentTestTest and is intended
    ///to contain all IFluentTestTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IFluentTestCollectionIsTheSameCollectionAsTest
    {

        #region IsTheSameCollectionAs
        [TestMethod]
        public void IsSameCollectionPassesIfSameCollection()
        {
            var testCollection = new List<string>();
            Verify.That(testCollection).IsACollection().IsTheSameCollectionAs(testCollection);          
        }
        
        [TestMethod]
        public void IsSameCollectionFailsIfNull()
        {
            var testCollection = new List<string>();
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                ()=>Verify.That(testCollection).IsACollection().IsTheSameCollectionAs(null)
            );
        }
        
        [TestMethod]
        public void IsSameCollectionFailsIfDifferent()
        {
            var testCollection = new List<string>();
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testCollection).IsACollection().IsTheSameCollectionAs(new List<string>())
            );
        }

        #endregion

        #region IsNotTheSameCollectionAs
        [TestMethod]
        public void IsNotSameCollectionPassesIfDifferent()
        {
            var testCollection = new List<string>();
            Verify.That(testCollection).IsACollection().IsNotTheSameCollectionAs(new List<string>());
        }

        [TestMethod]
        public void IsNotSameCollectionFailsIfNull()
        {
            var testCollection = new List<string>();
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testCollection).IsACollection().IsNotTheSameCollectionAs(null)
            );
        }

        [TestMethod]
        public void IsNotSameCollectionFailsIfSame()
        {
            var testCollection = new List<string>();
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testCollection).IsACollection().IsNotTheSameCollectionAs(testCollection)
            );
        }

        #endregion
    }
}
