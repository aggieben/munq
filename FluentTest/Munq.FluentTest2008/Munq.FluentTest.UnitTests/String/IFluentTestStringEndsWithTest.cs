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
    public class IFluentTestStringEndsWithTest
    {
        private const string testString = "May the Force be with you.";

        #region EndsWith       
        [TestMethod]
        public void StringEndsWithFailsIfStringToCompareIsNull()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAString().EndsWith(null)
            );
        }
         
        [TestMethod]
        public void StringEndsWithFailsIfDifferentString()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAString().EndsWith("monkey")
            );
        }
        [TestMethod]
        public void StringEndsWithPassesIfStringEndsWithStringToCompare()
        {
            Verify.That(testString).IsAString().EndsWith("be with you.");
        }

        
        [TestMethod]
        public void StringEndsWithFailsIfStringToCompareIsEmpty()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAString().EndsWith(String.Empty)
            );
        }
        #endregion

        #region DoesNotEndWith
        [TestMethod]
        public void StringDoesNotEndWithFailsIfStringToCompareIsNull()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAString().DoesNotEndWith(null)
            );
        }

        [TestMethod]
        public void StringDoesNotEndWithFailsIfStringEndWithStringToCompare()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAString().DoesNotEndWith("be with you.")
            );
        }
        
        [TestMethod]
        public void StringDoesNotEndWithPassesIfStringDoesNotEndWithStringToCompare()
        {
            Verify.That(testString).IsAString().DoesNotEndWith("monkey.");
        }

        [TestMethod]
        public void StringDoesNotEndWithFailsIfStringToCompareIsEmpty()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAString().DoesNotEndWith(String.Empty)
            );
        }
        #endregion


    }
}
