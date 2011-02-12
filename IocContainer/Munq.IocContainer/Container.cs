using System;
using System.Collections.Generic;
using System.Linq;

namespace Munq
{
    public class Container : IIocContainer
    {
		private TypeRegistry typeRegistry = new TypeRegistry();

        // Track whether Dispose has been called.
        private bool disposed;

        // null for the lifetime manager is the same as AlwaysNew, but slightly faster.
        private ILifetimeManager defaultLifetimeManager = null;

        public ILifetimeManager LifeTimeManager { get { return defaultLifetimeManager; } }

        #region Register Func Members
        /// <summary>
        /// Registers a function to create instances of a type
        /// </summary>
        /// <typeparam name="TType">The type being registered</typeparam>
        /// <param name="func">The function that creates the type.  
        /// The function takes a single parameter of type Container</param>
        /// <returns>An IRegistration that can be used to configure the behavior of the registration</returns>
		public IRegistration Register<TType>(Func<IDependencyResolver, TType> func) 
            where TType : class
        {
            return Register(null, typeof(TType), c => (func(c) as Object));
        }

		public IRegistration Register<TType>(string name, Func<IDependencyResolver, TType> func) 
            where TType : class
        {
            return Register(name, typeof(TType), c =>( func(c) as Object));
        }

		public IRegistration Register(Type type, Func<IDependencyResolver, object> func)
        {
			return Register(null, type, func);
        }

		public IRegistration Register(string name, Type type, Func<IDependencyResolver, object> func)
        {
            if (func == null)
                throw new ArgumentNullException("func");

            var entry = new Registration(this, name, type, func);
            entry.WithLifetimeManager(defaultLifetimeManager);

            typeRegistry.Add(entry);

            return entry;
        }
		#endregion        

		#region Register Type Methods
		public IRegistration Register<TType, TImpl>()
			where TType : class
			where TImpl : class, TType
		{
			return Register<TType>(CreateInstanceDelegateFactory.Create<TType, TImpl>());
		}

		public IRegistration Register<TType, TImpl>(string name)
			where TType : class
			where TImpl : class, TType
		{
			return Register<TType>(name, CreateInstanceDelegateFactory.Create<TType, TImpl>());
		}

		public IRegistration Register(Type tType, Type tImpl)
		{
			return Register(tType, CreateInstanceDelegateFactory.Create(tImpl));
		}

		public IRegistration Register(string name, Type tType, Type tImpl)
		{
			return Register(name, tType, CreateInstanceDelegateFactory.Create(tImpl));
		}
		#endregion

        #region RegisterInstance Members
        public IRegistration RegisterInstance<TType>(TType instance) where TType : class
        { 
            return Register<TType>(c => instance).WithLifetimeManager(null); 
        }

        public IRegistration RegisterInstance<TType>(string name, TType instance) where TType : class
        {
            return Register<TType>(name, c => instance).WithLifetimeManager(null); 
        }
        
        public IRegistration RegisterInstance(Type type, object instance)
        {
            return Register(type, c => instance).WithLifetimeManager(null);
        }

        public IRegistration RegisterInstance(string name, Type type, object instance)
        {
            return Register(name, type, c => instance).WithLifetimeManager(null);
        }
        #endregion

        public void Remove(IRegistration ireg)
        {
			typeRegistry.Remove(ireg);
        }

		#region GetRegistration Members
		/// <summary>
		/// Returns an Registration of a registered type
		/// </summary>
		/// <typeparam name="TType">The type to get the Registration for</typeparam>
		/// <returns>An Registration for the type.  Throws a KeyNoFoundException if not registered.</returns>
		public IRegistration GetRegistration<TType>() where TType : class
		{
			return typeRegistry.Get(null, typeof(TType));
		}

		public IRegistration GetRegistration<TType>(string name) where TType : class
		{
			return typeRegistry.Get(name, typeof(TType));
		}

		public IRegistration GetRegistration(Type type)
		{
			return typeRegistry.Get(null, type);
		}

		public IRegistration GetRegistration(string name, Type type)
		{
			return typeRegistry.Get(name, type);
		}

		public List<IRegistration> GetRegistrations<TType>() where TType : class
		{
			return GetRegistrations(typeof(TType));
		}

		public List<IRegistration> GetRegistrations(Type type)
		{
			return typeRegistry.All(type).Cast<IRegistration>().ToList();
		}

		#endregion

        #region Resolve Members
        /// <summary>
        /// Returns an instance of a registered type
        /// </summary>
        /// <typeparam name="TType">The type to resolve</typeparam>
        /// <returns>An instance of the type.  Throws a KeyNoFoundException if not registered.</returns>
        public TType Resolve<TType>() where TType : class
        {
			return typeRegistry.Get(null, typeof(TType)).GetInstance() as TType;			 
        }

        public TType Resolve<TType>(string name) where TType : class
        {
			return typeRegistry.Get(name, typeof(TType)).GetInstance() as TType; 
        }

        public object Resolve(Type type)
        {
			return typeRegistry.Get(null, type).GetInstance();
		}

        public object Resolve(string name, Type type)
        {
			return typeRegistry.Get(name, type).GetInstance();
		}        
        #endregion
        
        #region LazyResolve Members
        //--------------------------------------------------------
        // Lazy Resolve methods returns a delegate that, when called
        // resolves the instance.  Used in case where you wish to delay
        // the actual instantiation of the class as it may use a scarce 
        // resource, or logic may not need to resolve it at all.
        public Func<TType> LazyResolve<TType>() where TType : class
        {
            Func<TType> lazyMethod;
           try
            {
                lazyMethod = Resolve<Func<TType>>();
            }
            catch(KeyNotFoundException)
            {
                lazyMethod = () => this.Resolve<TType>();
                RegisterInstance<Func<TType>>(lazyMethod);
            }
            return lazyMethod;
        }

        public Func<TType> LazyResolve<TType>(string name) where TType : class
        {
            Func<TType> lazyMethod;
            try
            {
                lazyMethod = Resolve<Func<TType>>(name);
            }
            catch(KeyNotFoundException)
            {
                lazyMethod = () => this.Resolve<TType>(name);
                RegisterInstance<Func<TType>>(lazyMethod);
            }
            return lazyMethod;
        }

        public Func<Object> LazyResolve(Type type)
        {
			return LazyResolve(null, type);
		}

        public Func<Object> LazyResolve(string name, Type type)
        {
            var entry = typeRegistry.Get(name, type);
            return entry.LazyFactory;
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
					typeRegistry.Dispose();
                }
            }
            disposed = true;
        }

        ~Container() { Dispose(false); }

        #endregion

		#region Resolve All Methods
		public IEnumerable<object> ResolveAll(Type type)
		{
			var registrations = typeRegistry.All(type);
			var instances = new List<object>();
			foreach (var reg in registrations)
			{
				instances.Add(reg.GetInstance());
			}
			return instances;
		}

		public IEnumerable<TType> ResolveAll<TType>() where TType : class
		{
			var registrations = typeRegistry.All(typeof(TType));
			var instances = new List<TType>();
			foreach (var reg in registrations)
			{
				instances.Add(reg.GetInstance() as TType);
			}
			return instances;
		}
		#endregion

        #region Fluent Interface Members
        public IIocContainer UsesDefaultLifetimeManagerOf(ILifetimeManager lifetimeManager)
        {
            defaultLifetimeManager = lifetimeManager;
            return this;
        }       
        #endregion        
	}
}
