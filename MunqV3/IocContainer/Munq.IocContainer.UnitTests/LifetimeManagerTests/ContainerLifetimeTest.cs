using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq.FluentTest;
using Munq.LifetimeManagers;

namespace Munq.Test
{    
    /// <summary>
    ///This is a test class for ContainerLifetimeTest and is intended
    ///to contain all ContainerLifetimeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ContainerLifetimeTest
    {

        /// <summary>
        /// Verify that Can Set the DefaultLifetimeManager To ContainerLifetime
        ///</summary>
		[TestMethod()]
		public void CanSetDefaultLifetimeManagerToContainerLifetime()
		{
			using (var iocContainer = new IocContainer())
			{
				var lifetime = new ContainerLifetime();
				iocContainer.UsesDefaultLifetimeManagerOf(lifetime);

				Verify.That(iocContainer.DefaultLifetimeManager).IsTheSameObjectAs(lifetime);
			}
		}

        /// <summary>
        /// Verifies that the Container LifetimeManager Always Returns a Same Instance
        ///</summary>
		[TestMethod()]
		public void ContainerLifetimeManagerAlwaysReturnsSameInstance()
		{
			using (var iocContainer = new IocContainer())
			{
				var lifetime = new ContainerLifetime();
				iocContainer.UsesDefaultLifetimeManagerOf(lifetime);
				iocContainer.Register<IFoo>(c => new Foo1());

				var result1 = iocContainer.Resolve<IFoo>();
				var result2 = iocContainer.Resolve<IFoo>();
				var result3 = iocContainer.Resolve<IFoo>();

				Verify.That(result3).IsNotNull();
				Verify.That(result2).IsNotNull();
				Verify.That(result1).IsNotNull()
							.IsTheSameObjectAs(result2)
							.IsTheSameObjectAs(result3);
			}
		}

		/// <summary>
		/// Verifies that the Container LifetimeManager Always Returns a Same Instance
		///</summary>
		[TestMethod()]
		public void ContainerLifetimeManagerExtensionAlwaysReturnsSameInstance()
		{
			using (var iocContainer = new IocContainer())
			{
				iocContainer.Register<IFoo>(c => new Foo1()).AsContainerSingleton();

				var result1 = iocContainer.Resolve<IFoo>();
				var result2 = iocContainer.Resolve<IFoo>();
				var result3 = iocContainer.Resolve<IFoo>();

				Verify.That(result3).IsNotNull();
				Verify.That(result2).IsNotNull();
				Verify.That(result1).IsNotNull()
							.IsTheSameObjectAs(result2)
							.IsTheSameObjectAs(result3);
			}
		}
    }
}
