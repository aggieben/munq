
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
            return new FluentTestObject(objectToTest);
        }
        
    }
}
