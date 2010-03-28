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
                () => Verify.That(testString).IsAString().Matches(null)
            );
        }

        [TestMethod]
        public void MatchesFailsForNonMatchingRegEx()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAString().Matches(new Regex(@"\d\d\d"))
            );
        }

        [TestMethod]
        public void MatchesPassesForMatchingRegEx()
        {
            Verify.That(testString).IsAString().Matches(new Regex(@".*F.*"));
        }
        #endregion

        #region DoesNotMatch
        [TestMethod]
        public void DoesNotMatchFailsForNullCompareRegEx()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAString().DoesNotMatch(null)
            );
        }

        [TestMethod]
        public void DoesNotMatchFailsForMatchingRegEx()
        {
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testString).IsAString().DoesNotMatch(new Regex(@"^May.*\.$"))
            );
        }

        [TestMethod]
        public void DoesNotMatchPassesForNonMatchingRegEx()
        {
            Verify.That(testString).IsAString().DoesNotMatch(new Regex("Jedi"));
        }
        #endregion


    }
}
