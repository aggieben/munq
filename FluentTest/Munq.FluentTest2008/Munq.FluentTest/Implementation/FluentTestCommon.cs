
namespace Munq.FluentTest
{
    public partial class FluentTestObject : IFluentTestCommon
    {
        protected readonly object ObjectToTest;
        protected string ErrorMessage;

        public FluentTestObject(object objectToTest)
        {
            ObjectToTest = objectToTest;
            ErrorMessage = null;
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
                FailWithDefaultMessage("The object under test was not null as expected.");
        }
        #endregion
        
        private void FailWithDefaultMessage(string msg)
        {
            ErrorMessage = ErrorMessage ?? msg;  
            Fail();
        }
    }
}
