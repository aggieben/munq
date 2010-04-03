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
    public class IFluentTestCollectionIsAnInstanceOfTypeTest
    {

        #region IsAnInstanceOfType
        [TestMethod]
        public void IsAnInstanceOfTypePassesIfIsAnInstanceOf()
        {
            var testCollection = new List<string>();
            Verify.That(testCollection).IsACollection().IsAnInstanceOfType(typeof(List<string>));          
        }

        [TestMethod]
        public void IsAnInstanceOfTypeFailsIfNull()
        {
            var testCollection = new List<string>();
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testCollection).IsACollection().IsAnInstanceOfType(null)
            );
        }

        [TestMethod]
        public void IsAnInstanceOfTypeFailsIfDifferentType()
        {
            var testCollection = new List<string>();
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testCollection).IsACollection().IsAnInstanceOfType(typeof(Collection<int>))
            );
        }
        #endregion

        #region IsNotAnInstanceOfType
        [TestMethod]
        public void IsNotAnInstanceOfTypePassesIfIsNotAnInstanceOf()
        {
            var testCollection = new List<string>();
            Verify.That(testCollection).IsACollection().IsNotAnInstanceOfType(typeof(Collection<int>));
        }

        [TestMethod]
        public void IsNotAnInstanceOfTypeFailsIfNull()
        {
            var testCollection = new List<string>();
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testCollection).IsACollection().IsNotAnInstanceOfType(null)
            );
        }

        [TestMethod]
        public void IsNotAnInstanceOfTypeFailsIfDifferentType()
        {
            var testCollection = new List<string>();
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testCollection).IsACollection().IsNotAnInstanceOfType(typeof(List<string>))
            );
        }
        #endregion
    }
}
