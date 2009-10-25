using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Munq.DI
{
    public partial class Container : IDisposable
    {
		//private HybridDictionary typeRegistry = new HybridDictionary();
		private Dictionary<RegistrationKey, Registration> typeRegistry = 
			new Dictionary<RegistrationKey, Registration>();

        // Track whether Dispose has been called.
        private bool disposed = false;
		private ILifetimeManager defaultLifetimeManager = null;
        /// <summary>
        /// Creates an DI Container
        /// </summary>
        public Container() { }

        /// <summary>
        /// Registers a function to create instances of a type
        /// </summary>
        /// <typeparam name="TType">The type being registered</typeparam>
        /// <param name="func">The function that creates the type.  
		/// The function takes a single paramenter of type Container</param>
        /// <returns>An IRegistration that can be used to configure the behaviour of the registration</returns>
        public IRegistration Register<TType>(Func<Container, TType> func) where TType : class
        { return Register(null, typeof(TType), c=>(object)func(c)); }

        public IRegistration Register<TType>(string name, Func<Container, TType> func) where TType : class
		{ return Register(name, typeof(TType), c => (object)func(c)); }

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
		
		public IRegistration Register(Type type, Func<Container, object> func)
        { return Register(null, type, func); }

		public IRegistration Register(string name, Type type, Func<Container, object> func)
        {
            if (func == null)
                throw new ArgumentNullException("func");

            var entry = new Registration(type, func);
            entry.WithLifetimeManager(defaultLifetimeManager);
            var key = new RegistrationKey(name, type);
            this.typeRegistry[key] = entry;

            return entry;
        }
        /// <summary>
        /// Returns an instance of a registered type
        /// </summary>
        /// <typeparam name="TType">The type to resolve</typeparam>
        /// <returns>An instance of the type.  Throws a KeyNoFoundException if not registered.</returns>
        public TType Resolve<TType>() where TType : class
        { return Resolve(null, typeof(TType))as TType; }

        public TType Resolve<TType>(string name) where TType : class
        { return Resolve(name, typeof(TType)) as TType; }
        
        public object Resolve(Type type)
        { return Resolve(null, type); }

        public object Resolve(string name, Type type)
        {
            var key = new RegistrationKey(name, type);
            var entry = this.typeRegistry[key];
            if (entry == null)
                throw new KeyNotFoundException();

            try {	
				// optimization for default case
				return (entry.LifetimeManager == null)
					? entry.Factory(this)
					: entry.GetInstance(this); 
				}
            catch { throw new KeyNotFoundException(); }
        }
        
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
        
        public Container UsesDefaultLifetimeManagerOf(ILifetimeManager lifetimeManager)
        {
			defaultLifetimeManager = lifetimeManager;
			return this;
        }
        
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
