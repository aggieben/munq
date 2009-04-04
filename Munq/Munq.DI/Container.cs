using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace Munq.DI
{
    public class Container
    {
        HybridDictionary TypeRegistry = new HybridDictionary();

        /// <summary>
        /// Creates an DI Container
        /// </summary>
        public Container() 
        {
        }

        /// <summary>
        /// Registers a function to create instances of a type
        /// </summary>
        /// <typeparam name="TType">The type being registered</typeparam>
        /// <param name="f">The function that creates the type.  The function takes a single paramenter of type Container</param>
        /// <returns>An IRegistration that can be used to configure the behaviour of the registration</returns>
        public IRegistration Register<TType>(Func<Container, TType> f) where TType:class
        {
            if (f == null)
                throw new ArgumentNullException();

            var key = new RegistrationKey<TType>();
            var entry = new Registration<TType>(f);
            TypeRegistry[key] = entry;

            return entry;
        }

        /// <summary>
        /// Returns and instance of a registered type
        /// </summary>
        /// <typeparam name="TType">The type to resolve</typeparam>
        /// <returns>An instance of the type.  Throws a KeyNoFoundException if not registered.</returns>
        public TType Resolve<TType>() where TType:class
        {
            try
            {
                var key = new RegistrationKey<TType>();
                var entry = (Registration<TType>)TypeRegistry[key];
                if (entry == null)
                    throw new KeyNotFoundException();

                return entry.GetInstance(this);

            }
            catch
            {
                throw new KeyNotFoundException();
            }
        }

        public IRegistration Register<TType>(string name, Func<Container, TType> f) where TType:class
        {
             if (f == null)
                throw new ArgumentNullException();

            var key = new NamedRegistrationKey<TType>(name);
            var entry = new Registration<TType>(f);
            TypeRegistry[key] = entry;

            return entry;
       }

        public TType Resolve<TType>(string name) where TType:class
        {
            try
            {
                var key = new NamedRegistrationKey<TType>(name);
                var entry = (Registration<TType>)TypeRegistry[key];
                if (entry == null)
                    throw new KeyNotFoundException();

                return entry.GetInstance(this);

            }
            catch
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
