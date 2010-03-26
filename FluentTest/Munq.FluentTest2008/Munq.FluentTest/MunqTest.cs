using System;

namespace Munq.FluentTest
{
    public class Verify 
    {      
        public static IFluentTestProvider Provider { get; set;}
        
        static Verify()
        {
        }
            
        public static IFluentTest That(object objectToTest)
        {
            if (Provider == null)
                throw new NullReferenceException("Test Framework Provider not initialized");
                
            return new FluentTestObject(objectToTest);
        }
        
    }
}
