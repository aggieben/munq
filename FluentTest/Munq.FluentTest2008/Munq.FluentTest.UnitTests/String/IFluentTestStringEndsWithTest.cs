using Munq.FluentTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

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
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Verify.Provider = new MsTestProvider();
        }

        #region EndsWith
        
        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void StringEndsWithFailsIfStringToCompareIsNull()
        {
            Verify.That(testString).IsAString().EndsWith(null);
        }
         
        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void StringEndsWithFailsIfDifferentString()
        {
            Verify.That(testString).IsAString().EndsWith("monkey");
        }
        
        [TestMethod]
        public void StringEndsWithFailsIfStringToCompareIsEmpty()
        {
            Verify.That(testString).IsAString().EndsWith(String.Empty);
        }
        #endregion

        #region DoesNotEndWith
        #endregion


    }
}
