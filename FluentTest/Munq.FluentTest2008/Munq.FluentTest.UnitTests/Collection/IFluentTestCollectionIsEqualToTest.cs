#region Copyright Notice
// Copyright 2010 by Matthew Dennis
#endregion

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq.FluentTest;
using System.Collections.Generic;
using System.Collections;

namespace Munq.FluentTest.UnitTests
{  
    /// <summary>
    ///This is a test class for IFluentTestTest and is intended
    ///to contain all IFluentTestTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IFluentTestCollectionIsEqualToTest
    {
        #region IsEqualTo
        [TestMethod]
        public void IsEqualToPassesIfEqual()
        {
            var testCollection = new List<object>(new object[] { "One", DateTime.Now, 1 });
            var compareCollection = new List<object>(new object[] { "One", DateTime.Now, 1 });

            Verify.That(testCollection).IsACollectionThat().IsEqualTo(compareCollection);          
        }
        
        [TestMethod]
        public void IsEqualToFailsIfDifferentContents()
        {
            var testCollection = new List<object>(new object[] { "One", DateTime.Now, 1 });
            var compareCollection = new List<object>(new object[] { "One", DateTime.Now, 2 });
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testCollection).IsACollectionThat().IsEqualTo(compareCollection)
            );
        }

        [TestMethod]
        public void IsEqualToFailsIfDifferentLengths()
        {
            var testCollection = new List<object>(new object[] { "One", DateTime.Now, 1 });
            var compareCollection = new List<object>(new object[] { "One", DateTime.Now, 1, "Extra" });
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testCollection).IsACollectionThat().IsEqualTo(compareCollection)
            );
        }
        [TestMethod]
        public void IsEqualToFailsIfCollectionToCompareIsNull()
        {
            var testCollection = new List<object>(new object[] { "One", DateTime.Now, 1 });
            ICollection compareCollection = null;
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testCollection).IsACollectionThat().IsEqualTo(compareCollection)
            );
        }
        #endregion

        #region IsNotEqualTo
        [TestMethod]
        public void IsNotEqualToPassesIfNotEqual()
        {
            var testCollection = new List<object>(new object[] { "One", DateTime.Now, 1 });
            var compareCollection = new List<object>(new object[] { "One", DateTime.Now, 2 });

            Verify.That(testCollection).IsACollectionThat().IsNotEqualTo(compareCollection);
        }

        [TestMethod]
        public void IsNotEqualToFailsIEqual()
        {
            var testCollection = new List<object>(new object[] { "One", DateTime.Now, 1 });
            var compareCollection = new List<object>(new object[] { "One", DateTime.Now, 1 });
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testCollection).IsACollectionThat().IsNotEqualTo(compareCollection)
            );
        }

        [TestMethod]
        public void IsNotEqualToPassesIfDifferentLengths()
        {
            var testCollection = new List<object>(new object[] { "One", DateTime.Now, 1 });
            var compareCollection = new List<object>(new object[] { "One", DateTime.Now, 1, "Extra" });
                Verify.That(testCollection).IsACollectionThat().IsNotEqualTo(compareCollection);
        }
        
        [TestMethod]
        public void IsNotEqualToFailsIfCollectionToCompareIsNull()
        {
            var testCollection = new List<object>(new object[] { "One", DateTime.Now, 1 });
            ICollection compareCollection = null;
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testCollection).IsACollectionThat().IsNotEqualTo(compareCollection)
            );
        }
        #endregion
    }
}
