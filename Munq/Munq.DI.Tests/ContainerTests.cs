using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq.DI;
using Munq.DI.LifetimeManagers;

using MvcFakes;
using System.Web;
using System.Web.SessionState;

using System.Threading;
using System.Web.Caching;


namespace Munq.DI.Tests
{
    /// <summary>
    /// Summary description for ContainerTests
    /// </summary>
    [TestClass]
    public class ContainerTests
    {
        public ContainerTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        // verify that we can create a container object
        [TestMethod]
        public void CanCreateContainer()
        {
            var container = new Munq.DI.Container();
            Assert.IsNotNull(container);
        }

        // verify that we get an IRegistration from a Register call
        [TestMethod]
        public void RegisterReturnsIRegistration()
        {
            var container = new Container();

            var result = container.Register<IFoo>(c => new Foo1());

            Assert.IsNotNull(result);
            Assert.IsNotNull(result as IRegistration);
        }

        // verify that Register<TType> throws exception if passed null
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterNullFunctionForTypeThrowsException()
        {
            var container = new Container();
            container.Register<IFoo>(null);

        }

        // verify that Resolving a registered type returns an instance of the type
        [TestMethod]
        public void ResolveRegisteredTypeReturnsInstanceOfType()
        {
            var container = new Container();
            container.Register<IFoo>(c => new Foo1());

            var result = container.Resolve<IFoo>();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IFoo));
            Assert.IsInstanceOfType(result, typeof(Foo1));
        }

        // verify that resolving an unregistered type throw a notfound exception
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void ResolvingUnregisteredTypeThrowsException()
        {
            var container = new Container();
            container.Register<IFoo>(c => new Foo1());

            var result = container.Resolve<IBar>();
        }

        // verify that default Container returns new instances for each Resolve
        [TestMethod]
        public void MultipleResolvesReturnDifferentInstances()
        {
            var container = new Container();
            container.Register<IFoo>(c => new Foo1());

            var result1 = container.Resolve<IFoo>();
            var result2 = container.Resolve<IFoo>();

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.AreNotSame(result1, result2);
        }

        // verify that can register and resolve multiple types
        [TestMethod]
        public void CanRegisterAndResolveMultipleTypes()
        {
            var container = new Container();
            container.Register<IFoo>(c => new Foo1());
            container.Register<IBar>(c => new Bar1());

            var foo = container.Resolve<IFoo>();
            var bar = container.Resolve<IBar>();

            Assert.IsNotNull(foo);
            Assert.IsNotNull(bar);

            Assert.IsInstanceOfType(foo, typeof(IFoo));
            Assert.IsInstanceOfType(bar, typeof(IBar));
        }

        //verify can register and resolve named registrations
        [TestMethod]
        public void CanRegisterAndResolveNamedRegistrations()
        {
            var container = new Container();
            container.Register<IFoo>("named", c=>new Foo2());

            var result = container.Resolve<IFoo>("named");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IFoo));
            Assert.IsInstanceOfType(result, typeof(Foo2));
        }

        // verifying Resolving an unregistered named registration throws an exception. 
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void ResolvingUnregisteredNameRegistrationThrowsException()
        {
            var container = new Container();
            container.Register<IBar>(c => new Bar1());
            container.Register<IBar>("TheOtherBar", c => new Bar2());

            container.Resolve<IBar>("TheWrongName");
        }

        // verifying Registering a null function in an named registration throws an exception.
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisteringNullFunctionForNamedRegistrationThrowsException()
        {
            var container = new Container();

            container.Register<IFoo>("name", null);
        }

        //verifying Different Named Registrations resolve to different types. 
        [TestMethod]
        public void DifferentNamedRegistrationsResolveToDifferentTypes()
        {
            var container = new Container();
            container.Register<IFoo>("Foo1", c => new Foo1());
            container.Register<IFoo>("Foo2", c => new Foo2());

            var result1 = container.Resolve<IFoo>("Foo1");
            var result2 = container.Resolve<IFoo>("Foo2");

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);

            Assert.IsInstanceOfType(result1, typeof(IFoo));
            Assert.IsInstanceOfType(result1, typeof(Foo1));

            Assert.IsInstanceOfType(result2, typeof(IFoo));
            Assert.IsInstanceOfType(result2, typeof(Foo2));
        }

        //verifying Named and Unnamed Registration resolve to different types.
        [TestMethod]
        public void NamedAndUnnamedRegistrationsResolveToDifferentTypes()
        {
            var container = new Container();
            container.Register<IFoo>(c => new Foo1());
            container.Register<IFoo>("Foo2", c => new Foo2());

            var result1 = container.Resolve<IFoo>();
            var result2 = container.Resolve<IFoo>("Foo2");

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);

            Assert.IsInstanceOfType(result1, typeof(IFoo));
            Assert.IsInstanceOfType(result1, typeof(Foo1));

            Assert.IsInstanceOfType(result2, typeof(IFoo));
            Assert.IsInstanceOfType(result2, typeof(Foo2));

        }

        // Verify passing a null LifetimeManager equivalent to AlwaysNew
        [TestMethod]
        
        public void NullLifetimeManagerSameAsAlwaysNew()
        {
            var container = new Container();

            container.Register<IFoo>(c => new Foo1())
                .WithLifetimeManager(null);

            var result1 = container.Resolve<IFoo>();
            var result2 = container.Resolve<IFoo>();

            Assert.AreNotSame(result1, result2);

        }

        // verify Container Lifetime always returns same instance 
        [TestMethod]
        public void ContainerLifetimeAlwaysReturnsSameInstance()
        {
            var container = new Container();

            container.Register<IFoo>(c => new Foo1())
                .WithLifetimeManager(Registration.ContainerLifetimeManager);

            var result1 = container.Resolve<IFoo>();
            var result2 = container.Resolve<IFoo>();

            Assert.IsNotNull(result1);
            Assert.AreSame(result1, result2);
        }

        // verify Session Lifetime returns same instance for session
        [TestMethod]
        public void SessionLifetimeManagerReturnsSameObjectForSameSession()
        {
            var sessionItems = new SessionStateItemCollection();
            var context1 = new FakeHttpContext("Http://fakeUrl1.com",null,null,null,null,sessionItems);
            var context2 = new FakeHttpContext("Http://fakeUrl2.com",null,null,null,null,sessionItems);

            var sessionltm = new SessionLifetime();

            var container = new Container();
            container.Register<IFoo>(c => new Foo1())
                .WithLifetimeManager(sessionltm);

            sessionltm.SetContext(context1);

            var result1 = container.Resolve<IFoo>();
            var result2 = container.Resolve<IFoo>();

            sessionltm.SetContext(context2);

            var result3 = container.Resolve<IFoo>();

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.IsNotNull(result3);

            Assert.AreSame(result1, result2);   // same request and session
            Assert.AreSame(result2, result3);   // different requests, same session
        }

        // verify Session Lifetime returns different instance for different session
        [TestMethod]
        public void SessionLifetimeManagerReturnsDifferentObjectForDifferentSession()
        {
            var sessionItems1 = new SessionStateItemCollection();
            var sessionItems2 = new SessionStateItemCollection();
            var context1 = new FakeHttpContext("Http://fakeUrl1.com", null, null, null, null, sessionItems1);
            var context2 = new FakeHttpContext("Http://fakeUrl2.com", null, null, null, null, sessionItems2);

            var sessionltm = new SessionLifetime();

            var container = new Container();
            container.Register<IFoo>(c => new Foo1())
                .WithLifetimeManager(sessionltm);

            sessionltm.SetContext(context1);

            var result1 = container.Resolve<IFoo>();
            var result2 = container.Resolve<IFoo>();

            sessionltm.SetContext(context2);

            var result3 = container.Resolve<IFoo>();

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.IsNotNull(result3);

            Assert.AreSame(result1, result2);       // same session
            Assert.AreNotSame(result2, result3);    // different sessions
        }

        // verify Request Lifetime returns same instance for same request, different for different request
        [TestMethod]
        public void RequestLifetimeManagerReturnsSameObjectForSameRequest()
        {
            var context1 = new FakeHttpContext("Http://fakeUrl1.com");
            var context2 = new FakeHttpContext("Http://fakeUrl2.com");

            var requestltm = new RequestLifetime();

            var container = new Container();
            container.Register<IFoo>(c => new Foo1())
                .WithLifetimeManager(requestltm);

            requestltm.SetContext(context1);

            var result1 = container.Resolve<IFoo>();
            var result2 = container.Resolve<IFoo>();

            requestltm.SetContext(context2);

            var result3 = container.Resolve<IFoo>();

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.IsNotNull(result3);

            Assert.AreSame(result1, result2);       // same request
            Assert.AreNotSame(result2, result3);    // different request
        }

        // verify Cached Lifetime returns same instance for same if cache not expired
        [TestMethod]
        public void CachedLifetimeManagerReturnsSameObjectIfCacheNotExpired()
        {
            var cachedltm = new CachedLifetime();

            var container = new Container();
            container.Register<IFoo>(c => new Foo1())
                .WithLifetimeManager(cachedltm);

            var result1 = container.Resolve<IFoo>();
            var result2 = container.Resolve<IFoo>();

            var result3 = container.Resolve<IFoo>();

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.IsNotNull(result3);

            Assert.AreSame(result1, result2);       // same request
            Assert.AreSame(result2, result3);    // different request
        }

        // verify Cached Lifetime returns differnt instance  if cache expired
        [TestMethod]
        public void CachedLifetimeManagerReturnsDifferentObjectIfCacheExpired()
        {
            var cachedltm = new CachedLifetime();

            var container = new Container();
            var ireg = container.Register<IFoo>(c => new Foo1())
                .WithLifetimeManager(cachedltm);

            var result1 = container.Resolve<IFoo>();
            var result2 = container.Resolve<IFoo>();

            // simulate expiry
            HttpRuntime.Cache.Remove(ireg.Id);

            var result3 = container.Resolve<IFoo>();

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.IsNotNull(result3);

            Assert.AreSame(result1, result2);       // cache not expired
            Assert.AreNotSame(result2, result3);    // cache expired
        }

        // verify Cached Lifetime returns differnt instance  if absoluteTime expires
        [TestMethod]
        public void CachedLifetimeManagerReturnsDifferentObjectIfAbsoluteTimeExpired()
        {
            var cachedltm = new CachedLifetime()
                .ExpiresOn(DateTime.UtcNow.AddSeconds(2));

            var container = new Container();

            var ireg = container.Register<IFoo>(c => new Foo1())
                .WithLifetimeManager(cachedltm);

            var result1 = container.Resolve<IFoo>();
            var result2 = container.Resolve<IFoo>();

            // simulate expiry
            Thread.Sleep(3000);

            var result3 = container.Resolve<IFoo>();

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.IsNotNull(result3);

            Assert.AreSame(result1, result2);       // cache not expired
            Assert.AreNotSame(result2, result3);    // cache expired
        }

        // verify Cached Lifetime returns differnt instance  if absoluteTime expires
        [TestMethod]
        public void CachedLifetimeManagerReturnsDifferentObjectIfSlidingTimeExpired()
        {
            var cachedltm = new CachedLifetime()
                .ExpiresAfterNotAccessedFor(new TimeSpan(0,0,2));

            var container = new Container();

            var ireg = container.Register<IFoo>(c => new Foo1())
                .WithLifetimeManager(cachedltm);

            var result1 = container.Resolve<IFoo>();
            var result2 = container.Resolve<IFoo>();

            // simulate expiry
            Thread.Sleep(3000);

            var result3 = container.Resolve<IFoo>();

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.IsNotNull(result3);

            Assert.AreSame(result1, result2);       // cache not expired
            Assert.AreNotSame(result2, result3);    // cache expired
        }

        bool itemRemoved;
        CacheItemRemovedReason reason;

        public void RemovedCallback(String k, Object v, CacheItemRemovedReason r)
        {
            itemRemoved = true;
            reason = r;
        }

        // verify Callback called when item is removed
        [TestMethod]
        public void CallbackIsCalledWhenItemRemovedFromCache()
        {
            var cachedltm = new CachedLifetime()
                .ExpiresAfterNotAccessedFor(new TimeSpan(0, 0, 2))
                .CallbackOnRemoval(RemovedCallback);

            var container = new Container();

            var ireg = container.Register<IFoo>(c => new Foo1())
                .WithLifetimeManager(cachedltm);
            
            itemRemoved = false;
            var result1 = container.Resolve<IFoo>();

            // simulate expiry
            Thread.Sleep(3000);
            var result2 = container.Resolve<IFoo>();

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.AreNotSame(result1, result2);
            Assert.IsTrue(itemRemoved);

        }

        // verify Dispose is called on Disposable instance for ContainerLifetime objects
        static bool disposeWasCalled = false;

        public class DisposableBar : IBar, IDisposable
        {
            #region IDisposable Members

            public void Dispose()
            {
                disposeWasCalled = true;
            }
            #endregion
        }

        [TestMethod]
        public void DisposableWithContainerLifetimeAreDisposed()
        {
            var c = new Container();
            var clt = new ContainerLifetime();

            c.Register<IBar>(x => new DisposableBar()).WithLifetimeManager(clt);
            var df = c.Resolve<IBar>();
            disposeWasCalled = false;

            c.Dispose();

            Assert.IsTrue(disposeWasCalled);
        }
    }
}
