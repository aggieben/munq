using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq.FluentTest;

namespace Munq.Test
{
    /// <summary>
    ///This is a test class for DefaultLifetimeTest and is intended
    ///to contain all Unit Tests for the default Lifetime
    ///</summary>
    [TestClass()]
    public class DefaultLifetimeTest
    {
        /// <summary>
        /// Verify that the default LifetimeManager is null
        ///</summary>
        [TestMethod()]
        public void DefaultLifetimeMangerIsNull()
        {
			using (var iocContainer = new IocContainer())
			{
				Assert.IsNull(iocContainer.DefaultLifetimeManager);
			}
        }

        /// <summary>
        /// Verifies that the Default LifetimeManager Always Returns a New Instance
        ///</summary>
		[TestMethod()]
		public void DefaultLifetimeManagerAlwaysReturnsNewInstance()
		{
			using (var iocContainer = new IocContainer())
			{
				iocContainer.Register<IFoo>(c => new Foo1());

				var result1 = iocContainer.Resolve<IFoo>();
				var result2 = iocContainer.Resolve<IFoo>();
				var result3 = iocContainer.Resolve<IFoo>();

				Verify.That(result3).IsNotNull();
				Verify.That(result2).IsNotNull()
							.IsNotTheSameObjectAs(result3);
				Verify.That(result1).IsNotNull()
							.IsNotTheSameObjectAs(result2)
							.IsNotTheSameObjectAs(result3);
			}
		}
    }
}
