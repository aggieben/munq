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
    public class IFluentTestStringStartsWithTest
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Verify.Provider = new MsTestProvider();
        }

        #region StartsWith
        #endregion

        #region DoesNotStartWith
        #endregion


    }
}
