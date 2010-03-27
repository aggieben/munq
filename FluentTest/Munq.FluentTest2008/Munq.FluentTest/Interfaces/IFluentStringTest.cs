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
        /// The assertion fails if the string is not empty.
        /// </summary>
        IFluentStringTest IsEmpty();
        
        /// <summary>
        /// The assertion fails if the string is not empty.
        /// </summary>
        /// <param name="msg">The message to display.</param>
        IFluentStringTest IsEmpty(string msg);

        /// <summary>
        /// The assertion fails if the string is empty.
        /// </summary>
        IFluentStringTest IsNotEmpty();

        /// <summary>
        /// The assertion fails if the string is empty.
        /// </summary>
        /// <param name="msg">The message to display.</param>
        IFluentStringTest IsNotEmpty(string msg);

        /// <summary>
        /// The assertion fails if the string under test does not contain the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        IFluentStringTest Contains(string stringToCompare);

        /// <summary>
        /// The assertion fails if the string under test does not contain the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        /// <param name="msg">The message to display.</param>
        IFluentStringTest Contains(string stringToCompare, string msg);

        /// <summary>
        /// The assertion fails if the string under test contains the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        IFluentStringTest DoesNotContain(string stringToCompare);

        /// <summary>
        /// The assertion fails if the string under test contains the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        /// <param name="msg">The message to display.</param>
        IFluentStringTest DoesNotContain(string stringToCompare, string msg);

        /// <summary>
        /// The assertion fails if the string under test does not match the regular expression.
        /// </summary>
        /// <param name="regex">The regular expression to apply to the string under test.</param>
        IFluentStringTest Matches(Regex regex);

        /// <summary>
        /// The assertion fails if the string under test does not match the regular expression.
        /// </summary>
        /// <param name="regex">The regular expression to apply to the string under test.</param>
        /// <param name="msg">The message to display.</param>
        IFluentStringTest Matches(Regex regex, string msg);

        /// <summary>
        /// The assertion fails if the string under test matches the regular expression.
        /// </summary>
        /// <param name="regex">The regular expression to apply to the string under test.</param>
        IFluentStringTest DoesNotMatch(Regex regex);

        /// <summary>
        /// The assertion fails if the string under test matches the regular expression.
        /// </summary>
        /// <param name="regex">The regular expression to apply to the string under test.</param>
        /// <param name="msg">The message to display.</param>
        IFluentStringTest DoesNotMatch(Regex regex, string msg);

        /// <summary>
        /// The assertion fails if the string under test does not start with the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        IFluentStringTest StartsWith(string stringToCompare);

        /// <summary>
        /// The assertion fails if the string under test does not start with the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        /// <param name="msg">The message to display.</param>
        IFluentStringTest StartsWith(string stringToCompare, string msg);

        /// <summary>
        /// The assertion fails if the string under test starts with the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        IFluentStringTest DoesNotStartsWith(string stringToCompare);

        /// <summary>
        /// The assertion fails if the string under test starts with the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        /// <param name="msg">The message to display.</param>
        IFluentStringTest DoesNotStartsWith(string stringToCompare, string msg);

        ///<summary>
        /// The assertion fails if the string under test does not end with the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        IFluentStringTest EndsWith(string stringToCompare);

        /// <summary>
        /// The assertion fails if the string under test does not end with the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        /// <param name="msg">The message to display.</param>
        IFluentStringTest EndsWith(string stringToCompare, string msg);

        ///<summary>
        /// The assertion fails if the string under test ends with the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        IFluentStringTest DoesNotEndsWith(string stringToCompare);

        /// <summary>
        /// The assertion fails if the string under test ends with the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        /// <param name="msg">The message to display.</param>
        IFluentStringTest DoesNotEndsWith(string stringToCompare, string msg);
    }
}
