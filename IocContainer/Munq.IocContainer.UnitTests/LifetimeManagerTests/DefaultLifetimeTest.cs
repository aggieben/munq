using Munq.LifetimeManagers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Munq;
using Munq.FluentTest;

namespace Munq.Test
{
    /// <summary>
    ///This is a test class for DefaultLifetimeTest and is intended
    ///to contain all Unit Tests for the default Lifetime
    ///</summary>
    [TestClass()]
    public class DefaultLifetimeTest
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
        //
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
            iocContainer.Dispose();
        }
        #endregion

        /// <summary>
        /// Verify that the default LifetimeManager is null
        ///</summary>
        [TestMethod()]
        public void DefaultLifetimeMangerIsNull()
        {
            Assert.IsNull(iocContainer.LifeTimeManager);
        }

        /// <summary>
        /// Verifies that the Default LifetimeManager Always Returns a New Instance
        ///</summary>
        [TestMethod()]
        public void DefaultLifetimeManagerAlwaysReturnsNewInstance()
        {
            iocContainer.Register<IFoo>(c => new Foo1());

            var result1 = iocContainer.Resolve<IFoo>();
            var result2 = iocContainer.Resolve<IFoo>();
            var result3 = iocContainer.Resolve<IFoo>();

            Verify.That(result3).IsNotNull();
            Verify.That(result2).IsNotNull()
                        .IsNotTheSameObjectAs(result3);
            Verify.That(result1).IsNotNull()
                        .IsNotTheSameObjectAs(result2)
                        .IsNotTheSameObjectAs(result3);
        }
    }
}
