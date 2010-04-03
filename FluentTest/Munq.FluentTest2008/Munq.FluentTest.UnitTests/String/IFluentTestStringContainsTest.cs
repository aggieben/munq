using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq.FluentTest;

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
            Verify.That(StringUnderTest).IsAStringThat().Contains("Jump");
        }
        
        [TestMethod]
        public void ContainsFailsIfStringUnderTestDoesNotContainStringToCompare()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(StringUnderTest).IsAStringThat().Contains("Zebra")
            );
        }
        
        [TestMethod]
        public void ContainsFailsIfStringToCompareIsNull()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(StringUnderTest).IsAStringThat().Contains(null)
            );
        }

         #endregion

        #region DoesNotContain
        [TestMethod]
        public void DoesNotContainFailsIfStringUnderTestContainsStringToCompare()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(StringUnderTest).IsAStringThat().DoesNotContain("Jump")
            );
        }

        [TestMethod]
        public void DoesNotContainPassesIfStringUnderTestDoesNotContainStringToCompare()
        {
            Verify.That(StringUnderTest).IsAStringThat().DoesNotContain("Zebra");
        }

        [TestMethod]
        public void DoesNotContainPassesIfStringToCompareIsNull()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(StringUnderTest).IsAStringThat().DoesNotContain(null)
            );
        }
        #endregion
    }
}
