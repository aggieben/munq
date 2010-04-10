#region Copyright Notice
// Copyright 2010 by Matthew Dennis
#endregion

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq.FluentTest;
using System.Text.RegularExpressions;

namespace Munq.FluentTest.UnitTests
{  
    /// <summary>
    ///This is a test class for IFluentTestTest and is intended
    ///to contain all IFluentTestTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IFluentTestObjectIsInstanceOfTest
    {
        #region IsAnInstanceOf
        /// <summary>
        ///A test for IsAnInstanceOfType
        ///</summary>
        [TestMethod()]
        public void IsAnInstanceOfTypeFailsForNull()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(null).IsAnInstanceOfType(typeof(Object))
            ).AndHasAMessageThat().Contains("[null] should be an instance of [Object]");
        }

        /// <summary>
        ///A test for IsAnInstanceOfType
        ///</summary>
        [TestMethod()]
        public void IsAnInstanceOfTypeFailsForDifferentType()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(new MyTestClass()).IsAnInstanceOfType(typeof(string))
            ).AndHasAMessageThat().Contains("[My Test Class instance] should be an instance of [String]");
        }
        /// <summary>
        ///A test for IsAnInstanceOfType
        ///</summary>
        [TestMethod()]
        public void IsAnInstanceOfTypePassesForSameType()
        {
            Verify.That(new MyTestClass()).IsAnInstanceOfType(typeof(MyTestClass));
        }

        #endregion

        #region IsNotAnInstanceOf

        /// <summary>
        ///A test for IsNotAnInstanceOfType
        ///</summary>
        [TestMethod()]
        public void IsNotAnInstanceOfTypeFailsForNull()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(null).IsNotAnInstanceOfType(null)
            ).AndHasAMessageThat().Contains("[null] should not be an instance of [null]");
        }

        /// <summary>
        ///A test for IsNotAnInstanceOfType
        ///</summary>
        [TestMethod()]
        public void IsNotAnInstanceOfTypePassessForDifferentType()
        {
            Verify.That(new MyTestClass()).IsNotAnInstanceOfType(typeof(string));
        }
        
        /// <summary>
        ///A test for IsNotAnInstanceOfType
        ///</summary>
        [TestMethod()]
        public void IsNotAnInstanceOfTypeFailsForSameType()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(new MyTestClass()).IsNotAnInstanceOfType(typeof(MyTestClass))
            ).AndHasAMessageThat()
            .Contains("[My Test Class instance] should not be an instance of [MyTestClass]");
        }
        #endregion
    }
}
