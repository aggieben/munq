#region Copyright Notice
// Copyright 2010 by Matthew Dennis
#endregion

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
        #region IsNotNull
        /// <summary>
        ///A test for IsNotNull
        ///</summary>
        [TestMethod()]
        public void IsNotNullFailsForNull()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(null).IsNotNull()
            ).AndHasAMessageThat().Contains("[null] should not be null");
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
