using Munq.LifetimeManagers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Munq;
using Munq.FluentTest;

namespace Munq.Test
{
    /// <summary>
    ///This is a test class for AlwaysNewLifetimeTest and is intended
    ///to contain all AlwaysNewLifetimeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AlwaysNewLifetimeTest
    {
        /// <summary>
        /// Verify that Can Set the DefaultLifetimeManager To AlwaysNewLifetime
        ///</summary>
		[TestMethod()]
		public void CanSetDefaultLifetimeManagerToAlwaysNew()
		{
			using (var iocContainer = new IocContainer())
			{
				var lifetime = new AlwaysNewLifetime();
				iocContainer.UsesDefaultLifetimeManagerOf(lifetime);

				Verify.That(iocContainer.DefaultLifetimeManager).IsTheSameObjectAs(lifetime);
			}
		}

        /// <summary>
        /// Verifies that the AlwaysNew LifetimeManager Always Returns a New Instance
        ///</summary>
		[TestMethod()]
		public void AlwayNewLifetimeManagerAlwaysReturnsNewInstance()
		{
			using (var iocContainer = new IocContainer())
			{
				var lifetime = new AlwaysNewLifetime();
				iocContainer.UsesDefaultLifetimeManagerOf(lifetime);
				iocContainer.Register<IFoo>(c => new Foo1());

				var foo1 = iocContainer.Resolve<IFoo>();
				var foo2 = iocContainer.Resolve<IFoo>();
				var foo3 = iocContainer.Resolve<IFoo>();

				Verify.That(foo1).IsAnInstanceOfType(typeof(IFoo))
							.IsNotTheSameObjectAs(foo2)
							.IsNotTheSameObjectAs(foo3);

				Verify.That(foo2).IsAnInstanceOfType(typeof(IFoo))
							.IsNotTheSameObjectAs(foo3);

				Verify.That(foo3).IsAnInstanceOfType(typeof(IFoo));
			}
		}

		/// <summary>
		/// Verifies that the AlwaysNew LifetimeManager Always Returns a New Instance
		///</summary>
		[TestMethod()]
		public void AlwayNewLifetimeManagerExtensionAlwaysReturnsNewInstance()
		{
			using (var iocContainer = new IocContainer())
			{
				iocContainer.Register<IFoo>(c => new Foo1()).AsAlwaysNew();

				var foo1 = iocContainer.Resolve<IFoo>();
				var foo2 = iocContainer.Resolve<IFoo>();
				var foo3 = iocContainer.Resolve<IFoo>();

				Verify.That(foo1).IsAnInstanceOfType(typeof(IFoo))
							.IsNotTheSameObjectAs(foo2)
							.IsNotTheSameObjectAs(foo3);

				Verify.That(foo2).IsAnInstanceOfType(typeof(IFoo))
							.IsNotTheSameObjectAs(foo3);

				Verify.That(foo3).IsAnInstanceOfType(typeof(IFoo));
			}
		}
    }
}
