using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Munq.FluentTest
{
    class MsTestProvider : IFluentTestProvider
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

        #endregion
    }
}
