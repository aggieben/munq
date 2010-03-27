using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq.FluentTest;

namespace Munq.FluentTest.UnitTests
{   
    /// <summary>
    ///This is a test class for IFluentTestCommonTest and is intended
    ///to contain all IFluentTestCommonTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IFluentTestCommonTest
    {
        /// <summary>
        ///A test for Fail
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void FailWithMessage()
        {
            Verify.That(null).Fail("Fail(string msg) Passed!");
        }

        /// <summary>
        ///A test for Fail
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void Fail()
        {
            Verify.That(null).Fail();
        }

        /// <summary>
        ///A test for Inconclusive
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertInconclusiveException))]
        public void InconclusiveWithMessage()
        {
            Verify.That(null).Inconclusive("Inconclusive(string msg) Passed!");
        }

        /// <summary>
        ///A test for Inconclusive
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertInconclusiveException))]
        public void Inconclusive()
        {
            Verify.That(null).Inconclusive();
        }

        /// <summary>
        ///A test for IsNull
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsNullWithMessageFailsForNonNullObject()
        {
            Verify.That(new object()).IsNull("IsNull(string msg) Passed!");
        }

        /// <summary>
        ///A test for IsNull
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(AssertFailedException))]
        public void IsNullFailsForNonNullObject()
        {
            Verify.That(new object()).IsNull();
        }
        ///</summary>
        [TestMethod()]
        public void IsNullWithMessagePassessForNullObject()
        {
            Verify.That(null).IsNull("IsNull(string msg) Passed!");
        }

       /// <summary>
        ///A test for IsNull
        ///</summary>
        [TestMethod()]
        public void IsNullPassesForNullObject()
        {
            Verify.That(null).IsNull();
        }
    }
}
