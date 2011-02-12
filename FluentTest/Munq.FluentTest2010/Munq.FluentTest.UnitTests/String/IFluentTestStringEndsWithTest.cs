#region Copyright Notice
// Copyright 2010 by Matthew Dennis
#endregion

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
                () => Verify.That(testString).IsAStringThat().EndsWith(null)
            ).AndHasAMessageThat().Contains("[May the Force be with you.] can't be compared to [null]");
        }
         
        [TestMethod]
        public void StringEndsWithFailsIfDifferentString()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAStringThat().EndsWith("monkey")
            ).AndHasAMessageThat().Contains("[May the Force be with you.] should end with [monkey]");
        }
        [TestMethod]
        public void StringEndsWithPassesIfStringEndsWithStringToCompare()
        {
            Verify.That(testString).IsAStringThat().EndsWith("be with you.");
        }

        
        [TestMethod]
        public void StringEndsWithPassesIfStringToCompareIsEmpty()
        {
            Verify.That(testString).IsAStringThat().EndsWith(String.Empty);
        }
        #endregion

        #region DoesNotEndWith
        [TestMethod]
        public void StringDoesNotEndWithFailsIfStringToCompareIsNull()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAStringThat().DoesNotEndWith(null)
            ).AndHasAMessageThat().Contains("[May the Force be with you.] can't be compared to [null]");
        }

        [TestMethod]
        public void StringDoesNotEndWithFailsIfStringEndWithStringToCompare()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAStringThat().DoesNotEndWith("be with you.")
            ).AndHasAMessageThat().Contains("[May the Force be with you.] should not end with [be with you.]");
        }
        
        [TestMethod]
        public void StringDoesNotEndWithPassesIfStringDoesNotEndWithStringToCompare()
        {
            Verify.That(testString).IsAStringThat().DoesNotEndWith("monkey.");
        }

        [TestMethod]
        public void StringDoesNotEndWithFailsIfStringToCompareIsEmpty()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAStringThat().DoesNotEndWith(String.Empty)
            ).AndHasAMessageThat().Contains("[May the Force be with you.] should not end with []");
        }
        #endregion


    }
}
