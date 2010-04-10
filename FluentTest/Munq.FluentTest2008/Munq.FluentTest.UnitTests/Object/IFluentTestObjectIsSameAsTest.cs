#region Copyright Notice
// Copyright 2010 by Matthew Dennis
#endregion

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Munq.FluentTest.UnitTests
{
    /// <summary>
    ///This is a test class for IFluentTestTest and is intended
    ///to contain all IFluentTestTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IFluentTestObjectIsSameAsTest
    {
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
        public void IsTheSameObjectFailsForNullTestAndNonNullCompareObjects()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(null).IsTheSameObjectAs(new MyTestClass())
            ).AndHasAMessageThat().Contains("[null] should be the same object as [My Test Class instance]");
        }
        /// <summary>
        ///A test for IsTheSameObjectAs
        ///</summary>
        [TestMethod()]
        public void IsTheSameObjectFailsForNonNullTestAndNullCompareObjects()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(new MyTestClass()).IsTheSameObjectAs(null)
            );
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

        [TestMethod()]
        public void IsTheSameObjectFailsForDifferentTestAndCompareObjects()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(new MyTestClass()).IsTheSameObjectAs(new MyTestClass())
            ).AndHasAMessageThat().Contains("[My Test Class instance] should be the same object as [My Test Class instance]");
        }

        #endregion

        #region IsNotTheSameObjectAs

        /// <summary>
        ///A test for IsTheSameObjectAs
        ///</summary>
        [TestMethod()]
        public void IsNotTheSameObjectPassesForNullTestAndCompareObjects()
        {
            Verify.That(null).IsTheSameObjectAs(null);
        }

        /// <summary>
        ///A test for IsTheSameObjectAs
        ///</summary>
        [TestMethod()]
        public void IsNotTheSameObjectPassesForNullTestAndNonNullCompareObjects()
        {
                Verify.That(null).IsNotTheSameObjectAs(new MyTestClass());
        }
        /// <summary>
        ///A test for IsTheSameObjectAs
        ///</summary>
        [TestMethod()]
        public void IsNotTheSameObjectPassesForNonNullTestAndNullCompareObjects()
        {
                Verify.That(new MyTestClass()).IsNotTheSameObjectAs(null);
        }

        /// <summary>
        ///A test for IsTheSameObjectAs
        ///</summary>
        [TestMethod()]
        public void IsNotTheSameObjectFailsForSameNonNullTestAndCompareObjects()
        {
            var testObject = new MyTestClass();
           Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                 () => Verify.That(testObject).IsNotTheSameObjectAs(testObject)
            ).AndHasAMessageThat().Contains("[My Test Class instance] should not be the same object as [My Test Class instance]");
        }
        [TestMethod()]
        public void IsNotTheSameObjectPassesForDifferentTestAndCompareObjects()
        {
            Verify.That(new MyTestClass()).IsNotTheSameObjectAs(new MyTestClass());
        }
        #endregion
    }
}
