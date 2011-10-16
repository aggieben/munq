using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq.FluentTest;

namespace Munq.Test
{
    /// <summary>
    /// Tests the IOC Container Interface
    /// </summary>
    [TestClass]
    public class ContainerInterfaceTests
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

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
        /// verify that we can create a container object
        /// </summary>
        [TestMethod]
        public void CanCreateContainer()
        {
            using (var iocContainer = new IocContainer())
            {
                Verify.That(iocContainer).IsNotNull();
            }
        }


        #region Register Tests
        /// <summary>
        /// Verifies that the GenericNamelessRegister method returns an IRegistration
        /// </summary>
        [TestMethod]
        public void GenericNamelessRegistrationReturnsRegistrationObject()
        {
            var result = iocContainer.Register<IFoo>(c => new Foo1());

            Verify.That(result).IsAnInstanceOfType(typeof(IRegistration));
            Verify.That(result.Name).IsNull();
        }

        /// <summary>
        /// Verifies that the GenericNamedRegister method returns an IRegistration
        /// </summary>
        [TestMethod]
        public void GenericNamedRegistrationReturnsRegistrationObject()
        {
            var result = iocContainer.Register<IFoo>("Bob", c => new Foo1());

            Verify.That(result).IsAnInstanceOfType(typeof(IRegistration));
            Verify.That(result.Name).IsAStringThat().IsEqualTo("Bob");
        }

        /// <summary>
        /// Verifies that the NonGenericNamelessRegister method returns an IRegistration
        /// </summary>
        [TestMethod]
        public void NonGenericNamelessRegistrationReturnsRegistrationObject()
        {
            var result = iocContainer.Register(typeof(IFoo), c => new Foo1());

            Verify.That(result).IsAnInstanceOfType(typeof(IRegistration));
            Verify.That(result.Name).IsNull();
        }

        /// <summary>
        /// Verifies that the NonGenericNamedRegister method returns an IRegistration
        /// </summary>
        [TestMethod]
        public void NonGenericNamedRegistrationReturnsRegistrationObject()
        {
            var result = iocContainer.Register("Bob", typeof(IFoo), c => new Foo1());

            Verify.That(result).IsAnInstanceOfType(typeof(IRegistration));
            Verify.That(result.Name).IsAStringThat().IsEqualTo("Bob");
        }

        /// <summary>
        /// Verifies that registering a second factory method resolving to the same type
        /// with the same name overwrites the existing factory method.
        /// </summary>
        [TestMethod]
        public void RegisteringASecondFactoryForSameTypeAndNameOverwitesFirst()
        {
            iocContainer.Register("Bob", typeof(IFoo), c => new Foo1());
            iocContainer.Register("Bob", typeof(IFoo), c => new Foo2());

            var result = iocContainer.Resolve<IFoo>("Bob");

            Verify.That(result).IsAnInstanceOfType(typeof(Foo2));
        }

        #endregion

        #region Register Instance Tests
        /// <summary>
        /// Verifies that the GenericNamelessRegisterInstance method returns an IRegistration
        /// </summary>
        [TestMethod]
        public void GenericNamelessInstanceRegistrationReturnsRegistrationObject()
        {
            var fooInstance = new Foo1();

            var result = iocContainer.RegisterInstance<IFoo>(fooInstance);

            Verify.That(result).IsAnInstanceOfType(typeof(IRegistration));
            Verify.That(result.Name).IsNull();
        }

        /// <summary>
        /// Verifies that the GenericNamedRegisterInstance method returns an IRegistration
        /// </summary>
        [TestMethod]
        public void GenericNamedInstanceRegistrationReturnsRegistrationObject()
        {
            var fooInstance = new Foo1();

            var result = iocContainer.RegisterInstance("Bob", fooInstance);

            Verify.That(result).IsAnInstanceOfType(typeof(IRegistration));
            Verify.That(result.Name).IsAStringThat().IsEqualTo("Bob");
        }

        /// <summary>
        /// Verifies that the NonGenericNamelessRegisterInstance method returns an IRegistration
        /// </summary>
        [TestMethod]
        public void NonGenericNamelessInstanceRegistrationReturnsRegistrationObject()
        {
            var fooInstance = new Foo1();

            var result = iocContainer.RegisterInstance(typeof(IFoo), fooInstance);
            Verify.That(result).IsAnInstanceOfType(typeof(IRegistration));
            Verify.That(result.Name).IsNull();
        }

