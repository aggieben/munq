#region Copyright Notice
// Copyright 2010 by Matthew Dennis
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.FluentTest
{
    public partial class FluentTestObject : IFluentTestCommon
    {
        private void FailWithDefaultMessage(string msg)
        {
            string entireMessage = string.Format("[{0}] {1}", ObjectToTest ?? "null", msg);
            ErrorMessage = ErrorMessage ?? entireMessage;
            Fail();
        }

        private void FailWithDefaultMessage(string template, params object[] args)
        {
            string endOfMessage = string.Format(template, args);
            FailWithDefaultMessage(endOfMessage);
        }
        
        private void FailWithObjectToCompareDefaultMessage(string errorStr, object objectToCompare)
        {
            FailWithDefaultMessage("{0} [{1}]",
                errorStr,
                objectToCompare ?? "null");
        }

        private void FailWithTypeDefaultMessage(string errorStr, Type type)
        {
            FailWithDefaultMessage("{0} [{1}]",
                errorStr,
                type == null ? "null" : type.Name);
        }
    }
}
