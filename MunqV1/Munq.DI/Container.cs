using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Munq.DI
{
    public partial class Container : IDisposable
    {
		private HybridDictionary typeRegistry = new HybridDictionary();

        // Track whether Dispose has been called.
        private bool disposed = false;
		private ILifetimeManager defaultLifetimeManager = null;
		
        /// <summary> Creates an DI Container </summary>
        public Container() { }

        #region Register Members
        /// <summary>
        /// Registers a function to create instances of a type
        /// </summary>
        /// <typeparam name="TType">The type being registered</typeparam>
        /// <param name="func">The function that creates the type.  
        /// The function takes a single paramenter of type Container</param>
        /// <returns>An IRegistration that can be used to configure the behaviour of the registration</returns>
        public IRegistration Register<TType>(Func<Container, TType> func) where TType : class
        { return Register(typeof(TType), c => (object)func(c)); }

        public IRegistration Register<TType>(string name, Func<Container, TType> func) where TType : class
        { return Register(name, typeof(TType), c => (object)func(c)); }


        public IRegistration Register(Type type, Func<Container, object> func)
        {
            if (func == null)
                throw new ArgumentNullException("func");

            var entry = new Registration(null, type, func);
            entry.WithLifetimeManager(defaultLifetimeManager);

            this.typeRegistry[new UnNamedRegistrationKey(type)] = entry;

            return entry;
        }

        public IRegistration Register(string name, Type type, Func<Container, object> func)
        {
            if (func == null)
                throw new ArgumentNullException("func");

            var entry = new Registration(name, type, func);
            entry.WithLifetimeManager(defaultLifetimeManager);

            typeRegistry[new NamedRegistrationKey(name, type)] = entry;

            return entry;
        }
        
        #endregion        

        #region RegisterInstance Members
		public IRegistration RegisterInstance<TType>(TType instance) where TType : class
        { return Register<TType>(c => instance); }

        public IRegistration RegisterInstance<TType>(string name, TType instance) where TType : class
        { return Register<TType>(name, c => instance); }
        
        public IRegistration RegisterInstance(Type type, object instance)
        {
			return Register(type, c => instance);
        }

		public IRegistration RegisterInstance(string name, Type type, object instance)
		{
			return Register(name, type, c => instance);
		}
	    #endregion
	    
	    #region Resolve Members
        /// <summary>
        /// Returns an instance of a registered type
        /// </summary>
        /// <typeparam name="TType">The type to resolve</typeparam>
        /// <returns>An instance of the type.  Throws a KeyNoFoundException if not registered.</returns>
        public TType Resolve<TType>() where TType : class
        { return (TType)Resolve(typeof(TType)); }

        public TType Resolve<TType>(string name) where TType : class
        { return (TType)Resolve(name, typeof(TType)); }

        public object Resolve(Type type)
        {
            var entry = (Registration)typeRegistry[new UnNamedRegistrationKey(type)];

            try
            {
                // optimization for default case
                return (entry.LifetimeManager == null)
                    ? entry.Factory(this)
                    : entry.GetInstance(this);
            }
            catch { throw new KeyNotFoundException(); }
        }

        public object Resolve(string name, Type type)
        {
            var entry = (Registration)typeRegistry[new NamedRegistrationKey(name, type)];

            try
            {
                // optimization for default case
                return (entry.LifetimeManager == null)
                    ? entry.Factory(this)
                    : entry.GetInstance(this);
            }
            catch { throw new KeyNotFoundException(); }
        }        
        #endregion
        
        #region LazyResolve Members
        //--------------------------------------------------------
        // Lazy Resolve methods returns a delegate that, when called
        // resolves the instance.  Used in case where you wish to delay
        // the actual instantation of the class as it may use a scarce 
        // resource, or logic may not need to resolve it at all.
        Func<TType> LazyResolve<TType>() where TType : class
        {
            return () => Resolve(null, typeof(TType)) as TType;
        }

        Func<TType> LazyResolve<TType>(string name) where TType : class
        {
            return () => Resolve(name, typeof(TType)) as TType;
        }

        Func<Object> LazyResolve(Type type)
        {
            return () => Resolve(null, type);
        }

        Func<Object> LazyResolve(string name, Type type)
        {
            return () => Resolve(name, type);
        }       
        #endregion

        #region GetRegistration Members
        /// <summary>
        /// Returns an Registration of a registered type
        /// </summary>
        /// <typeparam name="TType">The type to get the Registration for</typeparam>
        /// <returns>An Registration for the type.  Throws a KeyNoFoundException if not registered.</returns>
        public IRegistration GetRegistration<TType>() where TType : class
        { return GetRegistration(typeof(TType)); }

        public IRegistration GetRegistration<TType>(string name) where TType : class
        { return GetRegistration(name, typeof(TType)); }

        public IRegistration GetRegistration(Type type)
        { return (IRegistration)typeRegistry[new UnNamedRegistrationKey(type)];}

        public IRegistration GetRegistration(string name, Type type)
        { return (IRegistration)typeRegistry[new NamedRegistrationKey(name, type)];}
        
        public List<IRegistration> GetRegistrations<TType>() where TType : class
        {
            return GetRegistrations(typeof(TType));
        }
        
        public List<IRegistration> GetRegistrations(Type type)
        {
            List<IRegistration> registrations = new List<IRegistration>();
            foreach (IRegistrationKey key in typeRegistry.Keys)
            {
                if (key.GetInstanceType() == type)
                    registrations.Add((IRegistration)typeRegistry[key]);
            }
            return registrations;
        }
        
        #endregion

        #region Fluent Interface Members
        public Container UsesDefaultLifetimeManagerOf(ILifetimeManager lifetimeManager)
        {
            defaultLifetimeManager = lifetimeManager;
            return this;
        }       
        #endregion        

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!disposed)
            {
                // If disposing equals true, dispose all ContainerLifetime instances
                if (disposing)
                {
                    foreach (Registration reg in typeRegistry.Values)
                    {
                        var instance = reg.Instance as IDisposable;
                        if (instance != null)
                        {
                            instance.Dispose();
                            reg.Instance = null;
                        }
                    }
                }
            }
            disposed = true;
        }

        ~Container() { Dispose(false); }

        #endregion
    }
}