        /// <summary>
        /// Verifies that the NonGenericNamedRegisterInstance method returns an IRegistration
        /// </summary>
        [TestMethod]
        public void NonGenericNamedInstanceRegistrationReturnsRegistrationObject()
        {
            var fooInstance = new Foo1();

            var result = iocContainer.RegisterInstance("Bob", typeof(IFoo), fooInstance);

            Verify.That(result).IsAnInstanceOfType(typeof(IRegistration));
            Verify.That(result.Name).IsAStringThat().IsEqualTo("Bob");
        }
        #endregion

        /// <summary>
        /// Verifies that Registrations can be removed.
        /// </summary>
        [TestMethod]
        public void CanRemoveRegistration()
        {
            var reg = iocContainer.Register<IFoo>(c => new Foo1());

            var result = iocContainer.GetRegistration<IFoo>();

            Verify.That(reg).IsTheSameObjectAs(result);

            iocContainer.Remove(reg);

            Verify.TheExpectedException(typeof(KeyNotFoundException)).IsThrownWhen(
               () => result = iocContainer.GetRegistration<IFoo>()
            );

        }

        #region Resolve Tests
        /// <summary>
        /// Verifies that the GenericNamelessResolve method returns the expected type.
        /// </summary>
        [TestMethod]
        public void GenericNamelessResolveReturnsObjectOfExpectedType()
        {
            iocContainer.Register<IFoo>(c => new Foo1());
            var result = iocContainer.Resolve<IFoo>();

            Verify.That(result)
                       .IsNotNull()
                       .IsAnInstanceOfType(typeof(IFoo))
                       .IsAnInstanceOfType(typeof(Foo1));
        }

        /// <summary>
        /// Verifies that the GenericNamedResolve method returns the expected type.
        /// </summary>
        [TestMethod]
        public void GenericNamedResolveReturnsObjectOfExpectedType()
        {
            iocContainer.Register<IFoo>("Bob", c => new Foo1());
            var result = iocContainer.Resolve<IFoo>("Bob");

            Verify.That(result)
                       .IsNotNull()
                       .IsAnInstanceOfType(typeof(IFoo))
                       .IsAnInstanceOfType(typeof(Foo1));
        }

        /// <summary>
        /// Verifies that the NonGenericNamelessResolve method returns the expected type.
        /// </summary>
        [TestMethod]
        public void NonGenericNamelessResolveReturnsObjectOfExpectedType()
        {
            iocContainer.Register(typeof(IFoo), c => new Foo1());
            var result = iocContainer.Resolve<IFoo>();

            Verify.That(result)
                       .IsNotNull()
                       .IsAnInstanceOfType(typeof(IFoo))
                       .IsAnInstanceOfType(typeof(Foo1));
        }

        /// <summary>
        /// Verifies that the NonGenericNamedResolve method returns the expected type.
        /// </summary>
        [TestMethod]
        public void NonGenericNamedResolveReturnsObjectOfExpectedType()
        {
            iocContainer.Register("Bob", typeof(IFoo), c => new Foo1());
            var result = iocContainer.Resolve<IFoo>("Bob");

            Verify.That(result)
                       .IsNotNull()
                       .IsAnInstanceOfType(typeof(IFoo))
                       .IsAnInstanceOfType(typeof(Foo1));
        }

        /// <summary>
        /// Verifies that the Attempting To Resolve A Nonexisting Entry Throws.
        /// </summary>
        [TestMethod]
        public void AttemptingToResolveANonexistingEntryThrows()
        {
            
                Verify.TheExpectedException(typeof(KeyNotFoundException))
                    .IsThrownWhen(() => iocContainer.Resolve<IFoo>("Bob"))
                    .AndHasAMessageThat().IsEqualTo("Munq IocContainer failed to resolve Munq.Test.IFoo");            
         }

        [TestMethod]
        public void RegisteredClassCanBeResolvedByInterface()
        {
            iocContainer.Register<Foo1, Foo1>();
            var result = iocContainer.Resolve<IFoo>();
            Verify.That(result).IsNotNull().IsAnInstanceOfType(typeof(Foo1));
        }

