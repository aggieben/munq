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
        /// <summary>
        /// Verify that Can Set the DefaultLifetimeManager To CahcedLifetime
        ///</summary>
		[TestMethod()]
		public void CanSetDefaultLifetimeManagerToCachedLifetime()
		{
			using (var iocContainer = new IocContainer())
			{
				var lifetime = new CachedLifetime();
				iocContainer.UsesDefaultLifetimeManagerOf(lifetime);

				Verify.That(iocContainer.DefaultLifetimeManager).IsTheSameObjectAs(lifetime);
			}
		}

        /// <summary>
        /// verify Cached Lifetime returns same instance for same if cache not expired
        /// </summary>
		[TestMethod]
		public void CachedLifetimeManagerReturnsSameObjectIfCacheNotExpired()
		{
			using (var iocContainer = new IocContainer())
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
		}

        /// <summary>
        /// verify Cached Lifetime returns differnt instance  if cache expired
        /// </summary>
		[TestMethod]
		public void CachedLifetimeManagerReturnsDifferentObjectIfCacheExpired()
		{
			using (var iocContainer = new IocContainer())
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
		}

        /// <summary>
        /// verify Cached Lifetime returns differnt instance  if absoluteTime expires
        /// </summary>
		[TestMethod]
		public void CachedLifetimeManagerReturnsDifferentObjectIfAbsoluteTimeExpired()
		{
			using (var iocContainer = new IocContainer())
			{
				var cachedltm = new CachedLifetime()
					.ExpiresOn(DateTime.UtcNow.AddSeconds(2));

				iocContainer.Register<IFoo>(c => new Foo1())
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
		}
 
        /// <summary>
        /// verify Cached Lifetime returns differnt instance  if sliding Time expires
        /// </summary>
		[TestMethod]
		public void CachedLifetimeManagerReturnsDifferentObjectIfSlidingTimeExpired()
		{
			using (var iocContainer = new IocContainer())
			{
				var cachedltm = new CachedLifetime()
					.ExpiresAfterNotAccessedFor(new TimeSpan(0, 0, 1));

				iocContainer.Register<IFoo>(c => new Foo1())
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
			using (var iocContainer = new IocContainer())
			{
				var cachedltm = new CachedLifetime()
					.ExpiresAfterNotAccessedFor(new TimeSpan(0, 0, 1))
					.CallbackOnRemoval(RemovedCallback);

				iocContainer.Register<IFoo>(c => new Foo1())
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
		}
 
        /// <summary>
        ///A test for IsDependentOn
        ///</summary>
		[TestMethod()]
		public void IsDependentOnTest()
		{
			using (var iocContainer = new IocContainer())
			{
				var executionDirectory = Environment.CurrentDirectory;
				// create a file for the cached item to be dependent on
				var filePath = executionDirectory + "\\DependencyFile.txt";

				if (File.Exists(filePath))
					File.Delete(filePath);

				var dependencyFile = File.CreateText(filePath);

				dependencyFile.WriteLine("This is a file that the cache item is dependent on.");
				dependencyFile.Close();

				var cachedltm = new CachedLifetime()
									.IsDependentOn(new CacheDependency(filePath));

				iocContainer.Register<IFoo>(c => new Foo1())
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
		}

		/// <summary>
		/// verify Cached Lifetime returns same instance for same if cache not expired
		/// </summary>
		[TestMethod]
		public void CachedLifetimeManagerExtensionReturnsSameObjectIfCacheNotExpired()
		{
			using (var iocContainer = new IocContainer())
			{
				iocContainer.Register<IFoo>(c => new Foo1()).AsCached();

				var result1 = iocContainer.Resolve<IFoo>();
				var result2 = iocContainer.Resolve<IFoo>();

				var result3 = iocContainer.Resolve<IFoo>();

				Verify.That(result1).IsNotNull()
							.IsTheSameObjectAs(result2)
							.IsTheSameObjectAs(result3);
			}
		}

		/// <summary>
		/// verify Cached Lifetime returns differnt instance  if cache expired
		/// </summary>
		[TestMethod]
		public void CachedLifetimeManageExtensionrReturnsDifferentObjectIfCacheExpired()
		{
			using (var iocContainer = new IocContainer())
			{
				var ireg = iocContainer.Register<IFoo>(c => new Foo1()).AsCached();

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
