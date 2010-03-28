using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Munq.FluentTest
{
    public partial class FluentTestObject : IFluentStringTest
    {
        private string StringToTest { get { return ObjectToTest as string; } }
        private StringComparison ComparisonMode = StringComparison.CurrentCulture;

        #region IFluentStringTest methods
        IFluentStringTest IFluentStringTest.WithFailureMessage(string msg)
        {
            ErrorMessage = msg;
            return this;
        }
        
        public IFluentStringTest UsingStringComparison(StringComparison comparisonMode)
        {
            ComparisonMode = comparisonMode;
            return this;
        }

        private static void CheckStringToCompareNotNull(string stringToCompare)
        {
           if(string.IsNullOrEmpty(stringToCompare))
            Verify.Fail();
        }
        
        IFluentStringTest IFluentStringTest.Contains(string stringToCompare)
        {
            CheckStringToCompareNotNull(stringToCompare);
            if (!StringToTest.Contains(stringToCompare))
                Verify.Fail();
            return this;
        }

        IFluentStringTest IFluentStringTest.DoesNotContain(string stringToCompare)
        {
            CheckStringToCompareNotNull(stringToCompare);
            if (StringToTest.Contains(stringToCompare))
                Verify.Fail();
            return this;
        }

        IFluentStringTest IFluentStringTest.DoesNotMatch(Regex regex)
        {
            if (regex == null)
                Verify.Fail();
            if (regex.IsMatch(StringToTest))
                Verify.Fail();
            return this;
        }

        IFluentStringTest IFluentStringTest.Matches(Regex regex)
        {
            if (regex == null)
                Verify.Fail();
            if (!regex.IsMatch(StringToTest))
                Verify.Fail();
            return this;
        }

        IFluentStringTest IFluentStringTest.StartsWith(string stringToCompare)
        {
            CheckStringToCompareNotNull(stringToCompare);
            if (!StringToTest.StartsWith(stringToCompare, ComparisonMode))
                Verify.Fail();
            return this;
        }

         IFluentStringTest IFluentStringTest.DoesNotStartWith(string stringToCompare)
        {
            CheckStringToCompareNotNull(stringToCompare);
            if (StringToTest.StartsWith(stringToCompare, ComparisonMode))
                Verify.Fail();
            return this;
        }
        
        IFluentStringTest IFluentStringTest.EndsWith(string stringToCompare)
        {
            CheckStringToCompareNotNull(stringToCompare);
            if (!StringToTest.EndsWith(stringToCompare, ComparisonMode))
                Verify.Fail();
            return this;
        }

        IFluentStringTest IFluentStringTest.DoesNotEndsWith(string stringToCompare)
        {
            CheckStringToCompareNotNull(stringToCompare);
            if (StringToTest.EndsWith(stringToCompare, ComparisonMode))
                Verify.Fail();
            return this;
        }

        IFluentStringTest IFluentStringTest.IsEmpty()
        {
            if (StringToTest != string.Empty)
                Verify.Fail();
            return this;
        }

        IFluentStringTest IFluentStringTest.IsNotEmpty()
        {
            if (StringToTest == string.Empty)
                Verify.Fail();
            return this;
        }
        #endregion

        #region IFluentStringTest Members


        IFluentStringTest IFluentStringTest.IsEqualTo(string stringToCompare)
        {
            CheckStringToCompareNotNull(stringToCompare);
            if (!StringToTest.Equals(stringToCompare, ComparisonMode))
                Verify.Fail();
             return this;
        }

        IFluentStringTest IFluentStringTest.IsNotEqualTo(string stringToCompare)
        {
            CheckStringToCompareNotNull(stringToCompare);
            if (StringToTest.Equals(stringToCompare, ComparisonMode))
                Verify.Fail();
            return this;
        }

        #endregion
    }
}