        [TestMethod]
        public void RegisteredClassesCanBeResolvedAllByInterface()
        {
            iocContainer.Register<Foo1, Foo1>();
            iocContainer.Register<Foo2, Foo2>();
            iocContainer.Register<Bar1, Bar1>();

            var results = iocContainer.ResolveAll<IFoo>();
            Verify.That(results).IsNotNull()
                                .IsAnInstanceOfType(typeof(IEnumerable<IFoo>));
            Verify.That(results.Count()).IsEqualTo(2);
        }
        #endregion

        #region Lazy Resolve Tests
        /// <summary>
        /// Verifies that the GenericNamelessLazyResolve method returns 
        /// a delegate that returns the expected type.
        /// </summary>
        [TestMethod]
        public void GenericNamelessLazyResolveReturnsDelegateReturningObjectOfExpectedType()
        {
            iocContainer.Register<IFoo>(c => new Foo1());
            var result = iocContainer.LazyResolve<IFoo>();

            Verify.That(result)
                       .IsNotNull()
                       .IsAnInstanceOfType(typeof(Func<IFoo>));

            Verify.That(result())
                       .IsAnInstanceOfType(typeof(Foo1));
        }

        /// <summary>
        /// Verifies that the GenericNamedLazyResolve method returns 
        /// a delegate that returns the expected type.
        /// </summary>
        [TestMethod]
        public void GenericNamedLazyResolveReturnsDelegateReturningObjectOfExpectedType()
        {
            iocContainer.Register<IFoo>("Bob", c => new Foo1());
            var result = iocContainer.LazyResolve<IFoo>("Bob");

            Verify.That(result)
                       .IsNotNull()
                       .IsAnInstanceOfType(typeof(Func<IFoo>));

            Verify.That(result())
                       .IsAnInstanceOfType(typeof(Foo1));
        }

        /// <summary>
        /// Verifies that the NonGenericNamelessLazyResolve method returns 
        /// a delegate that returns the expected type.
        /// </summary>
        [TestMethod]
        public void NonGenericNamelessLazyResolveReturnsDelegateReturningObjectOfExpectedType()
        {
            iocContainer.Register(typeof(IFoo), c => new Foo1());
            var result = iocContainer.LazyResolve(typeof(IFoo));

            Verify.That(result)
                       .IsNotNull()
                       .IsAnInstanceOfType(typeof(Func<object>));

            Verify.That(result())
                       .IsAnInstanceOfType(typeof(Foo1));
        }

        /// <summary>
        /// Verifies that the NonGenericNamedLazyResolve method returns 
        /// a delegate that returns the expected type.
        /// </summary>
        [TestMethod]
        public void NonGenericNamedLazyResolveReturnsDelegateReturningObjectOfExpectedType()
        {
            iocContainer.Register("Bob", typeof(IFoo), c => new Foo1());

            var result = iocContainer.LazyResolve("Bob", typeof(IFoo));

            Verify.That(result)
                       .IsNotNull()
                       .IsAnInstanceOfType(typeof(Func<object>));

            Verify.That(result())
                       .IsAnInstanceOfType(typeof(Foo1));
        }
        #endregion

        #region GetRegistration(s) Tests
        /// <summary>
        /// Verifies that the Generic Nameless GetRegistration Return The Expected Registration.
        /// </summary>
        [TestMethod]
        public void GenericNamelessGetRegistrationReturnTheExpectedRegistration()
        {
            var expectedRegistration = iocContainer.Register<IFoo>(c => new Foo1());
            var result = iocContainer.GetRegistration<IFoo>();

            Verify.That(result).IsNotNull()
                       .IsTheSameObjectAs(expectedRegistration);
        }

        /// <summary>
        /// Verifies that the Generic Named GetRegistration Return The Expected Registration.
        /// </summary>
        [TestMethod]
        public void GenericNamedGetRegistrationReturnTheExpectedRegistration()
        {
            var expectedRegistration = iocContainer.Register<IFoo>("Bob", c => new Foo1());
            var result = iocContainer.GetRegistration<IFoo>("Bob");

            Verify.That(result).IsNotNull()
                       .IsTheSameObjectAs(expectedRegistration);
        }

        /// <summary>
        /// Verifies that the NonGeneric Nameless GetRegistration Return The Expected Registration.
        /// </summary>
        [TestMethod]
        public void NonGenericNamelessGetRegistrationReturnTheExpectedRegistration()
        {
            var expectedRegistration = iocContainer.Register(typeof(IFoo), c => new Foo1());
            var result = iocContainer.GetRegistration<IFoo>();

            Verify.That(result).IsNotNull()
                       .IsTheSameObjectAs(expectedRegistration);
        }

