using System.Web.SessionState;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq.FluentTest;
using Munq.LifetimeManagers;
using MvcFakes;

namespace Munq.Test
{
	/// <summary>
	///This is a test class for SessionLifetimeTest and is intended
	///to contain all SessionLifetimeTest Unit Tests
	///</summary>
	[TestClass()]
	public class SessionLifetimeTest
	{
		/// <summary>
		/// Verify that Can Set the DefaultLifetimeManager To RequestLifetime
		///</summary>
		[TestMethod()]
		public void CanSetDefaultLifetimeManagerToRequestLifetime()
		{
			using (var iocContainer = new IocContainer())
			{
				var lifetime = new RequestLifetime();
				iocContainer.UsesDefaultLifetimeManagerOf(lifetime);

				Verify.That(iocContainer.DefaultLifetimeManager).IsTheSameObjectAs(lifetime);
			}
		}

		/// <summary>
		/// verify Session Lifetime returns same instance for same session, 
		/// different for different sessions
		/// </summary>
		[TestMethod]
		public void SessionLifetimeManagerReturnsDifferentObjectForDifferentSession()
		{
			var sessionItems1 = new SessionStateItemCollection();
			var sessionItems2 = new SessionStateItemCollection();
			var context1 = new FakeHttpContext("Http://fakeUrl1.com", null, null, null, null, sessionItems1);
			var context2 = new FakeHttpContext("Http://fakeUrl2.com", null, null, null, null, sessionItems2);

			var sessionltm = new SessionLifetime();

			using (var container = new IocContainer())
			{
				container.Register<IFoo>(c => new Foo1()).WithLifetimeManager(sessionltm);
				sessionltm.SetContext(context1);
				var result1 = container.Resolve<IFoo>();
				var result2 = container.Resolve<IFoo>();
				sessionltm.SetContext(context2);
				var result3 = container.Resolve<IFoo>();
				Verify.That(result3).IsNotNull();
				Verify.That(result2).IsNotNull();
				Verify.That(result1).IsNotNull().IsTheSameObjectAs(result2).IsNotTheSameObjectAs(result3);
			}
		}

		/// <summary>
		/// verify Session Lifetime returns same instance for same session, 
		/// different for different sessions
		/// </summary>
		[TestMethod]
		public void SessionLifetimeManagerExtensionReturnsDifferentObjectForDifferentSession()
		{
			var sessionItems1 = new SessionStateItemCollection();
			var sessionItems2 = new SessionStateItemCollection();
			var context1 = new FakeHttpContext("Http://fakeUrl1.com", null, null, null, null, sessionItems1);
			var context2 = new FakeHttpContext("Http://fakeUrl2.com", null, null, null, null, sessionItems2);

			using (var container = new IocContainer())
			{
				container.Register<IFoo>(c => new Foo1()).AsSessionSingleton();
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