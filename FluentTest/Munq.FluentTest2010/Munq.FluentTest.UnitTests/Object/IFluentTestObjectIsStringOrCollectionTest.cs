#region Copyright Notice
// Copyright 2010 by Matthew Dennis
#endregion

using Munq.FluentTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Munq.FluentTest.UnitTests
{  
    /// <summary>
    ///This is a test class for IFluentTestTest and is intended
    ///to contain all IFluentTestTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IFluentTestObjectIsStringOrCollectionTest
    {
        private class MyTestClass
        {
        }

        #region IsCollection
        /// <summary>
        ///A test for IsACollection
        ///</summary>
        [TestMethod()]
        public void IsACollectionFailsForNull()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(null).IsACollectionThat()
            ).AndHasAMessageThat().Contains("[null] is not a collection");
        }
        
        /// <summary>
        ///A test for IsACollection
        ///</summary>
        [TestMethod()]
        public void IsACollectionFailsForNotCollection()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(new object()).IsACollectionThat()
            ).AndHasAMessageThat().Contains("Object] is not a collection");
        }

        [TestMethod()]
        public void IsACollectionPassesForCollection()
        {
            Verify.That(new List<object>()).IsACollectionThat();
        }

        #endregion

        #region IsAString

        /// <summary>
        ///A test for IsAString
        ///</summary>
        [TestMethod()]
        public void IsAStringFailsForNull()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(null).IsAStringThat()
            ).AndHasAMessageThat().Contains("[null] is not a string");
        }

        ///<summary>
        ///A test for IsAString
        ///</summary>
        [TestMethod()]
        public void IsAStringFailsForNonString()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(new DateTime()).IsAStringThat()
            ).AndHasAMessageThat().Contains("] is not a string");
        }

        ///<summary>
        ///A test for IsAString
        ///</summary>
        [TestMethod()]
        public void IsAStringPassesForString()
        {
            Verify.That("This Should Pass!").IsAStringThat();
        }

        #endregion
    }
}
