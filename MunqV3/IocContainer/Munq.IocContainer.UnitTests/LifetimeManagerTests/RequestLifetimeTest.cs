using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq.FluentTest;
using Munq.LifetimeManagers;
using MvcFakes;

namespace Munq.Test
{      
    /// <summary>
    ///This is a test class for RequestLifetimeTest and is intended
    ///to contain all RequestLifetimeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RequestLifetimeTest
    {

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Verify that Can Set the DefaultLifetimeManager To RequestLifetime
        ///</summary>
        [TestMethod()]
        public void CanSetDefaultLifetimeManagerToRequestLifetime()
        {
			var lifetime = new RequestLifetime();
			using (var iocContainer = new IocContainer())
			{
				iocContainer.UsesDefaultLifetimeManagerOf(lifetime);

				Verify.That(iocContainer.DefaultLifetimeManager).IsTheSameObjectAs(lifetime);
			}
        }

        /// <summary>
        /// verify Request Lifetime returns same instance for same request, 
        /// different for different request
        /// </summary>
        [TestMethod]
        public void RequestLifetimeManagerReturnsSameObjectForSameRequest()
        {
            var context1 = new FakeHttpContext("Http://fakeUrl1.com");
            var context2 = new FakeHttpContext("Http://fakeUrl2.com");

            var requestltm = new RequestLifetime();

            using (var container = new IocContainer())
            {
                container.Register<IFoo>(c => new Foo1()).WithLifetimeManager(requestltm);
				RequestLifetime.SetContext(context1);
                var result1 = container.Resolve<IFoo>();
                var result2 = container.Resolve<IFoo>();
				RequestLifetime.SetContext(context2);
                var result3 = container.Resolve<IFoo>();
                Verify.That(result3).IsNotNull();
                Verify.That(result2).IsNotNull();
                Verify.That(result1).IsNotNull().IsTheSameObjectAs(result2).IsNotTheSameObjectAs(result3);
            }
        }

		/// <summary>
		/// verify Request Lifetime returns same instance for same request, 
		/// different for different request
		/// </summary>
		[TestMethod]
		public void RequestLifetimeManagerExtensionReturnsSameObjectForSameRequest()
		{
			var context1 = new FakeHttpContext("Http://fakeUrl1.com");
			var context2 = new FakeHttpContext("Http://fakeUrl2.com");

			using (var container = new IocContainer())
			{
				container.Register<IFoo>(c => new Foo1()).AsRequestSingleton();
				LifetimeExtensions.HttpContext = context1;
				var result1 = container.Resolve<IFoo>();
				var result2 = container.Resolve<IFoo>();
				LifetimeExtensions.HttpContext = context2;
				var result3 = container.Resolve<IFoo>();
				Verify.That(result3).IsNotNull();
				Verify.That(result2).IsNotNull();
				Verify.That(result1).IsNotNull().IsTheSameObjectAs(result2).IsNotTheSameObjectAs(result3);
			}
		}

    }
}
