using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq.DI;

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
    }
}
