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
    public class IFluentTestStringIsEqualToTest
    {
        private const string testString = "May the Force be with you.";
            
        #region IsEqualTo
        [TestMethod]
        public void StringIsEqualToFailsIfStringToCompareIsNull()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAStringThat().IsEqualTo(null)
            ).AndHasAMessageThat().Contains("[May the Force be with you.] can't be compared to [null]");
        }

        [TestMethod]
        public void StringIsEqualToFailsIfDoesIsEqualTotring()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAStringThat().IsEqualTo("monkey")
            ).AndHasAMessageThat().Contains("[May the Force be with you.] should be equal to [monkey]");
        }
        
        [TestMethod]
        public void StringIsEqualToPassesIfStringIsEqualToStringToCompare()
        {
            Verify.That(testString).IsAStringThat().IsEqualTo(testString);
        }

        [TestMethod]
        public void StringIsEqualToFailsIfStringToCompareIsEmpty()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAStringThat().IsEqualTo(String.Empty)
            ).AndHasAMessageThat().Contains("[May the Force be with you.] should be equal to []");
        }
        #endregion

        #region IsNotEqualTo            
        [TestMethod]
        public void StringIsNotEqualToFailsIfStringToCompareIsNull()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAStringThat().IsNotEqualTo(null)
            ).AndHasAMessageThat().Contains("[May the Force be with you.] can't be compared to [null]");
        }

        [TestMethod]
        public void StringIsNotEqualToFailsIfDoesIsEqualTotring()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAStringThat().IsNotEqualTo(testString)
            ).AndHasAMessageThat().Contains("[May the Force be with you.] should not be equal to [May the Force be with you.]");
        }
        
        [TestMethod]
        public void StringIsNotEqualToPassesIfStringIsNotEqualToStringToCompare()
        {
            Verify.That(testString).IsAStringThat().IsNotEqualTo("monkey");
        }

        [TestMethod]
        public void StringIsNotEqualToPassesIfStringToCompareIsEmpty()
        {
            Verify.That(testString).IsAStringThat().IsNotEqualTo(String.Empty);
        }
       #endregion
    }
}
