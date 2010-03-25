using System;
using System.Collections.Generic;
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
        public ContainerInterfaceTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
         /// verify that we can create a container object
         /// </summary>
         [TestMethod]
         public void CanCreateContainer()
         {
             using (var iocContainer = new Container())
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

             Assert.IsInstanceOfType(result, typeof(IRegistration));
             Assert.IsNull(result.Name);
         }

         /// <summary>
         /// Verifies that the GenericNamedRegister method returns an IRegistration
         /// </summary>
         [TestMethod]
         public void GenericNamedRegistrationReturnsRegistrationObject()
         {
             var result = iocContainer.Register<IFoo>("Bob", c => new Foo1());

             Assert.IsInstanceOfType(result, typeof(IRegistration));
             Assert.IsNotNull(result.Name);
             Assert.AreEqual("Bob", result.Name);
         }

         /// <summary>
         /// Verifies that the NonGenericNamelessRegister method returns an IRegistration
         /// </summary>
         [TestMethod]
         public void NonGenericNamelessRegistrationReturnsRegistrationObject()
         {
             var result = iocContainer.Register(typeof(IFoo), c => new Foo1());

             Assert.IsInstanceOfType(result, typeof(IRegistration));
             Assert.IsNull(result.Name);
         }

         /// <summary>
         /// Verifies that the NonGenericNamedRegister method returns an IRegistration
         /// </summary>
         [TestMethod]
         public void NonGenericNamedRegistrationReturnsRegistrationObject()
         {
             var result = iocContainer.Register("Bob", typeof(IFoo), c => new Foo1());

             Assert.IsInstanceOfType(result, typeof(IRegistration));
             Assert.IsNotNull(result.Name);
             Assert.AreEqual("Bob", result.Name);
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

             Assert.IsInstanceOfType(result, typeof(Foo2));
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

             Assert.IsInstanceOfType(result, typeof(IRegistration));
             Assert.IsNull(result.Name);
         }

         /// <summary>
         /// Verifies that the GenericNamedRegisterInstance method returns an IRegistration
         /// </summary>
         [TestMethod]
         public void GenericNamedInstanceRegistrationReturnsRegistrationObject()
         {
             var fooInstance = new Foo1();

             var result = iocContainer.RegisterInstance("Bob", fooInstance);

             Assert.IsInstanceOfType(result, typeof(IRegistration));
             Assert.IsNotNull(result.Name);
             Assert.AreEqual("Bob", result.Name);
         }

         /// <summary>
         /// Verifies that the NonGenericNamelessRegisterInstance method returns an IRegistration
         /// </summary>
         [TestMethod]
         public void NonGenericNamelessInstanceRegistrationReturnsRegistrationObject()
         {
             var fooInstance = new Foo1();

             var result = iocContainer.RegisterInstance(typeof(IFoo), fooInstance);
             Assert.IsInstanceOfType(result, typeof(IRegistration));
             Assert.IsNull(result.Name);
         }

         /// <summary>
         /// Verifies that the NonGenericNamedRegisterInstance method returns an IRegistration
         /// </summary>
         [TestMethod]
         public void NonGenericNamedInstanceRegistrationReturnsRegistrationObject()
         {
             var fooInstance = new Foo1();

             var result = iocContainer.RegisterInstance("Bob", typeof(IFoo), fooInstance);
             Assert.IsInstanceOfType(result, typeof(IRegistration));
             Assert.IsNotNull(result.Name);
             Assert.AreEqual("Bob", result.Name);
         }
         #endregion

        /// <summary>
        /// Verifies that Registrations can be removed.
        /// </summary>
         [TestMethod]
         [ExpectedException(typeof(KeyNotFoundException))]
         public void CanRemoveRegistration()
         {
             var reg = iocContainer.Register<IFoo>(c => new Foo1());

             var result = iocContainer.GetRegistration<IFoo>();

             Verify.That(reg).IsTheSameObjectAs(result);

             iocContainer.Remove(reg);
             result = iocContainer.GetRegistration<IFoo>();
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
             iocContainer.Register(typeof(IFoo),c => new Foo1());
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
         [ExpectedException(typeof(KeyNotFoundException))]
         public void AttemptingToResolveANonexistingEntryThrows()
         {
             var result = iocContainer.Resolve<IFoo>("Bob");

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
             var result               = iocContainer.GetRegistration<IFoo>();

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
             var result               = iocContainer.GetRegistration<IFoo>("Bob");

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
             var result               = iocContainer.GetRegistration<IFoo>();

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
             var result               = iocContainer.GetRegistration<IFoo>("Bob");

             Verify.That(result).IsNotNull()
                        .IsTheSameObjectAs(expectedRegistration);
         }

         /// <summary>
         /// Verifies that the Attempting Generic Get Registration For A Nonexisting Entry Throws.
         /// </summary>
         [TestMethod]
         [ExpectedException(typeof(KeyNotFoundException))]
         public void AttemptingNamedGenericGetRegistrationForANonexistingEntryThrows()
         {
             var result = iocContainer.GetRegistration<IFoo>("Bob");

         }

         /// <summary>
         /// Verifies that the Attempting NonGeneric Get Registration For A Nonexisting Entry Throws.
         /// </summary>
         [TestMethod]
         [ExpectedException(typeof(KeyNotFoundException))]
         public void AttemptingNonGenericNamedGetRegistrationForANonexistingEntryThrows()
         {
             var result = iocContainer.GetRegistration("Bob", typeof(IFoo));

         }
         /// <summary>
         /// Verifies that the Attempting Generic Get Registration For A Nonexisting Entry Throws.
         /// </summary>
         [TestMethod]
         [ExpectedException(typeof(KeyNotFoundException))]
         public void AttemptingNamelessGenericGetRegistrationForANonexistingEntryThrows()
         {
             var result = iocContainer.GetRegistration<IFoo>();
         }

         /// <summary>
         /// Verifies that the Attempting NonGeneric Get Registration For A Nonexisting Entry Throws.
         /// </summary>
         [TestMethod]
         [ExpectedException(typeof(KeyNotFoundException))]
         public void AttemptingNonGenericNamelessGetRegistrationForANonexistingEntryThrows()
         {
             var result = iocContainer.GetRegistration(typeof(IFoo));
         }

        /// <summary>
         /// Verifies that Generic GetRegistrations Includes All But Only Expected Registrations.
        /// </summary>
         [TestMethod]
         public void GenericGetRegistrationsIncludesAllButOnlyExpectedRegistrations()
         {
             var namelessFoo = iocContainer.Register<IFoo>(c => new Foo1());
             var bobFoo      = iocContainer.Register<IFoo>("Bob", c=> new Foo2());
             var billFoo     = iocContainer.Register<IFoo>("Bill", c => new Foo2());
             var janeBar     = iocContainer.Register<IBar>("Jane", c => new Bar1());

             var result      = iocContainer.GetRegistrations<IFoo>();

             Verify.That(result).IsACollection()
                        .IsNotNull()
                        .IsAnInstanceOfType(typeof(List<IRegistration>))
                        .CountIsEqualTo(3)
                        .AllItemsAreInstancesOfType(typeof(IRegistration))
                        .AllItemsAreNotNull()
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
             var bobFoo      = iocContainer.Register("Bob", typeof(IFoo), c => new Foo2());
             var billFoo     = iocContainer.Register("Bill", typeof(IFoo), c => new Foo2());
             var janeBar     = iocContainer.Register("Jane", typeof(IBar), c => new Bar1());

             var result      = iocContainer.GetRegistrations(typeof(IFoo));

             Verify.That(result).IsACollection()
                        .IsNotNull()
                        .IsAnInstanceOfType(typeof(List<IRegistration>))
                        .CountIsEqualTo(3)
                        .AllItemsAreInstancesOfType(typeof(IRegistration))
                        .AllItemsAreNotNull()
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
             var namelessFoo = iocContainer.Register<IFoo>(c => new Foo1());
             var bobFoo      = iocContainer.Register<IFoo>("Bob", c=> new Foo2());
             var billFoo     = iocContainer.Register<IFoo>("Bill", c => new Foo2());

             var result      = iocContainer.GetRegistrations<IBar>();

             Verify.That(result).IsACollection()
                        .IsNotNull()
                        .IsAnInstanceOfType(typeof(List<IRegistration>))
                        .CountIsEqualTo(0);
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

             Verify.That(iocContainer.LifeTimeManager).IsTheSameObjectAs(aLifetimeManager);
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
             using (var container = new Container())
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
             using (var container = new Container())
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
             using (var container = new Container())
             {
                 container.Register<IFoo>("Foo1", c => new Foo1());
                 container.Register<IFoo>("Foo2", c => new Foo2());
                 var result1 = container.Resolve<IFoo>("Foo1");
                 var result2 = container.Resolve<IFoo>("Foo2");

                 Verify.That(result1)
                            .IsNotNull()
                            .IsAnInstanceOfType(typeof(IFoo))
                            .IsAnInstanceOfType(typeof(Foo1));

                 Verify.That(result2)
                            .IsNotNull()
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
             using (var container = new Container())
             {
                 container.Register<IFoo>(c => new Foo1());
                 container.Register<IFoo>("Foo2", c => new Foo2());
                 var result1 = container.Resolve<IFoo>();
                 var result2 = container.Resolve<IFoo>("Foo2");

                 Verify.That(result1)
                            .IsNotNull()
                            .IsAnInstanceOfType(typeof(IFoo))
                            .IsAnInstanceOfType(typeof(Foo1));

                 Verify.That(result2)
                            .IsNotNull()
                            .IsAnInstanceOfType(typeof(IFoo))
                            .IsAnInstanceOfType(typeof(Foo2))
                            .IsNotTheSameObjectAs(result1);
             }

         }

         #endregion

    }
}
