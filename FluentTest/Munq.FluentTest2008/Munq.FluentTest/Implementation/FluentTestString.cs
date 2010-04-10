#region Copyright Notice
// Copyright 2010 by Matthew Dennis
#endregion

using System;
using System.Text.RegularExpressions;

namespace Munq.FluentTest
{
    public partial class FluentTestObject : IFluentTestString
    {
        private string StringToTest { get { return ObjectToTest as string; } }
        private StringComparison ComparisonMode = StringComparison.CurrentCulture;

        private void CheckStringToCompareNotNull(string stringToCompare)
        {
           if(stringToCompare == null)
                FailWithDefaultMessage("can't be compared to [null]"); 
        }

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

        IFluentTestString IFluentTestString.IsEqualTo(string stringToCompare)
        {
            CheckStringToCompareNotNull(stringToCompare);
            if (!StringToTest.Equals(stringToCompare, ComparisonMode))
                FailWithObjectToCompareDefaultMessage("should be equal to", stringToCompare);
            return this;
        }

        IFluentTestString IFluentTestString.IsNotEqualTo(string stringToCompare)
        {
            CheckStringToCompareNotNull(stringToCompare);
            if (StringToTest.Equals(stringToCompare, ComparisonMode))
                FailWithObjectToCompareDefaultMessage("should not be equal to", stringToCompare);
            return this;
        }
        
        IFluentTestString IFluentTestString.Contains(string stringToCompare)
        {
            CheckStringToCompareNotNull(stringToCompare);
            if (!StringToTest.Contains(stringToCompare))
                FailWithObjectToCompareDefaultMessage("should contain", stringToCompare);
            return this;
        }

        IFluentTestString IFluentTestString.DoesNotContain(string stringToCompare)
        {
            CheckStringToCompareNotNull(stringToCompare);
            if (StringToTest.Contains(stringToCompare))
                FailWithObjectToCompareDefaultMessage("should not contain", stringToCompare);
            return this;
        }

        IFluentTestString IFluentTestString.DoesNotMatch(Regex regex)
        {
            if (regex == null)
                FailWithDefaultMessage("can't be compared to [null]");
                
            if (regex.IsMatch(StringToTest))
                FailWithObjectToCompareDefaultMessage("should not match", regex);
            return this;
        }
 
        IFluentTestString IFluentTestString.Matches(Regex regex)
        {
            if (regex == null)
                FailWithDefaultMessage("can't be compared to [null]");
                
            if (!regex.IsMatch(StringToTest))
                FailWithObjectToCompareDefaultMessage("should match", regex);
            return this;
        }

        IFluentTestString IFluentTestString.StartsWith(string stringToCompare)
        {
            CheckStringToCompareNotNull(stringToCompare);
            if (!StringToTest.StartsWith(stringToCompare, ComparisonMode))
                FailWithObjectToCompareDefaultMessage("should start with", stringToCompare);
            return this;
        }

         IFluentTestString IFluentTestString.DoesNotStartWith(string stringToCompare)
        {
            CheckStringToCompareNotNull(stringToCompare);
            if (StringToTest.StartsWith(stringToCompare, ComparisonMode))
                FailWithObjectToCompareDefaultMessage("should not start with", stringToCompare);
            return this;
        }
        
        IFluentTestString IFluentTestString.EndsWith(string stringToCompare)
        {
            CheckStringToCompareNotNull(stringToCompare);
            if (!StringToTest.EndsWith(stringToCompare, ComparisonMode))
                FailWithObjectToCompareDefaultMessage("should end with", stringToCompare);
            return this;
        }

        IFluentTestString IFluentTestString.DoesNotEndWith(string stringToCompare)
        {
            CheckStringToCompareNotNull(stringToCompare);
            if (StringToTest.EndsWith(stringToCompare, ComparisonMode))
                FailWithObjectToCompareDefaultMessage("should not end with", stringToCompare);
            return this;
        }

        IFluentTestString IFluentTestString.IsEmpty()
        {
            if (StringToTest != string.Empty)
                FailWithDefaultMessage("should be empty");
            return this;
        }

        IFluentTestString IFluentTestString.IsNotEmpty()
        {
            if (StringToTest == string.Empty)
                FailWithDefaultMessage("should not be empty");
            return this;
        }

        #endregion
    }
}
