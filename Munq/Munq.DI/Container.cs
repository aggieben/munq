using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Munq.DI
{
    public class Container : IDisposable
    {
        private HybridDictionary typeRegistry = new HybridDictionary();
        // Track whether Dispose has been called.
        private bool disposed = false;

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
        public IRegistration Register<TType>(Func<Container, object> func) where TType : class
        { return Register(null, typeof(TType), func); }

        public IRegistration Register<TType>(string name, Func<Container, object> func) where TType : class
        { return Register(name, typeof(TType), func); }

        public IRegistration RegisterInstance<TType>(TType instance) where TType : class
        { return Register<TType>(c => instance); }

        public IRegistration RegisterInstance<TType>(string name, TType instance) where TType : class
        { return Register<TType>(name, c => instance); }
        
        public IRegistration Register(Type type, object func)
        { return Register(null, type, func); }

        public IRegistration Register(string name, Type type, object func)
        {
            if (func == null)
                throw new ArgumentNullException("func");

            var entry = new Registration(name, type, func);
            this.typeRegistry[entry] = entry;

            return entry;
        }
        /// <summary>
        /// Returns an instance of a registered type
        /// </summary>
        /// <typeparam name="TType">The type to resolve</typeparam>
        /// <returns>An instance of the type.  Throws a KeyNoFoundException if not registered.</returns>
        public TType Resolve<TType>() where TType : class
        { return (TType)Resolve(null, typeof(TType)); }

        public TType Resolve<TType>(string name) where TType : class
        { return (TType)Resolve(name, typeof(TType)); }
        
        public object Resolve(Type type)
        { return Resolve(null, type); }

        public object Resolve(string name, Type type)
        {
            var key = new Registration(name, type, null);
            var entry = (Registration)this.typeRegistry[key];
            if (entry == null)
                throw new KeyNotFoundException();

            try { return entry.GetInstance(this); }
            catch { throw new KeyNotFoundException(); }
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
