using System;
using Munq.FluentTest.MsTest;

namespace Munq.FluentTest
{
    public class Verify
    {      
        public static IFluentTestProvider Provider { get; set;}
        
        static Verify()
        {
            Provider = new MsTestProvider();
        }
            
        public static IFluentTest That(object objectToTest)
        {
            CheckProviderInitialized();                
            return new FluentTestObject(objectToTest);
        }
        
        public static IFluentTestExpectedException TheExpectedException(Type typeOfExpectedException)
        {
            CheckProviderInitialized();
            return new FluentTestExpectedException(typeOfExpectedException);
        }
        
        private static void CheckProviderInitialized()
        {
            if (Provider == null)
                throw new NullReferenceException("Test Framework Provider not initialized");
        }

        #region IFluentTestProvider Members

        public static void Fail()
        {
            CheckProviderInitialized();
            Provider.Fail();
        }

        public static void Fail(string msg)
        {
            CheckProviderInitialized();
            Provider.Fail(msg);
        }

        public static void InConclusive()
        {
            CheckProviderInitialized();
            Provider.InConclusive();
        }

        public static void InConclusive(string msg)
        {
            CheckProviderInitialized();
            Provider.InConclusive(msg);
        }

        public static Type FailExceptionType
        {
            get
            {
                CheckProviderInitialized();    
                return Provider.FailExceptionType;
            }
        }

        public static Type InConclusiveExceptionType
        {
            get
            {
                CheckProviderInitialized();
                return Provider.InConclusiveExceptionType;
            }
        }
        #endregion
    }
}
