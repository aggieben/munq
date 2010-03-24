using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Munq.FluentTest
{
    public class FluentTestCommon : Munq.FluentTest.IFluentTestCommon
    {
        protected readonly object ObjectToTest;

        public FluentTestCommon(object objectToTest)
        {
            ObjectToTest = objectToTest;
        }

        #region IFluentTestCommon members
        public void Fail()
        {
            Assert.Fail();
        }

        public void Fail(string msg)
        {
            Assert.Fail(msg);
        }

        public void Inconclusive()
        {
            Assert.Inconclusive();
        }

        public void Inconclusive(string msg)
        {
            Assert.Inconclusive(msg);
        }

        public void IsNull()
        {
            Assert.IsNull(ObjectToTest);
        }

        public void IsNull(string msg)
        {
            Assert.IsNull(ObjectToTest, msg);
        }

        #endregion
    }
}
