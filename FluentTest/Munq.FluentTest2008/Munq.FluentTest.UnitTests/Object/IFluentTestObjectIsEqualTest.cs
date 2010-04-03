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
    public class IFluentTestObjectIsEqualTest
    {
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
        public void IsEqualFailsForNullTestAndNonNullCompareObjects()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(null).IsEqualTo(new Object())
            ).AndHasAMessageThat().Contains("[null] should be equal to");
        }

        /// <summary>
        ///A test for IsEqualTo
        ///</summary>
        [TestMethod()]
        public void IsEqualFailsForNonNullTestAndNullCompareObjects()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(new Object()).IsEqualTo(null)
            ).AndHasAMessageThat().Contains("should be equal to [null]");
        }

        /// <summary>
        ///A test for IsEqualTo
        ///</summary>
        [TestMethod()]
        public void IsEqualFailsForDifferentTestAndCompareObjects()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That("Test thing1").IsEqualTo("Test thing2")
            ).AndHasAMessageThat().Contains("[Test thing1] should be equal to [Test thing2]");
        }

        /// <summary>
        ///A test for IsEqualTo
        ///</summary>
        [TestMethod()] 
        public void IsEqualPassesForEqualTestAndCompareObjects()
        {
            Verify.That(1).IsEqualTo(1);
        }
        #endregion

        #region IsNotEqualTo
        /// <summary>
        ///A test for IsNotEqualTo
        ///</summary>
        [TestMethod()]
        public void IsNotEqualPFailsForNullTestAndCompareObjects()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(null).IsNotEqualTo(null)
            ).AndHasAMessageThat().Contains("[null] should not be equal to [null]");
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
        public void IsNotEqualFailsForEqualTestAndCompareObjects()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(1).IsNotEqualTo(1)
            ).AndHasAMessageThat().Contains("[1] should not be equal to [1]");
        }
        #endregion
    }
}
