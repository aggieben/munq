using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Munq.FluentTest
{
    public class MsTestProvider : IFluentTestProvider
    {
         
        #region IFluentTestProvider Members

        public void Fail()
        {
            Assert.Fail();
        }

        public void Fail(string msg)
        {
            Assert.Fail(msg);
        }

        public void InConclusive()
        {
            Assert.Inconclusive();
        }

        public void InConclusive(string msg)
        {
            Assert.Inconclusive(msg);
        }


        public Type FailExceptionType
        {
            get { return typeof(AssertFailedException); }
        }

        public Type InConclusiveExceptionType
        {
            get { return typeof(AssertInconclusiveException); }
        }

        #endregion
    }
}
