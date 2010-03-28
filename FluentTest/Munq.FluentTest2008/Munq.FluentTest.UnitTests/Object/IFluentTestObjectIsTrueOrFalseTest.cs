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
        public void IsTrueFailsForNull()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(null).IsTrue()
            );
        }

        /// <summary>
        ///A test for IsTrue
        ///</summary>
        [TestMethod()]
        public void IsTrueFailsForFalse()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(false).IsTrue()
            );           
        }

        /// <summary>
        ///A test for IsTrue
        ///</summary>
        [TestMethod()]
        public void IsTrueFailsForNonBool()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That("A string").IsTrue()
            );
        }

        /// <summary>
        ///A test for IsTrue
        ///</summary>
        [TestMethod()]
        public void IsTruePassesForTrue()
        {
            Verify.That(1 == 1).IsTrue();
        }

        #endregion

        #region IsFalse
        /// <summary>
        ///A test for IsFalse
        ///</summary>
        [TestMethod()]
        public void IsFalseFailsForNull()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(null).IsFalse()
            );
        }

        /// <summary>
        ///A test for IsFalse
        ///</summary>
        [TestMethod()]
        public void IsFalseFailsForTrue()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(true).IsFalse()
            );
        }

        /// <summary>
        ///A test for IsFalse
        ///</summary>
        [TestMethod()]
        public void IsFalseFailsForNonBool()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That("A string").IsFalse()
            );
        }

        /// <summary>
        ///A test for IsFalse
        ///</summary>
        [TestMethod()]
        public void IsFalsePassesForFalse()
        {
            Verify.That(1 != 1).IsFalse();
        }
        #endregion
     }
}
