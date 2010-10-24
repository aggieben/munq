using System;
using System.IO;
using System.Threading;
using System.Web.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq.FluentTest;
using Munq.LifetimeManagers;

namespace Munq.Test
{ 
    /// <summary>
    ///This is a test class for CachedLifetimeTest and is intended
    ///to contain all CachedLifetimeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CachedLifetimeTest
    {
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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}

        IIocContainer iocContainer;
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            iocContainer = new Munq.Container();
        }

        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            // remove the registrations, and cache values

            var regs = iocContainer.GetRegistrations<IFoo>();
            regs.ForEach(reg => iocContainer.Remove(reg));

            iocContainer.Dispose();
        }
        #endregion


        /// <summary>
        /// Verify that Can Set the DefaultLifetimeManager To CahcedLifetime
        ///</summary>
        [TestMethod()]
        public void CanSetDefaultLifetimeManagerToCachedLifetime()
        {
            var lifetime = new CachedLifetime();
            iocContainer.UsesDefaultLifetimeManagerOf(lifetime);

            Verify.That(iocContainer.LifeTimeManager).IsTheSameObjectAs(lifetime);
        }

        /// <summary>
        /// verify Cached Lifetime returns same instance for same if cache not expired
        /// </summary>
        [TestMethod]
        public void CachedLifetimeManagerReturnsSameObjectIfCacheNotExpired()
        {
            var cachedltm = new CachedLifetime();

            iocContainer.Register<IFoo>(c => new Foo1())
                .WithLifetimeManager(cachedltm);

            var result1 = iocContainer.Resolve<IFoo>();
            var result2 = iocContainer.Resolve<IFoo>();

            var result3 = iocContainer.Resolve<IFoo>();

            Verify.That(result1).IsNotNull()
                        .IsTheSameObjectAs(result2)
                        .IsTheSameObjectAs(result3);
        }

        /// <summary>
        /// verify Cached Lifetime returns differnt instance  if cache expired
        /// </summary>
        [TestMethod]
        public void CachedLifetimeManagerReturnsDifferentObjectIfCacheExpired()
        {
            var cachedltm = new CachedLifetime();

            var ireg = iocContainer.Register<IFoo>(c => new Foo1())
                .WithLifetimeManager(cachedltm);

            var result1 = iocContainer.Resolve<IFoo>();
            var result2 = iocContainer.Resolve<IFoo>();

            // simulate expiry
            ireg.InvalidateInstanceCache();

            var result3 = iocContainer.Resolve<IFoo>();

            Verify.That(result1).IsNotNull()
                        .IsTheSameObjectAs(result2)
                        .IsNotTheSameObjectAs(result3);
            Verify.That(result3).IsNotNull();
        }

        /// <summary>
        /// verify Cached Lifetime returns differnt instance  if absoluteTime expires
        /// </summary>
        [TestMethod]
        public void CachedLifetimeManagerReturnsDifferentObjectIfAbsoluteTimeExpired()
        {
            var cachedltm = new CachedLifetime()
                .ExpiresOn(DateTime.UtcNow.AddSeconds(2));

            var ireg = iocContainer.Register<IFoo>(c => new Foo1())
                .WithLifetimeManager(cachedltm);

            var result1 = iocContainer.Resolve<IFoo>();
            var result2 = iocContainer.Resolve<IFoo>();

            // simulate expiry
            Thread.Sleep(3000);

            var result3 = iocContainer.Resolve<IFoo>();

            Verify.That(result1).IsNotNull()
                        .IsTheSameObjectAs(result2)
                        .IsNotTheSameObjectAs(result3);
            Verify.That(result3).IsNotNull();
        }
 
        /// <summary>
        /// verify Cached Lifetime returns differnt instance  if sliding Time expires
        /// </summary>
        [TestMethod]
        public void CachedLifetimeManagerReturnsDifferentObjectIfSlidingTimeExpired()
        {
            var cachedltm = new CachedLifetime()
                .ExpiresAfterNotAccessedFor(new TimeSpan(0,0,1));

            var ireg = iocContainer.Register<IFoo>(c => new Foo1())
                .WithLifetimeManager(cachedltm);

            var result1 = iocContainer.Resolve<IFoo>();
            var result2 = iocContainer.Resolve<IFoo>();

            // simulate expiry
            Thread.Sleep(2000);

            var result3 = iocContainer.Resolve<IFoo>();

            Verify.That(result1).IsNotNull()
                        .IsTheSameObjectAs(result2)
                        .IsNotTheSameObjectAs(result3);
            Verify.That(result3).IsNotNull();
        }

        bool itemRemoved;
        CacheItemRemovedReason reason;

        public void RemovedCallback(String k, Object v, CacheItemRemovedReason r)
        {
            itemRemoved = true;
            reason = r;
        }

        /// <summary>
        /// verify Callback called when item is removed
        /// </summary>
        [TestMethod]
        public void CallbackIsCalledWhenItemRemovedFromCache()
        {
            var cachedltm = new CachedLifetime()
                .ExpiresAfterNotAccessedFor(new TimeSpan(0, 0, 1))
                .CallbackOnRemoval(RemovedCallback);

            var ireg = iocContainer.Register<IFoo>(c => new Foo1())
                .WithLifetimeManager(cachedltm);
            
            itemRemoved = false;
            var result1 = iocContainer.Resolve<IFoo>();

            // simulate expiry
            Thread.Sleep(2000);
            var result2 = iocContainer.Resolve<IFoo>();

            Verify.That(result1).IsNotNull();
            Verify.That(result2).IsNotNull()
                     .IsNotTheSameObjectAs(result1);

            Verify.That(itemRemoved).IsTrue();
        }
 
        /// <summary>
        ///A test for IsDependentOn
        ///</summary>
        [TestMethod()]
        public void IsDependentOnTest()
        {
            var executionDirectory = Environment.CurrentDirectory;
           // create a file for the cached item to be dependent on
            var filePath = executionDirectory+"\\DependencyFile.txt";

            if (File.Exists(filePath))
                File.Delete(filePath);

            var dependencyFile = File.CreateText(filePath);

            dependencyFile.WriteLine("This is a file that the cache item is dependent on.");
            dependencyFile.Close();

            var cacheDependency = new CacheDependency(filePath);

            var cachedltm = new CachedLifetime()
                                .IsDependentOn(cacheDependency);

            var ireg = iocContainer.Register<IFoo>(c => new Foo1())
                .WithLifetimeManager(cachedltm);

            var result1 = iocContainer.Resolve<IFoo>();
            var result2 = iocContainer.Resolve<IFoo>();

            // change the dependency file
            dependencyFile = File.AppendText(filePath);
            dependencyFile.WriteLine("Modified dependecy file.");
            dependencyFile.Close();

            // need to give the system time to detect the change.
            Thread.Sleep(500);

            var result3 = iocContainer.Resolve<IFoo>();

            // cleanup
            if (File.Exists(filePath))
                File.Delete(filePath);

            Verify.That(result3).IsNotNull();
            Verify.That(result1).IsNotNull()
                        .IsTheSameObjectAs(result2)
                        .IsNotTheSameObjectAs(result3);
        }

        /// <summary>
        ///A test for WithPriority
        ///</summary>
        [TestMethod()]
        public void WithPriorityTest()
        {
            // TODO:  Not sure how to test this ...
        }
    }
}
