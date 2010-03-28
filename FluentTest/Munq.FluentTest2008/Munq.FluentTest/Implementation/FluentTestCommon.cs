
namespace Munq.FluentTest
{
    public partial class FluentTestObject : IFluentTestCommon, IFluentTest, IFluentStringTest, IFluentCollectionTest
    {
        protected readonly object ObjectToTest;
        protected string ErrorMessage;

        public FluentTestObject(object objectToTest)
        {
            ObjectToTest = objectToTest;
            ErrorMessage = string.Empty;
        }

        #region IFluentTestCommon members
        public void Fail()
        {
            if (string.IsNullOrEmpty(ErrorMessage))
                Verify.Fail();
            else
                Verify.Fail(ErrorMessage);
        }

        public void Inconclusive()
        {
            if (string.IsNullOrEmpty(ErrorMessage))
                Verify.InConclusive();
            else
                Verify.InConclusive(ErrorMessage);
        }

        public void IsNull()
        {
            if(ObjectToTest != null)
                Verify.Fail();
        }
        #endregion
    }
}