        /// <summary>
        /// Verifies that the NonGeneric Named GetRegistration Return The Expected Registration.
        /// </summary>
        [TestMethod]
        public void NonGenericNamedGetRegistrationReturnTheExpectedRegistration()
        {
            var expectedRegistration = iocContainer.Register("Bob", typeof(IFoo), c => new Foo1());
            var result = iocContainer.GetRegistration<IFoo>("Bob");

            Verify.That(result).IsNotNull()
                       .IsTheSameObjectAs(expectedRegistration);
        }

        /// <summary>
        /// Verifies that the Attempting Generic Get Registration For A Nonexisting Entry Throws.
        /// </summary>
        [TestMethod]
        public void AttemptingNamedGenericGetRegistrationForANonexistingEntryThrows()
        {
            Verify.TheExpectedException(typeof(KeyNotFoundException)).IsThrownWhen(
                () => iocContainer.GetRegistration<IFoo>("Bob")
             ).AndHasAMessageThat().IsEqualTo("Registration not found for Munq.Test.IFoo");
        }

        /// <summary>
        /// Verifies that the Attempting NonGeneric Get Registration For A Nonexisting Entry Throws.
        /// </summary>
        [TestMethod]
        public void AttemptingNonGenericNamedGetRegistrationForANonexistingEntryThrows()
        {
            Verify.TheExpectedException(typeof(KeyNotFoundException)).IsThrownWhen(
                () => iocContainer.GetRegistration("Bob", typeof(IFoo))
             ).AndHasAMessageThat().IsEqualTo("Registration not found for Munq.Test.IFoo");
        }

        /// <summary>
        /// Verifies that the Attempting Generic Get Registration For A Nonexisting Entry Throws.
        /// </summary>
        [TestMethod]
        public void AttemptingNamelessGenericGetRegistrationForANonexistingEntryThrows()
        {
            Verify.TheExpectedException(typeof(KeyNotFoundException)).IsThrownWhen(
                () => iocContainer.GetRegistration<IFoo>()
             ).AndHasAMessageThat().IsEqualTo("Registration not found for Munq.Test.IFoo");
        }

        /// <summary>
        /// Verifies that the Attempting NonGeneric Get Registration For A Nonexisting Entry Throws.
        /// </summary>
        [TestMethod]
        public void AttemptingNonGenericNamelessGetRegistrationForANonexistingEntryThrows()
        {
            Verify.TheExpectedException(typeof(KeyNotFoundException)).IsThrownWhen(
                () => iocContainer.GetRegistration(typeof(IFoo))
             ).AndHasAMessageThat().IsEqualTo("Registration not found for Munq.Test.IFoo");
        }

        /// <summary>
        /// Verifies that Generic GetRegistrations Includes All But Only Expected Registrations.
        /// </summary>
        [TestMethod]
        public void GenericGetRegistrationsIncludesAllButOnlyExpectedRegistrations()
        {
            var namelessFoo = iocContainer.Register<IFoo>(c => new Foo1());
            var bobFoo = iocContainer.Register<IFoo>("Bob", c => new Foo2());
            var billFoo = iocContainer.Register<IFoo>("Bill", c => new Foo2());
            var janeBar = iocContainer.Register<IBar>("Jane", c => new Bar1());

            var result = iocContainer.GetRegistrations<IFoo>();

            Verify.That(result.ToList()).IsACollectionThat()
                       .IsAnInstanceOfType(typeof(List<IRegistration>))
                       .Count().IsEqualTo(3)
                       .AllItemsAreInstancesOfType(typeof(IRegistration))
                       .Contains(namelessFoo)
                       .Contains(bobFoo)
                       .Contains(billFoo)
                       .DoesNotContain(janeBar);
        }

