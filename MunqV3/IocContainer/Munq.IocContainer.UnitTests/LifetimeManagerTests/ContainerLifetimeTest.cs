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
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

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
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
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
        /// Verify that Can Set the DefaultLifetimeManager To ContainerLifetime
        ///</summary>
        [TestMethod()]
        public void CanSetDefaultLifetimeManagerToContainerLifetime()
        {
            var lifetime = new ContainerLifetime();
            iocContainer.UsesDefaultLifetimeManagerOf(lifetime);

            Verify.That(iocContainer.DefaultLifetimeManager).IsTheSameObjectAs(lifetime);
        }

        /// <summary>
        /// Verifies that the Container LifetimeManager Always Returns a Same Instance
        ///</summary>
        [TestMethod()]
        public void ContainerLifetimeManagerAlwaysReturnsSameInstance()
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
}
