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
    public class IFluentTestObjectIsInstanceOfTest
    {
        private class MyTestClass
        {
        }

        #region IsAnInstanceOf
        /// <summary>
        ///A test for IsAnInstanceOfType
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsAnInstanceOfTypeFailsForNull()
        {
            Verify.That(null).IsAnInstanceOfType(typeof(Object));
        }

        /// <summary>
        ///A test for IsAnInstanceOfType
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsAnInstanceOfTypeFailsForDifferentType()
        {
            Verify.That(new MyTestClass()).IsAnInstanceOfType(typeof(string));
        }
        /// <summary>
        ///A test for IsAnInstanceOfType
        ///</summary>
        [TestMethod()]
        public void IsAnInstanceOfTypePassesForSameType()
        {
            Verify.That(new MyTestClass()).IsAnInstanceOfType(typeof(MyTestClass));
        }

        /// <summary>
        ///A test for IsAnInstanceOfType
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsAnInstanceOfTypeWithMessageFailsForNull()
        {
            Verify.That(null)
                  .IsAnInstanceOfType(typeof(Object), "IsAnInstance(string msg) Passed!");
        }

        /// <summary>
        ///A test for IsAnInstanceOfType
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsAnInstanceOfTypeWithMessageFailsForDifferentType()
        {
            Verify.That(new MyTestClass())
                  .IsAnInstanceOfType(typeof(string), "IsAnInstance(string msg) Passed!");
        }
        /// <summary>
        ///A test for IsAnInstanceOfType
        ///</summary>
        [TestMethod()]
        public void IsAnInstanceOfTypeWithMessagePassesForSameType()
        {
            Verify.That(new MyTestClass())
                  .IsAnInstanceOfType(typeof(MyTestClass), "IsAnInstance(string msg) Failed!");
        }
        #endregion

        #region IsNotAnInstanceOf

        /// <summary>
        ///A test for IsNotAnInstanceOfType
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsNotAnInstanceOfTypeFailsForNull()
        {
            Verify.That(null).IsNotAnInstanceOfType(null);
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
        [ExpectedException(typeof(AssertFailedException))]
        public void IsNotAnInstanceOfTypeFailsForSameType()
        {
            Verify.That(new MyTestClass()).IsNotAnInstanceOfType(typeof(MyTestClass));
        }

        /// <summary>
        ///A test for IsNotAnInstanceOfType
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsNotAnInstanceOfTypeWithMessageFailsForNull()
        {
            Verify.That(null)
                  .IsNotAnInstanceOfType(null, "IsNotAnInstance(string msg) Passed!");
        }

        /// <summary>
        ///A test for IsNotAnInstanceOfType
        ///</summary>
        [TestMethod()]
        public void IsNotAnInstanceOfTypeWithMessagePassessForDifferentType()
        {
            Verify.That(new MyTestClass())
                  .IsNotAnInstanceOfType(typeof(string), "IsNotAnInstance(string msg) Failed!");
        }
        /// <summary>
        ///A test for IsNotAnInstanceOfType
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsNotAnInstanceOfTypeWithMessageFailsForSameType()
        {
            Verify.That(new MyTestClass())
                  .IsNotAnInstanceOfType(typeof(MyTestClass), "IsNotAnInstance(string msg) Passed!");
        }
        #endregion
    }
}
