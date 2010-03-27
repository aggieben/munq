using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq.FluentTest;

namespace Munq.FluentTest.UnitTests
{ 
    /// <summary>
    ///This is a test class for IFluentTestTest and is intended
    ///to contain all IFluentTestTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IFluentTestObjectIsTrueOrFalseTest
    {        
        #region IsTrue
        /// <summary>
        ///A test for IsTrue
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsTrueFailsForNull()
        {
            Verify.That(null).IsTrue();
        }

        /// <summary>
        ///A test for IsTrue
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsTrueFailsForFalse()
        {
            Verify.That(false).IsTrue();
        }

        /// <summary>
        ///A test for IsTrue
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsTrueFailsForNonBool()
        {
            Verify.That("A string").IsTrue();
        }

        /// <summary>
        ///A test for IsTrue
        ///</summary>
        [TestMethod()]
        public void IsTruePassesForTrue()
        {
            Verify.That(1 == 1).IsTrue();
        }

        /// <summary>
        ///A test for IsTrue
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsTrueWithMessageFailsForNull()
        {
            Verify.That(null).IsTrue("IsTrue(string msg) Passed!");
        }

        /// <summary>
        ///A test for IsTrue
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsTrueWithMessageFailsForFalse()
        {
            Verify.That(false).IsTrue("IsTrue(string msg) Passed!");
        }

        /// <summary>
        ///A test for IsTrue
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsTrueWithMessageFailsForNonBool()
        {
            Verify.That("A string").IsTrue("IsTrue(string msg) Passed!");
        }

        /// <summary>
        ///A test for IsTrue
        ///</summary>
        [TestMethod()]
        public void IsTrueWithMessagePassesForTrue()
        {
            Verify.That(1 == 1).IsTrue("IsTrue(string msg) Failed!");
        }
        #endregion

        #region IsFalse
        /// <summary>
        ///A test for IsFalse
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsFalseFailsForNull()
        {
            Verify.That(null).IsFalse();
        }

        /// <summary>
        ///A test for IsFalse
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsFalseFailsForTrue()
        {
            Verify.That(true).IsFalse();
        }

        /// <summary>
        ///A test for IsFalse
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsFalseFailsForNonBool()
        {
            Verify.That("A string").IsFalse();
        }

        /// <summary>
        ///A test for IsFalse
        ///</summary>
        [TestMethod()]
        public void IsFalsePassesForFalse()
        {
            Verify.That(1 != 1).IsFalse();
        }

        /// <summary>
        ///A test for IsFalse
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsFalseWithMessageFailsForNull()
        {
            Verify.That(null).IsFalse("IsFalse(string msg) Passed!");
        }

        /// <summary>
        ///A test for IsFalse
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsFalseWithMessageFailsForTrue()
        {
            Verify.That(true).IsFalse("IsFalse(string msg) Passed!");
        }

        /// <summary>
        ///A test for IsFalse
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsFalseWithMessageFailsForNonBool()
        {
            Verify.That("A string").IsFalse("IsFalse(string msg) Passed!");
        }

        /// <summary>
        ///A test for IsFalse
        ///</summary>
        [TestMethod()]
        public void IsFalseWithMessagePassesForFalse()
        {
            Verify.That(1 != 1).IsFalse("IsFalse(string msg) Failed!");
        }
        #endregion
     }
}
