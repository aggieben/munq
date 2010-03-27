using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Munq.FluentTest
{
    public class FluentTestString : FluentTestCommon, Munq.FluentTest.IFluentStringTest
    {
        private string StringToTest { get { return ObjectToTest as string; } }

        public FluentTestString(string stringToTest) : base(stringToTest)
        {
        }

        #region IFluentStringTest methods
        private bool _StringContains(string stringToCompare)
        {
            return stringToCompare != null && StringToTest.Contains(stringToCompare);
        }
        IFluentStringTest IFluentStringTest.Contains(string stringToCompare)
        {
            if (!_StringContains(stringToCompare))
                Verify.Provider.Fail();
            return this;
        }

        IFluentStringTest IFluentStringTest.Contains(string stringToCompare, string msg)
        {
            if (!_StringContains(stringToCompare))
                Verify.Provider.Fail(msg);
            return this;
        }

        private bool _StringDoesNotContain(string stringToCompare)
        {
            return stringToCompare != null && !StringToTest.Contains(stringToCompare);
        }
        IFluentStringTest IFluentStringTest.DoesNotContain(string stringToCompare)
        {
            if (!_StringDoesNotContain(stringToCompare))
                Verify.Provider.Fail();
            return this;
        }

        IFluentStringTest IFluentStringTest.DoesNotContain(string stringToCompare, string msg)
        {
            if (!_StringDoesNotContain(stringToCompare))
                Verify.Provider.Fail(msg);
            return this;
        }

        IFluentStringTest IFluentStringTest.DoesNotMatch(Regex regex)
        {
            StringAssert.DoesNotMatch(StringToTest, regex);
            return this;
        }

        IFluentStringTest IFluentStringTest.DoesNotMatch(Regex regex, string msg)
        {
            StringAssert.DoesNotMatch(StringToTest, regex, msg);
            return this;
        }

        IFluentStringTest IFluentStringTest.Matches(Regex regex)
        {
            StringAssert.Matches(StringToTest, regex);
            return this;
        }

        IFluentStringTest IFluentStringTest.Matches(Regex regex, string msg)
        {
            StringAssert.Matches(StringToTest, regex, msg);
            return this;
        }

        IFluentStringTest IFluentStringTest.StartsWith(string stringToCompare)
        {
            StringAssert.StartsWith(StringToTest, stringToCompare);
            return this;
        }

        IFluentStringTest IFluentStringTest.StartsWith(string stringToCompare, string msg)
        {
            StringAssert.StartsWith(StringToTest, stringToCompare, msg);
            return this;
        }

        IFluentStringTest IFluentStringTest.EndsWith(string stringToCompare)
        {
            StringAssert.EndsWith(StringToTest, stringToCompare);
            return this;
        }

        IFluentStringTest IFluentStringTest.EndsWith(string stringToCompare, string msg)
        {
            StringAssert.EndsWith(StringToTest, stringToCompare, msg);
            return this;
        }

         IFluentStringTest IFluentStringTest.DoesNotStartsWith(string stringToCompare)
        {
            Assert.IsTrue(!StringToTest.StartsWith(stringToCompare), "String under test starts with the string to compare.");
            return this;
        }

        IFluentStringTest IFluentStringTest.DoesNotStartsWith(string stringToCompare, string msg)
        {
            Assert.IsTrue(!StringToTest.StartsWith(stringToCompare), msg);
            return this;
        }

        IFluentStringTest IFluentStringTest.DoesNotEndsWith(string stringToCompare)
        {
            Assert.IsTrue(!StringToTest.EndsWith(stringToCompare), "String under test ends with the string to compare.");
            return this;
        }

        IFluentStringTest IFluentStringTest.DoesNotEndsWith(string stringToCompare, string msg)
        {
            Assert.IsTrue(!StringToTest.EndsWith(stringToCompare), msg);
            return this;
        }
         #endregion

        #region IFluentStringTest Members

        IFluentStringTest IFluentStringTest.IsEmpty()
        {
            throw new System.NotImplementedException();
        }
        IFluentStringTest IFluentStringTest.IsEmpty(string msg)
        {
            throw new System.NotImplementedException();
        }

        IFluentStringTest IFluentStringTest.IsNotEmpty()
        {
            throw new System.NotImplementedException();
        }
        IFluentStringTest IFluentStringTest.IsNotEmpty(string msg)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}
