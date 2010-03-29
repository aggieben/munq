using System;
using System.Text.RegularExpressions;

namespace Munq.FluentTest
{
    /// <summary>
    /// Methods for testing string.
    /// </summary>
    public interface IFluentTestString : IFluentTestCommon
    {
        /// <summary>
        /// Specifies a message to use on failure.
        /// </summary>
        /// <param name="msg"The message to use.</param>
        IFluentTestString WithFailureMessage(string msg);
        
        /// <summary>
        /// Sets the Comparison mode.
        /// </summary>
        /// <param name="comparisonMode"></param>
        /// <returns></returns>
        IFluentTestString UsingStringComparison(StringComparison comparisonMode);

        /// The assertion fails if the string is not empty.
        /// </summary>
        IFluentTestString IsEmpty();
        
        /// <summary>
        /// The assertion fails if the string is empty.
        /// </summary>
        IFluentTestString IsNotEmpty();

        /// <summary>
        /// The Assertion fails if the strings are not equal
        /// </summary>
        /// <param name="stringToCompare">The string to compare to the string under test.</param>
        IFluentTestString IsEqualTo(string stringToCompare);


        /// <summary>
        /// The Assertion fails if the strings are equal
        /// </summary>
        /// <param name="stringToCompare">The string to compare to the string under test.</param>
        IFluentTestString IsNotEqualTo(string stringToCompare);

        /// <summary>
        /// The assertion fails if the string under test does not contain the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        IFluentTestString Contains(string stringToCompare);

        /// <summary>
        /// The assertion fails if the string under test contains the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        IFluentTestString DoesNotContain(string stringToCompare);

        /// <summary>
        /// The assertion fails if the string under test does not match the regular expression.
        /// </summary>
        /// <param name="regex">The regular expression to apply to the string under test.</param>
        IFluentTestString Matches(Regex regex);

        /// <summary>
        /// The assertion fails if the string under test matches the regular expression.
        /// </summary>
        /// <param name="regex">The regular expression to apply to the string under test.</param>
        IFluentTestString DoesNotMatch(Regex regex);

        /// <summary>
        /// The assertion fails if the string under test does not start with the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        IFluentTestString StartsWith(string stringToCompare);

        /// <summary>
        /// The assertion fails if the string under test starts with the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        IFluentTestString DoesNotStartWith(string stringToCompare);

        ///<summary>
        /// The assertion fails if the string under test does not end with the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        IFluentTestString EndsWith(string stringToCompare);

        ///<summary>
        /// The assertion fails if the string under test ends with the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        IFluentTestString DoesNotEndsWith(string stringToCompare);
    }
}
