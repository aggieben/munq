using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq.FluentTest;

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
        public void IsAnInstanceOfTypeFailsForNull()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(null).IsAnInstanceOfType(typeof(Object))
            );
        }

        /// <summary>
        ///A test for IsAnInstanceOfType
        ///</summary>
        [TestMethod()]
        public void IsAnInstanceOfTypeFailsForDifferentType()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(new MyTestClass()).IsAnInstanceOfType(typeof(string))
            );
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
            );
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
            );
        }
        #endregion
    }
}
