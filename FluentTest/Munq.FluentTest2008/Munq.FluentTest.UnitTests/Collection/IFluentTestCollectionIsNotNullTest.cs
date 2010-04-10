#region Copyright Notice
// Copyright 2010 by Matthew Dennis
#endregion

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq.FluentTest;
using System.Collections.Generic;

namespace Munq.FluentTest.UnitTests
{  
    /// <summary>
    ///This is a test class for IFluentTestTest and is intended
    ///to contain all IFluentTestTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IFluentTestCollectionIsNotNullTest
    {

        #region IsNotNull
        // redundant as can't pass IsACollection if null
        [TestMethod]
        public void IsNotNullPasses()
        {
            var testCollection = new List<string>();
            Verify.That(testCollection).IsACollectionThat().IsNotNull();          
        }
        
        #endregion
    }
}
