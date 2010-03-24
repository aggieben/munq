
namespace Munq.FluentTest
{
    public class Verify 
    {
        public static IFluentTest That(object objectToTest)
        {
            return new FluentTestObject(objectToTest);
        }
    }
}