        /// <summary>
        /// Verifies that NonGeneric GetRegistrations Includes All But Only Expected Registrations.
        /// </summary>
        [TestMethod]
        public void NonGenericGetRegistrationsIncludesAllButOnlyExpectedRegistrations()
        {
            var namelessFoo = iocContainer.Register(typeof(IFoo), c => new Foo1());
            var bobFoo = iocContainer.Register("Bob", typeof(IFoo), c => new Foo2());
            var billFoo = iocContainer.Register("Bill", typeof(IFoo), c => new Foo2());
            var janeBar = iocContainer.Register("Jane", typeof(IBar), c => new Bar1());

            var result = iocContainer.GetRegistrations(typeof(IFoo));

            Verify.That(result.ToList()).IsACollectionThat()
                       .IsAnInstanceOfType(typeof(List<IRegistration>))
                       .Count().IsEqualTo(3)
                       .AllItemsAreInstancesOfType(typeof(IRegistration))
                       .Contains(namelessFoo)
                       .Contains(bobFoo)
                       .Contains(billFoo)
                       .DoesNotContain(janeBar);
        }

        /// <summary>
        /// Verifies that GetRegistrations Returns An Empty List If No Registrations Of
        /// The Requeseted Type.
        /// </summary>
        [TestMethod]
        public void GetRegistrationsReturnsAnEmptyListIfNoRegistrationsOfTheRequesetedType()
        {
            iocContainer.Register<IFoo>(c => new Foo1());
            iocContainer.Register<IFoo>("Bob", c => new Foo2());
            iocContainer.Register<IFoo>("Bill", c => new Foo2());

            var result = iocContainer.GetRegistrations<IBar>();

            Verify.That(result.ToList()).IsACollectionThat()
                       .IsAnInstanceOfType(typeof(List<IRegistration>))
                       .Count().IsEqualTo(0);
        }
        #endregion

        #region FluentInterface Tests
        /// <summary>
        /// Verify that UsesDefaultLifetimeManagerOf Changes The LifetimeManager
        /// That New Registrations Are Created With
        /// </summary>
        [TestMethod]
        public void UsesDefaultLifetimeManagerOfChangesTheDefaultLifetimeManager()
        {
            var aLifetimeManager = new LifetimeManagers.ContainerLifetime();
            iocContainer.UsesDefaultLifetimeManagerOf(aLifetimeManager);

            iocContainer.Register<IFoo>(c => new Foo1());
            var foo1 = iocContainer.Resolve<IFoo>();
            var foo2 = iocContainer.Resolve<IFoo>();

            Verify.That(iocContainer.DefaultLifetimeManager).IsTheSameObjectAs(aLifetimeManager);
            Verify.That(foo1).IsTheSameObjectAs(foo2);
        }
        #endregion

        #region Additional Tests
        /// <summary>
        ///  verify that default Container returns new instances for each Resolve
        ///  when using the default null LifetimeManager
        /// </summary>
        [TestMethod]
        public void MultipleResolvesReturnDifferentInstances()
        {
            using (var container = new IocContainer())
            {
                container.Register<IFoo>(c => new Foo1());
                var result1 = container.Resolve<IFoo>();
                var result2 = container.Resolve<IFoo>();

                Verify.That(result1).IsNotNull();
                Verify.That(result2).IsNotNull().IsNotTheSameObjectAs(result1);
            }
        }


        /// <summary>
        /// verify that can register and resolve multiple types
        /// </summary>
        [TestMethod]
        public void CanRegisterAndResolveMultipleTypes()
        {
            using (var container = new IocContainer())
            {
                container.Register<IFoo>(c => new Foo1());
                container.Register<IBar>(c => new Bar1());
                var foo = container.Resolve<IFoo>();
                var bar = container.Resolve<IBar>();

                Verify.That(foo).IsNotNull().IsAnInstanceOfType(typeof(IFoo));
                Verify.That(bar).IsNotNull().IsAnInstanceOfType(typeof(IBar));
            }
        }

        /// <summary>
        /// verifying Different Named Registrations resolve to different types. 
        /// </summary>
        [TestMethod]
        public void DifferentNamedRegistrationsResolveToDifferentTypes()
        {
            using (var container = new IocContainer())
            {
                container.Register<IFoo>("Foo1", c => new Foo1());
                container.Register<IFoo>("Foo2", c => new Foo2());
                var result1 = container.Resolve<IFoo>("Foo1");
                var result2 = container.Resolve<IFoo>("Foo2");

                Verify.That(result1)
                           .IsAnInstanceOfType(typeof(IFoo))
                           .IsAnInstanceOfType(typeof(Foo1));

                Verify.That(result2)
                           .IsAnInstanceOfType(typeof(IFoo))
                           .IsAnInstanceOfType(typeof(Foo2))
                           .IsNotTheSameObjectAs(result1);
            }
        }


