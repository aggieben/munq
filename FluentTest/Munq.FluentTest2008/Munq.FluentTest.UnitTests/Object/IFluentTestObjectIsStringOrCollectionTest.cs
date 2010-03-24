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
        [ExpectedException(typeof(AssertFailedException))]
        public void IsACollectionFailsForNull()
        {
            Verify.That(null).IsACollection();
        }
        /// <summary>
        ///A test for IsACollection
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsACollectionFailsForNotCollection()
        {
            Verify.That(new object()).IsACollection();
        }

        [TestMethod()]
        public void IsACollectionPassesForCollection()
        {
            Verify.That(new List<object>()).IsACollection();
        }

        /// <summary>
        ///A test for IsACollection
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsACollectionWithMessageFailsForNull()
        {
            Verify.That(null).IsACollection("IsACollection(string msg) Passed!");
        }

        /// <summary>
        ///A test for IsACollection
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsACollectionWithMessageFailsForNotCollection()
        {
            Verify.That(new object()).IsACollection("IsACollection(string msg) Passed!");
        }

        /// <summary>
        ///A test for IsACollection
        ///</summary>
        [TestMethod()]
        public void IsACollectionWithMessagePassesForCollection()
        {
            Verify.That(new List<object>()).IsACollection("IsACollection(string msg) Failed!");
        }
        #endregion

        #region IsAString

        /// <summary>
        ///A test for IsAString
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsAStringFailsForNull()
        {
            Verify.That(null).IsAString();
        }

        ///<summary>
        ///A test for IsAString
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsAStringFailsForNonString()
        {
            Verify.That(new DateTime()).IsAString();
        }

        ///<summary>
        ///A test for IsAString
        ///</summary>
        [TestMethod()]
        public void IsAStringPassesForString()
        {
            Verify.That("This Should Pass!").IsAString();
        }

        /// <summary>
        ///A test for IsAString
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsAStringWithMessageFailsForNull()
        {
            Verify.That(null).IsAString("IsAString(string msg) Passed!");
        }

        ///<summary>
        ///A test for IsAString
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsAStringWithMessagegFailsForNonString()
        {
            Verify.That(new DateTime()).IsAString("IsAString(string msg) Passed!");
        }

        /// <summary>
        ///A test for IsAString
        ///</summary>
        [TestMethod()]
        public void IsAStringWithMessagePassesForString()
        {
            Verify.That("This Should Pass!").IsAString("IsAString(string msg) Failed!");
        }

        #endregion
    }
}
