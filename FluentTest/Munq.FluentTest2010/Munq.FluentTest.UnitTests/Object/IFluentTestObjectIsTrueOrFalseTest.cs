#region Copyright Notice
// Copyright 2010 by Matthew Dennis
#endregion

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
            ).AndHasAMessageThat().Contains("[null] should be true");
        }

        /// <summary>
        ///A test for IsTrue
        ///</summary>
        [TestMethod()]
        public void IsTrueFailsForFalse()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(false).IsTrue()
            ).AndHasAMessageThat().Contains("[False] should be true");           
        }

        /// <summary>
        ///A test for IsTrue
        ///</summary>
        [TestMethod()]
        public void IsTrueFailsForNonBool()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That("A string").IsTrue()
            ).AndHasAMessageThat().Contains("[A string] should be true");
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
            ).AndHasAMessageThat().Contains("[null] should be false");
        }

        /// <summary>
        ///A test for IsFalse
        ///</summary>
        [TestMethod()]
        public void IsFalseFailsForTrue()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(true).IsFalse()
            ).AndHasAMessageThat().Contains("[True] should be false");
        }

        /// <summary>
        ///A test for IsFalse
        ///</summary>
        [TestMethod()]
        public void IsFalseFailsForNonBool()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That("A string").IsFalse()
            ).AndHasAMessageThat().Contains("[A string] should be false");
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