        /// <summary>
        /// verifying Named and Unnamed Registration resolve to different types.
        /// </summary>
        [TestMethod]
        public void NamedAndUnnamedRegistrationsResolveToDifferentTypes()
        {
            using (var container = new IocContainer())
            {
                container.Register<IFoo>(c => new Foo1());
                container.Register<IFoo>("Foo2", c => new Foo2());
                var result1 = container.Resolve<IFoo>();
                var result2 = container.Resolve<IFoo>("Foo2");

                Verify.That(result1)
                           .IsAnInstanceOfType(typeof(IFoo))
                           .IsAnInstanceOfType(typeof(Foo1));

                Verify.That(result2)
                           .IsAnInstanceOfType(typeof(IFoo))
                           .IsAnInstanceOfType(typeof(Foo2))
                           .IsNotTheSameObjectAs(result1);
            }

        }

        [TestMethod]
        public void UnnamedTypeRegistrationReturnsRegistration()
        {
            using (var container = new IocContainer())
            {
                var result = container.Register<IFoo, Foo1>();
                Verify.That(result).IsNotNull().IsAnInstanceOfType(typeof(IRegistration));
            }
        }
        [TestMethod]
        public void UnnamedTypeRegistrationResolvesToCorrectType()
        {
            using (var container = new IocContainer())
            {
                container.Register<IFoo, Foo1>();
                var result = container.Resolve<IFoo>();

                Verify.That(result).IsNotNull().IsAnInstanceOfType(typeof(Foo1));
            }
        }

        [TestMethod]
        public void UnnamedTypeWithParametersRegistrationResolvesToCorrectType()
        {
            using (var container = new IocContainer())
            {
                container.Register<IFoo, Foo1>();
                container.Register<IBar, Bar1>();
                container.Register<IFooBar, FooBar>();

                IFooBar result = container.Resolve<IFooBar>();

                Verify.That(result).IsNotNull().IsAnInstanceOfType(typeof(FooBar));
                Verify.That(result.foo).IsNotNull().IsAnInstanceOfType(typeof(Foo1));
                Verify.That(result.bar).IsNotNull().IsAnInstanceOfType(typeof(Bar1));
            }
        }

        [TestMethod]
        public void NnamedTypeRegistrationReturnsRegistration()
        {
            using (var container = new IocContainer())
            {
                var result = container.Register<IFoo, Foo1>("bob");
                Verify.That(result).IsNotNull().IsAnInstanceOfType(typeof(IRegistration));
            }
        }
        [TestMethod]
        public void NamedTypeRegistrationResolvesToCorrectType()
        {
            using (var container = new IocContainer())
            {
                container.Register<IFoo, Foo1>("bob");
                var result = container.Resolve<IFoo>("bob");

                Verify.That(result).IsNotNull().IsAnInstanceOfType(typeof(Foo1));
            }
        }

        [TestMethod]
        public void NamedTypeWithParametersRegistrationResolvesToCorrectType()
        {
            using (var container = new IocContainer())
            {
                container.Register<IFoo, Foo1>();
                container.Register<IBar, Bar1>();
                container.Register<IFooBar, FooBar>("bob");

                IFooBar result = container.Resolve<IFooBar>("bob");

                Verify.That(result).IsNotNull().IsAnInstanceOfType(typeof(FooBar));
                Verify.That(result.foo).IsNotNull().IsAnInstanceOfType(typeof(Foo1));
                Verify.That(result.bar).IsNotNull().IsAnInstanceOfType(typeof(Bar1));
            }
        }

        [TestMethod]
        public void TypeRegisterWhenTImplHasNoPublicConstructorThrows()
         {
             using( var container = new IocContainer())
             {
                Verify.TheExpectedException(typeof(ArgumentException)).IsThrownWhen(
                    () =>container.Register<INoConstructor, NoConstructor>()
                ).AndHasAMessageThat().IsEqualTo("The requested class Munq.Test.NoConstructor does not have a public constructor.");

             }
         }

        [TestMethod]
        public void RegisteringOpenGenericTypesResolves()
        {
            using ( var container = new IocContainer())
            {
                container.Register(typeof(IFoo<>), typeof(Foo<>));

                var result = container.Resolve<IFoo<int>>();

                Verify.That(result).IsNotNull().IsAnInstanceOfType(typeof(Foo<int>));
            }
        }
         #endregion

    }
}
