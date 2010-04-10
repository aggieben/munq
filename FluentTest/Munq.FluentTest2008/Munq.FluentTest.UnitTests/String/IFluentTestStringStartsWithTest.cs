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
    public class IFluentTestStringStartsWithTest
    {
        private const string testString = "May the Force be with you.";
            
        #region StartsWith
        [TestMethod]
        public void StringStartsWithFailsIfStringToCompareIsNull()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAStringThat().StartsWith(null)
            ).AndHasAMessageThat().Contains("[May the Force be with you.] can't be compared to [null]");
        }

        [TestMethod]
        public void StringStartsWithFailsIfDoesNotStartWithString()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAStringThat().StartsWith("monkey")
            ).AndHasAMessageThat().Contains("[May the Force be with you.] should start with [monkey]");
        }
        [TestMethod]
        public void StringStartsWithPassesIfStringStartsWithStringToCompare()
        {
            Verify.That(testString).IsAStringThat().StartsWith("May the");
        }


        [TestMethod]
        public void StringStartsWithPassesIfStringToCompareIsEmpty()
        {
            Verify.That(testString).IsAStringThat().StartsWith(String.Empty);
        }
        #endregion

        #region DoesNotStartWith
        [TestMethod]
        public void StringDoesNotStartWithFailsIfStringToCompareIsNull()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAStringThat().DoesNotStartWith(null)
            ).AndHasAMessageThat().Contains("[May the Force be with you.] can't be compared to [null]");
        }

        [TestMethod]
        public void StringDoesNotStartWithFailsIfStartsWithString()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAStringThat().DoesNotStartWith("May the")
            ).AndHasAMessageThat().Contains("[May the Force be with you.] should not start with [May the]");
        }
        [TestMethod]
        public void StringDoesNotStartWithPassesIfStringDoesNotStartWithStringToCompare()
        {
            Verify.That(testString).IsAStringThat().DoesNotStartWith("monkey");
        }


        [TestMethod]
        public void StringDoesNotStartWithFailsIfStringToCompareIsEmpty()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAStringThat().DoesNotStartWith(String.Empty)
            ).AndHasAMessageThat().Contains("[May the Force be with you.] should not start with []");
        }
        #endregion


    }
}
