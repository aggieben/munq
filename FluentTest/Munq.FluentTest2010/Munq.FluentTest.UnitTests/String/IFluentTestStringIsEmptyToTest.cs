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
    public class IFluentTestStringIsEmptyToTest
    {
        private const string testString = "May the Force be with you.";
            
        #region IsEmpty
        [TestMethod]
        public void StringIsEmptyFailsIfIsNotEmpty()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAStringThat().IsEmpty()
            ).AndHasAMessageThat().Contains("[May the Force be with you.] should be empty");
        }
        
        [TestMethod]
        public void StringIsEmptyPassesIfStringIsEmpty()
        {
            Verify.That(String.Empty).IsAStringThat().IsEmpty();
        }
        #endregion

        #region IsNotEmpty            
        [TestMethod]
        public void StringIsNotEmptyFailsIfIsEmpty()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(String.Empty).IsAStringThat().IsNotEmpty()
            ).AndHasAMessageThat().Contains("[] should not be empty");
        }

        [TestMethod]
        public void StringIsNotEmptyPassesIfStringIsNotEmpty()
        {
            Verify.That(testString).IsAStringThat().IsNotEmpty();
        }
        #endregion
    }
}
