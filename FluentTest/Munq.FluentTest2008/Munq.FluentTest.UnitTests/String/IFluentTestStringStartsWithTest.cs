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
    public class IFluentTestStringStartsWithTest
    {
        private const string testString = "May the Force be with you.";
            
        #region StartsWith
        [TestMethod]
        public void StringStartsWithFailsIfStringToCompareIsNull()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAString().StartsWith(null)
            );
        }

        [TestMethod]
        public void StringStartsWithFailsIfDoesNotStartWithString()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAString().StartsWith("monkey")
            );
        }
        [TestMethod]
        public void StringStartsWithPassesIfStringStartsWithStringToCompare()
        {
            Verify.That(testString).IsAString().StartsWith("May the");
        }


        [TestMethod]
        public void StringStartsWithFailsIfStringToCompareIsEmpty()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAString().StartsWith(String.Empty)
            );
        }
        #endregion

        #region DoesNotStartWith
        [TestMethod]
        public void StringDoesNotStartWithFailsIfStringToCompareIsNull()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAString().DoesNotStartWith(null)
            );
        }

        [TestMethod]
        public void StringDoesNotStartWithFailsIfStartsWithString()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAString().DoesNotStartWith("May the")
            );
        }
        [TestMethod]
        public void StringDoesNotStartWithPassesIfStringDoesNotStartWithStringToCompare()
        {
            Verify.That(testString).IsAString().DoesNotStartWith("monkey");
        }


        [TestMethod]
        public void StringDoesNotStartWithFailsIfStringToCompareIsEmpty()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAString().DoesNotStartWith(String.Empty)
            );
        }
        #endregion


    }
}
