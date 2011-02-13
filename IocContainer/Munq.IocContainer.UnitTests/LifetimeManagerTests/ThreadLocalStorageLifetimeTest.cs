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

        Container iocContainer;
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
            iocContainer.Dispose();
        }
        #endregion

        /// <summary>
        /// Verify that Can Set the DefaultLifetimeManager To ThreadLocalStorageLifetime
        ///</summary>
        [TestMethod()]
        public void CanSetDefaultLifetimeManagerToThreadLocalStorageLifetime()
        {
            var lifetime = new ThreadLocalStorageLifetime();
            iocContainer.UsesDefaultLifetimeManagerOf(lifetime);

            Verify.That(iocContainer.DefaultLifetimeManager).IsTheSameObjectAs(lifetime);
        }

        /// <summary>
        /// verify Request Lifetime returns same instance for same request, 
        /// different for different request
        /// </summary>
        [TestMethod]
        public void ThreadLocalStorageLifetimeManagerReturnsSameObjectForSameRequest()
        {

            var requestltm = new ThreadLocalStorageLifetime();

            var container = new Container();
            container.Register<IFoo>(c => new Foo1())
                .WithLifetimeManager(requestltm);

            IFoo result1 = container.Resolve<IFoo>();
            IFoo result2 = container.Resolve<IFoo>();

            IFoo result3=null;
            IFoo result4=null;

            // get values on a different thread
            var t = Task.Factory.StartNew(() =>
            {
                result3 = container.Resolve<IFoo>();
                result4 = container.Resolve<IFoo>();
            });

            t.Wait();

            // check the results
            Verify.That(result3).IsNotNull(); 
            Verify.That(result4).IsNotNull()
                        .IsTheSameObjectAs(result3);

            Verify.That(result2).IsNotNull();
            Verify.That(result1).IsNotNull()
                        .IsTheSameObjectAs(result2)
                        .IsNotTheSameObjectAs(result3);
        }
    }
}
