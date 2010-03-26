
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
            Verify.Provider.Fail();
        }

        public void Fail(string msg)
        {
            Verify.Provider.Fail(msg);
        }

        public void Inconclusive()
        {
            Verify.Provider.InConclusive();
        }

        public void Inconclusive(string msg)
        {
            Verify.Provider.InConclusive(msg);
        }

        public void IsNull()
        {
            if(ObjectToTest != null)
                Verify.Provider.Fail();
        }

        public void IsNull(string msg)
        {
            if (ObjectToTest != null)
                Verify.Provider.Fail(msg);
        }

        #endregion
    }
}
