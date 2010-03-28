using System;
using System.Text.RegularExpressions;

namespace Munq.FluentTest
{
    /// <summary>
    /// Methods for testing string.
    /// </summary>
    public interface IFluentStringTest : IFluentTestCommon
    {
        /// <summary>
        /// Specifies a message to use on failure.
        /// </summary>
        /// <param name="msg"The message to use.</param>
        IFluentStringTest WithFailureMessage(string msg);
        
        /// <summary>
        /// Sets the Comparison mode.
        /// </summary>
        /// <param name="comparisonMode"></param>
        /// <returns></returns>
        IFluentStringTest UsingStringComparison(StringComparison comparisonMode);

        /// The assertion fails if the string is not empty.
        /// </summary>
        IFluentStringTest IsEmpty();
        
        /// <summary>
        /// The assertion fails if the string is empty.
        /// </summary>
        IFluentStringTest IsNotEmpty();

        IFluentStringTest IsEqual(string stringToCompare);
        IFluentStringTest IsNotEqual(string stringToCompare);

        /// <summary>
        /// The assertion fails if the string under test does not contain the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        IFluentStringTest Contains(string stringToCompare);

        /// <summary>
        /// The assertion fails if the string under test contains the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        IFluentStringTest DoesNotContain(string stringToCompare);

        /// <summary>
        /// The assertion fails if the string under test does not match the regular expression.
        /// </summary>
        /// <param name="regex">The regular expression to apply to the string under test.</param>
        IFluentStringTest Matches(Regex regex);

        /// <summary>
        /// The assertion fails if the string under test matches the regular expression.
        /// </summary>
        /// <param name="regex">The regular expression to apply to the string under test.</param>
        IFluentStringTest DoesNotMatch(Regex regex);

        /// <summary>
        /// The assertion fails if the string under test does not start with the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        IFluentStringTest StartsWith(string stringToCompare);

        /// <summary>
        /// The assertion fails if the string under test starts with the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        IFluentStringTest DoesNotStartsWith(string stringToCompare);

        ///<summary>
        /// The assertion fails if the string under test does not end with the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        IFluentStringTest EndsWith(string stringToCompare);

        ///<summary>
        /// The assertion fails if the string under test ends with the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        IFluentStringTest DoesNotEndsWith(string stringToCompare);
    }
}
