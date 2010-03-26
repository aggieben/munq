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
    public class IFluentTestObjectIsEqualTest
    {
        [AssemblyInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Verify.Provider = new MsTestProvider();
        }
         #region IsEqualTo
        /// <summary>
        ///A test for IsEqualTo
        ///</summary>
        [TestMethod()]
        public void IsEqualPassesForNullTestAndCompareObjects()
        {
            Verify.That(null).IsEqualTo(null);
        }

        /// <summary>
        ///A test for IsEqualTo
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsEqualFailsForNullTestAndNonNullCompareObjects()
        {
            Verify.That(null).IsEqualTo(new Object());
        }

        /// <summary>
        ///A test for IsEqualTo
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsEqualFailsForNonNullTestAndNullCompareObjects()
        {
            Verify.That(new Object()).IsEqualTo(null);
        }

        /// <summary>
        ///A test for IsEqualTo
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsEqualFailsForDifferentTestAndCompareObjects()
        {
            Verify.That("Test thing1").IsEqualTo("Test thing2");
        }

        /// <summary>
        ///A test for IsEqualTo
        ///</summary>
        [TestMethod()] 
        public void IsEqualPassesForEqualTestAndCompareObjects()
        {
            Verify.That(1).IsEqualTo(1);
        }

        /// <summary>
        ///A test for IsEqualTo
        ///</summary>
        [TestMethod()]
        public void IsEqualWithMessagePassesForNullTestAndCompareObjects()
        {
            Verify.That(null).IsEqualTo(null, "IsEqual(string msg) Failed!");
        }

        /// <summary>
        ///A test for IsEqualTo
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsEqualWithMessageFailsForNullTestAndNonNullCompareObjects()
        {
            Verify.That(null).IsEqualTo(new Object(), "IsEqual(string msg) Passed!");
        }

        /// <summary>
        ///A test for IsEqualTo
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsEqualWithMessageFailsForNonNullTestAndNullCompareObjects()
        {
            Verify.That(new Object()).IsEqualTo(null, "IsEqual(string msg) Passed!");
        }

        /// <summary>
        ///A test for IsEqualTo
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsEqualWithMessageFailsForDifferentTestAndCompareObjects()
        {
            Verify.That(new TimeSpan(0, 0, 1)).IsEqualTo("Test thing2", "IsEqual(string msg) Passed!");
        }

        /// <summary>
        ///A test for IsEqualTo
        ///</summary>
        [TestMethod()]
        public void IsEqualWithMessagePassesForEqualTestAndCompareObjects()
        {
            Verify.That(new TimeSpan(0, 0, 1))
                  .IsEqualTo(new TimeSpan(0, 0, 1), "IsEqual(string msg) Failed!");
        }
        #endregion

        #region IsNotEqualTo
        /// <summary>
        ///A test for IsNotEqualTo
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsNotEqualPFailsForNullTestAndCompareObjects()
        {
            Verify.That(null).IsNotEqualTo(null);
        }

        /// <summary>
        ///A test for IsNotEqualTo
        ///</summary>
        [TestMethod()]
        public void IsNotEqualPassesForNullTestAndNonNullCompareObjects()
        {
            Verify.That(null).IsNotEqualTo(new Object());
        }

        /// <summary>
        ///A test for IsNotEqualTo
        ///</summary>
        [TestMethod()]
        public void IsNotEqualPassessForNonNullTestAndNullCompareObjects()
        {
            Verify.That(new Object()).IsNotEqualTo(null);
        }

        /// <summary>
        ///A test for IsNotEqualTo
        ///</summary>
        [TestMethod()]
        public void IsNotEqualPassesForDifferentTestAndCompareObjects()
        {
            Verify.That("Test thing1").IsNotEqualTo("Test thing2");
        }

        /// <summary>
        ///A test for IsNotEqualTo
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsNotEqualFailsForEqualTestAndCompareObjects()
        {
            Verify.That(1).IsNotEqualTo(1);
        }

        /// <summary>
        ///A test for IsNotEqualTo
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsNotEqualWithMessageFailsForNullTestAndCompareObjects()
        {
            Verify.That(null).IsNotEqualTo(null, "IsNotEqual(string msg) Passed!");
        }

        /// <summary>
        ///A test for IsNotEqualTo
        ///</summary>
        [TestMethod()]
        public void IsNotEqualWithMessagePassesForNullTestAndNonNullCompareObjects()
        {
            Verify.That(null).IsNotEqualTo(new Object(), "IsNotEqual(string msg) Failed!");
        }

        /// <summary>
        ///A test for IsNotEqualTo
        ///</summary>
        [TestMethod()]
        public void IsNotEqualWithMessagePassesForNonNullTestAndNullCompareObjects()
        {
            Verify.That(new Object()).IsNotEqualTo(null, "IsNotEqual(string msg) Failed!");
        }

        /// <summary>
        ///A test for IsNotEqualTo
        ///</summary>
        [TestMethod()]
        public void IsNotEqualWithMessagePassesForDifferentTestAndCompareObjects()
        {
            Verify.That(new TimeSpan(0, 0, 1))
                  .IsNotEqualTo("Test thing2", "IsNotEqual(string msg) Failed!");
        }

        /// <summary>
        ///A test for IsNotEqualTo
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsNotEqualWithMessageFailsForEqualTestAndCompareObjects()
        {
            Verify.That(new TimeSpan(0, 0, 1))
                  .IsNotEqualTo(new TimeSpan(0, 0, 1), "IsNotEqual(string msg) Passed!");
        }
        #endregion
    }
}
