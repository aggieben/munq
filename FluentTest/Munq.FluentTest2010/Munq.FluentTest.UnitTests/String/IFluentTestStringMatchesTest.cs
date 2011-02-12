#region Copyright Notice
// Copyright 2010 by Matthew Dennis
#endregion

using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq.FluentTest;

namespace Munq.FluentTest.UnitTests
{  
    /// <summary>
    ///This is a test class for IFluentTestTest and is intended
    ///to contain all IFluentTestTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IFluentTestStringMatchesTest
    {
        private const string testString = "May the Force be with you.";
         #region Matches
        [TestMethod]
        public void MatchesFailsForNullCompareRegEx()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAStringThat().Matches(null)
            ).AndHasAMessageThat().Contains("[May the Force be with you.] can't be compared to [null]");
        }

        [TestMethod]
        public void MatchesFailsForNonMatchingRegEx()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAStringThat().Matches(new Regex(@"\d\d\d"))
            ).AndHasAMessageThat().Contains(@"[May the Force be with you.] should match [\d\d\d]");
        }

        [TestMethod]
        public void MatchesPassesForMatchingRegEx()
        {
            Verify.That(testString).IsAStringThat().Matches(new Regex(@".*F.*"));
        }
        #endregion

        #region DoesNotMatch
        [TestMethod]
        public void DoesNotMatchFailsForNullCompareRegEx()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAStringThat().DoesNotMatch(null)
            ).AndHasAMessageThat().Contains("[May the Force be with you.] can't be compared to [null]");
        }

        [TestMethod]
        public void DoesNotMatchFailsForMatchingRegEx()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAStringThat().DoesNotMatch(new Regex(@"^May.*\.$"))
            ).AndHasAMessageThat().Contains(@"[May the Force be with you.] should not match [^May.*\.$]");
        }

        [TestMethod]
        public void DoesNotMatchPassesForNonMatchingRegEx()
        {
            Verify.That(testString).IsAStringThat().DoesNotMatch(new Regex("Jedi"));
        }
        #endregion


    }
}
