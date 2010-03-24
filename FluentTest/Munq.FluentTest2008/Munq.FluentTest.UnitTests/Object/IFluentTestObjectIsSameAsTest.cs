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
    public class IFluentTestObjectIsSameAsTest
    {
        private class MyTestClass
        {
        }

        #region IsTheSameObjectAs
        /// <summary>
        ///A test for IsTheSameObjectAs
        ///</summary>
        [TestMethod()]
        public void IsTheSameObjectPassesForNullTestAndCompareObjects()
        {
            Verify.That(null).IsTheSameObjectAs(null);
        }

        /// <summary>
        ///A test for IsTheSameObjectAs
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsTheSameObjectFailsForNullTestAndNonNullCompareObjects()
        {
            Verify.That(null).IsTheSameObjectAs(new MyTestClass());
        }
        /// <summary>
        ///A test for IsTheSameObjectAs
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsTheSameObjectFailsForNonNullTestAndNullCompareObjects()
        {
            Verify.That(new MyTestClass()).IsTheSameObjectAs(null);
        }
        /// <summary>
        ///A test for IsTheSameObjectAs
        ///</summary>
        [TestMethod()]
        public void IsTheSameObjectPassessFroSameNonNullTestAndCompareObjects()
        {
            var testObject = new MyTestClass();
            Verify.That(testObject).IsTheSameObjectAs(testObject);
        }
        #endregion

        #region IsNotTheSameObjectAs

        #endregion
    }
}
