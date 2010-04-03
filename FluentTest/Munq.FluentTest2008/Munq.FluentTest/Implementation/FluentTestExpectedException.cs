using System;

namespace Munq.FluentTest {
    /// <summary>
    /// Check that the expected exception is thrown.
    /// </summary>
    /// <remarks>
    /// Allows use to: 
    ///   Verify.TheExpectedException( typeof(KeyNotFoundException ).IsThrowWhen(
    ///     () => {
    ///         var testDictionary = new MyDictionary();
    ///         testDictionary["missingItem"]; 
    ///         });
    /// </remarks>
    public class FluentTestExpectedException : IFluentTestExpectedException, IFluentTestThrownException
    {
        private Type ExpectedExceptionType;
        private Exception thrownException = null;
        
        public FluentTestExpectedException(Type typeOfException)
        {
            ExpectedExceptionType = typeOfException;
        }

        public IFluentTestThrownException IsThrownWhen(Action whatToDo)
        {
            try
            {
                whatToDo();
            }
            catch(Exception ex)
            {
                Type actualExceptionType = ex.GetType();
                if (actualExceptionType != ExpectedExceptionType)
                {
                string msg = String.Format("Expected Exception {0} but received {1}", 
                                            ExpectedExceptionType.Name,
                                            actualExceptionType.Name);
                    Verify.Fail(msg);
                }
                else
                {
                    thrownException = ex;
                    return this; // got the expected Exception.
                }
            }
            
            Verify.Fail(string.Format("Did not receive expected Exception {0}", 
                                                ExpectedExceptionType.Name));
            return null;
        }

        IFluentTestString IFluentTestThrownException.AndHasAMessageThat()
        {
            return Verify.That(thrownException.Message).IsAStringThat();
        }

    }
}
