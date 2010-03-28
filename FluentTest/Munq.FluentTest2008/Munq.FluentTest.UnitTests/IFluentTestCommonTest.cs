using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq.FluentTest;
using System;

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
        public void Fail()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(null).Fail()
            );
        }

        /// <summary>
        ///A test for Inconclusive
        ///</summary>
        [TestMethod()]
        public void Inconclusive()
        {
            Verify.TheExpectedException(Verify.InConclusiveExceptionType).IsThrownWhen(
                () => Verify.That(null).Inconclusive()
            );
        }


        /// <summary>
        ///A test for IsNull
        ///</summary>
        [TestMethod()]
        public void IsNullFailsForNonNullObject()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(new object()).IsNull()
            );          
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
