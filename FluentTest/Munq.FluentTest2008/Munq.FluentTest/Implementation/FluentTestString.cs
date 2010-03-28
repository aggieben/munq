using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Munq.FluentTest
{
    public partial class FluentTestObject : IFluentTestCommon, IFluentTest, IFluentStringTest, IFluentCollectionTest
    {
        private string StringToTest { get { return ObjectToTest as string; } }

        #region IFluentStringTest methods
        private bool _StringContains(string stringToCompare)
        {
            return stringToCompare != null && StringToTest.Contains(stringToCompare);
        }
        IFluentStringTest IFluentStringTest.Contains(string stringToCompare)
        {
            if (!_StringContains(stringToCompare))
                Verify.Fail();
            return this;
        }

        private bool _StringDoesNotContain(string stringToCompare)
        {
            return stringToCompare != null && !StringToTest.Contains(stringToCompare);
        }
        IFluentStringTest IFluentStringTest.DoesNotContain(string stringToCompare)
        {
            if (!_StringDoesNotContain(stringToCompare))
                Verify.Fail();
            return this;
        }

        IFluentStringTest IFluentStringTest.DoesNotMatch(Regex regex)
        {
            StringAssert.DoesNotMatch(StringToTest, regex);
            return this;
        }

        IFluentStringTest IFluentStringTest.Matches(Regex regex)
        {
            StringAssert.Matches(StringToTest, regex);
            return this;
        }

        IFluentStringTest IFluentStringTest.StartsWith(string stringToCompare)
        {
            StringAssert.StartsWith(StringToTest, stringToCompare);
            return this;
        }

        IFluentStringTest IFluentStringTest.EndsWith(string stringToCompare)
        {
            StringAssert.EndsWith(StringToTest, stringToCompare);
            return this;
        }

         IFluentStringTest IFluentStringTest.DoesNotStartsWith(string stringToCompare)
        {
            Assert.IsTrue(!StringToTest.StartsWith(stringToCompare), "String under test starts with the string to compare.");
            return this;
        }

        IFluentStringTest IFluentStringTest.DoesNotEndsWith(string stringToCompare)
        {
            Assert.IsTrue(!StringToTest.EndsWith(stringToCompare), "String under test ends with the string to compare.");
            return this;
        }

        IFluentStringTest IFluentStringTest.IsEmpty()
        {
            if (!string.IsNullOrEmpty(StringToTest))
                Verify.Fail();
            return this;
        }

        IFluentStringTest IFluentStringTest.IsNotEmpty()
        {
            if (string.IsNullOrEmpty(StringToTest))
                Verify.Fail();
            return this;
        }

        IFluentStringTest IFluentStringTest.WithFailureMessage(string msg)
        {
            ErrorMessage = msg;
            return this;
        }

        #endregion
    }
}
