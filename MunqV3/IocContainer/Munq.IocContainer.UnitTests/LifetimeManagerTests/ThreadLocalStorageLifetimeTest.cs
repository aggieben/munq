using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq.FluentTest;
using Munq.LifetimeManagers;

namespace Munq.Test
{      
    /// <summary>
    ///This is a test class for ThreadLocalStorageLifetimeTest and is intended
    ///to contain all ThreadLocalLifetimeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ThreadLocalStorageLifetimeTest
    {
        /// <summary>
        /// Verify that Can Set the DefaultLifetimeManager To ThreadLocalStorageLifetime
        ///</summary>
        [TestMethod()]
        public void CanSetDefaultLifetimeManagerToThreadLocalStorageLifetime()
        {
			using (var iocContainer = new IocContainer())
			{
				var lifetime = new ThreadLocalStorageLifetime();
				iocContainer.UsesDefaultLifetimeManagerOf(lifetime);

				Verify.That(iocContainer.DefaultLifetimeManager).IsTheSameObjectAs(lifetime);
			}
        }

        /// <summary>
        /// verify Request Lifetime returns same instance for same request, 
        /// different for different request
        /// </summary>
        [TestMethod]
        public void ThreadLocalStorageLifetimeManagerReturnsSameObjectForSameRequest()
        {
            var requestltm = new ThreadLocalStorageLifetime();
            using (var container = new IocContainer())
            {
                container.Register<IFoo>(c => new Foo1()).WithLifetimeManager(requestltm);
                IFoo result1 = container.Resolve<IFoo>();
                IFoo result2 = container.Resolve<IFoo>();
                IFoo result3 = null;
                IFoo result4 = null;
                // get values on a different thread
                var t = Task.Factory.StartNew(() =>
                {
                    result3 = container.Resolve<IFoo>();
                    result4 = container.Resolve<IFoo>();
                });
                t.Wait();
                // check the results
                Verify.That(result3).IsNotNull();
                Verify.That(result4).IsNotNull().IsTheSameObjectAs(result3);
                Verify.That(result2).IsNotNull();
                Verify.That(result1).IsNotNull().IsTheSameObjectAs(result2).IsNotTheSameObjectAs(result3);
            }
        }

		/// <summary>
		/// verify Request Lifetime returns same instance for same request, 
		/// different for different request
		/// </summary>
		[TestMethod]
		public void ThreadLocalStorageLifetimeExtensionManagerReturnsSameObjectForSameRequest()
		{
			using (var container = new IocContainer())
			{
				container.Register<IFoo>(c => new Foo1()).AsThreadSingleton();
				IFoo result1 = container.Resolve<IFoo>();
				IFoo result2 = container.Resolve<IFoo>();
				IFoo result3 = null;
				IFoo result4 = null;
				// get values on a different thread
				var t = Task.Factory.StartNew(() =>
				{
					result3 = container.Resolve<IFoo>();
					result4 = container.Resolve<IFoo>();
				});
				t.Wait();
				// check the results
				Verify.That(result3).IsNotNull();
				Verify.That(result4).IsNotNull().IsTheSameObjectAs(result3);
				Verify.That(result2).IsNotNull();
				Verify.That(result1).IsNotNull().IsTheSameObjectAs(result2).IsNotTheSameObjectAs(result3);
			}
		}
		
		[TestMethod]
        public void ShouldResolveGenerics()
        {
            var lifetime = new ThreadLocalStorageLifetime();
            using (var container = new Munq.IocContainer())
            {
                container.Register<IFoo<int>, Foo<int>>().WithLifetimeManager(lifetime);
                container.Register<IFoo<string>, Foo<string>>().WithLifetimeManager(lifetime);
                Assert.IsNotNull(container.Resolve<IFoo<int>>());
                // works
                Assert.IsNotNull(container.Resolve<IFoo<string>>());
            }  // fails

        }
    }
}
