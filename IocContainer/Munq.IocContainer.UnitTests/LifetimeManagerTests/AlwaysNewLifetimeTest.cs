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
        IocContainer iocContainer;
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            iocContainer = new Munq.IocContainer();
        }

        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            iocContainer.Dispose();
        }
        #endregion

        /// <summary>
        /// Verify that Can Set the DefaultLifetimeManager To AlwaysNewLifetime
        ///</summary>
        [TestMethod()]
        public void CanSetDefaultLifetimeManagerToAlwaysNew()
        {
            var lifetime = new AlwaysNewLifetime();
            iocContainer.UsesDefaultLifetimeManagerOf(lifetime);

            Verify.That(iocContainer.DefaultLifetimeManager).IsTheSameObjectAs(lifetime);
        }

        /// <summary>
        /// Verifies that the AlwaysNew LifetimeManager Always Returns a New Instance
        ///</summary>
        [TestMethod()]
        public void AlwayNewLifetimeManagerAlwaysReturnsNewInstance()
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
}
