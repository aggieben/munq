using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Munq.FluentTest
{
    public partial class FluentTestObject : IFluentTestString
    {
        private string StringToTest { get { return ObjectToTest as string; } }
        private StringComparison ComparisonMode = StringComparison.CurrentCulture;

        #region IFluentStringTest methods
        IFluentTestString IFluentTestString.WithFailureMessage(string msg)
        {
            ErrorMessage = msg;
            return this;
        }
        
        public IFluentTestString UsingStringComparison(StringComparison comparisonMode)
        {
            ComparisonMode = comparisonMode;
            return this;
        }

        private static void CheckStringToCompareNotNull(string stringToCompare)
        {
           if(string.IsNullOrEmpty(stringToCompare))
            Verify.Fail();
        }
        
        IFluentTestString IFluentTestString.Contains(string stringToCompare)
        {
            CheckStringToCompareNotNull(stringToCompare);
            if (!StringToTest.Contains(stringToCompare))
                Verify.Fail();
            return this;
        }

        IFluentTestString IFluentTestString.DoesNotContain(string stringToCompare)
        {
            CheckStringToCompareNotNull(stringToCompare);
            if (StringToTest.Contains(stringToCompare))
                Verify.Fail();
            return this;
        }

        IFluentTestString IFluentTestString.DoesNotMatch(Regex regex)
        {
            if (regex == null)
                Verify.Fail();
            if (regex.IsMatch(StringToTest))
                Verify.Fail();
            return this;
        }

        IFluentTestString IFluentTestString.Matches(Regex regex)
        {
            if (regex == null)
                Verify.Fail();
            if (!regex.IsMatch(StringToTest))
                Verify.Fail();
            return this;
        }

        IFluentTestString IFluentTestString.StartsWith(string stringToCompare)
        {
            CheckStringToCompareNotNull(stringToCompare);
            if (!StringToTest.StartsWith(stringToCompare, ComparisonMode))
                Verify.Fail();
            return this;
        }

         IFluentTestString IFluentTestString.DoesNotStartWith(string stringToCompare)
        {
            CheckStringToCompareNotNull(stringToCompare);
            if (StringToTest.StartsWith(stringToCompare, ComparisonMode))
                Verify.Fail();
            return this;
        }
        
        IFluentTestString IFluentTestString.EndsWith(string stringToCompare)
        {
            CheckStringToCompareNotNull(stringToCompare);
            if (!StringToTest.EndsWith(stringToCompare, ComparisonMode))
                Verify.Fail();
            return this;
        }

        IFluentTestString IFluentTestString.DoesNotEndWith(string stringToCompare)
        {
            CheckStringToCompareNotNull(stringToCompare);
            if (StringToTest.EndsWith(stringToCompare, ComparisonMode))
                Verify.Fail();
            return this;
        }

        IFluentTestString IFluentTestString.IsEmpty()
        {
            if (StringToTest != string.Empty)
                Verify.Fail();
            return this;
        }

        IFluentTestString IFluentTestString.IsNotEmpty()
        {
            if (StringToTest == string.Empty)
                Verify.Fail();
            return this;
        }
        #endregion

        #region IFluentStringTest Members


        IFluentTestString IFluentTestString.IsEqualTo(string stringToCompare)
        {
            CheckStringToCompareNotNull(stringToCompare);
            if (!StringToTest.Equals(stringToCompare, ComparisonMode))
                Verify.Fail();
             return this;
        }

        IFluentTestString IFluentTestString.IsNotEqualTo(string stringToCompare)
        {
            CheckStringToCompareNotNull(stringToCompare);
            if (StringToTest.Equals(stringToCompare, ComparisonMode))
                Verify.Fail();
            return this;
        }

        #endregion
    }
}
