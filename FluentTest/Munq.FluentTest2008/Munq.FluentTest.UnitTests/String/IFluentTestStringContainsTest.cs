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
    public class IFluentTestStringContainsTest
    {
    // no need to check if the object under test is a string.  You can't get here
    // if it isn't.
    const string StringUnderTest = "A Test String with Jump in it.";
         #region Contains
        [TestMethod]
        public void ContainsPassesIfStringUnderTestContainsStringToCompare()
        {
            Verify.That(StringUnderTest).IsAString().Contains("Jump");
        }
        
        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void ContainsFailsIfStringUnderTestDoesNotContainStringToCompare()
        {
           Verify.That(StringUnderTest).IsAString().Contains("Zebra"); 
        }
        
        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void ContainsFailsIfStringToComparIsNull()
        {
            Verify.That(StringUnderTest).IsAString().Contains(null);
        }

        [TestMethod]
        public void ContainsWithMsgPassesIfStringUnderTestContainsStringToCompare()
        {
            Verify.That(StringUnderTest).IsAString()
            .Contains("Jump","Contains(string msg) Failed!");
        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void ContainsWithMsgFailsIfStringUnderTestDoesNotContainStringToCompare()
        {
            Verify.That(StringUnderTest).IsAString()
            .Contains("Zebra", "Contains(string msg) Passed!");
        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void ContainsWithMsgFailsIfStringToComparIsNull()
        {
            Verify.That(StringUnderTest).IsAString()
            .Contains(null, "Contains(string msg) Passed!");
        }

         #endregion

        #region DoesNotContain
        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void DoesNotContainFailsIfStringUnderTestContainsStringToCompare()
        {
            Verify.That(StringUnderTest).IsAString().DoesNotContain("Jump");
        }

        [TestMethod]
        public void DoesNotContainPassesIfStringUnderTestDoesNotContainStringToCompare()
        {
            Verify.That(StringUnderTest).IsAString().DoesNotContain("Zebra");
        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void DoesNotContainPassesIfStringToComparIsNull()
        {
            Verify.That(StringUnderTest).IsAString().DoesNotContain(null);
        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void DoesNotContainWithMsgFailsIfStringUnderTestContainsStringToCompare()
        {
            Verify.That(StringUnderTest).IsAString()
            .DoesNotContain("Jump", "DoesNotContain(string msg) Passed!");
        }

        [TestMethod]
        public void DoesNotContainWithMsgPassesIfStringUnderTestDoesNotContainStringToCompare()
        {
            Verify.That(StringUnderTest).IsAString()
            .DoesNotContain("Zebra", "DoesNotContain(string msg) Failed!");
        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void DoesNotContainWithMsgPassesIfStringToComparIsNull()
        {
            Verify.That(StringUnderTest).IsAString()
            .DoesNotContain(null, "DoesNotContain(string msg) Passed!");
        }

        #endregion


    }
}
