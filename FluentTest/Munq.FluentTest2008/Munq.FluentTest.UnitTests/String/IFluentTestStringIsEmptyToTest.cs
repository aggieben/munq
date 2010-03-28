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
                () => Verify.That(testString).IsAString().IsEmpty()
            );
        }
        
        [TestMethod]
        public void StringIsEmptyPassesIfStringIsEmpty()
        {
            Verify.That(String.Empty).IsAString().IsEmpty();
        }
        #endregion

        #region IsNotEmpty            
        [TestMethod]
        public void StringIsNotEmptyFailsIfIsEmpty()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(String.Empty).IsAString().IsNotEmpty()
            );
        }

        [TestMethod]
        public void StringIsNotEmptyPassesIfStringIsNotEmpty()
        {
            Verify.That(testString).IsAString().IsNotEmpty();
        }
        #endregion
    }
}
