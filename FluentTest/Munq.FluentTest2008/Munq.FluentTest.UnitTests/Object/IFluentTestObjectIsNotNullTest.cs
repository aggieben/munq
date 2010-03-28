using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq.FluentTest;

namespace Munq.FluentTest.UnitTests
{  
    /// <summary>
    ///This is a test class for IFluentTestTest and is intended
    ///to contain all IFluentTestTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IFluentTestObjectIsNotNullTest
    {
        private class MyTestClass
        {
        }

        #region IsNotNull
        /// <summary>
        ///A test for IsNotNull
        ///</summary>
        [TestMethod()]
        public void IsNotNullFailsForNull()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(null).IsNotNull()
            );
        }

        ///<summary>
        ///A test for IsNotNull
        ///</summary>
        [TestMethod()]
        public void IsNotNullPassesFoNonNull()
        {
            Verify.That(new MyTestClass()).IsNotNull();
        }

        #endregion
    }
}
