using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Munq.DI
{
    public class Container: IDisposable
    {
        private HybridDictionary typeRegistry = new HybridDictionary();
        // Track whether Dispose has been called.
        private bool disposed = false;


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
        /// <param name="func">The function that creates the type.  The function takes a single paramenter of type Container</param>
        /// <returns>An IRegistration that can be used to configure the behaviour of the registration</returns>
        public IRegistration Register<TType>(Func<Container, TType> func) where TType : class
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            var key = new RegistrationKey<TType>();
            var entry = new Registration<TType>(func);
            this.typeRegistry[key] = entry;

            return entry;
        }

        /// <summary>
        /// Returns and instance of a registered type
        /// </summary>
        /// <typeparam name="TType">The type to resolve</typeparam>
        /// <returns>An instance of the type.  Throws a KeyNoFoundException if not registered.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public TType Resolve<TType>() where TType : class
        {
            try
            {
                var key = new RegistrationKey<TType>();
                var entry = (Registration<TType>)this.typeRegistry[key];
                if (entry == null)
                {
                    throw new KeyNotFoundException();
                }

                return entry.GetInstance(this);
            }
            catch
            {
                throw new KeyNotFoundException();
            }
        }

        public IRegistration Register<TType>(string name, Func<Container, TType> func) where TType : class
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            var key = new NamedRegistrationKey<TType>(name);
            var entry = new Registration<TType>(func);
            this.typeRegistry[key] = entry;

            return entry;
       }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public TType Resolve<TType>(string name) where TType : class
        {
            try
            {
                var key = new NamedRegistrationKey<TType>(name);
                var entry = (Registration<TType>)this.typeRegistry[key];
                if (entry == null)
                {
                    throw new KeyNotFoundException();
                }

                return entry.GetInstance(this);
            }
            catch
            {
                throw new KeyNotFoundException();
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            // Take yourself off the Finalization queue 
            // to prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

    // Dispose(bool disposing) executes in two distinct scenarios.
   // If disposing equals true, the method has been called directly
   // or indirectly by a user's code. Managed and unmanaged resources
   // can be disposed.
   // If disposing equals false, the method has been called by the 
   // runtime from inside the finalizer and you should not reference 
   // other objects. Only unmanaged resources can be disposed.
   protected virtual void Dispose(bool disposing)
   {
      // Check to see if Dispose has already been called.
      if(!this.disposed)
      {
         // If disposing equals true, dispose all ContainerLifetime instances
         if(disposing)
         {
             foreach ( Registration reg in this.typeRegistry.Values )
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

   // Use C# destructor syntax for finalization code.
   // This destructor will run only if the Dispose method 
   // does not get called.
   // It gives your base class the opportunity to finalize.
   // Do not provide destructors in types derived from this class.
   ~Container()      
   {
      // Do not re-create Dispose clean-up code here.
      // Calling Dispose(false) is optimal in terms of
      // readability and maintainability.
      Dispose(false);
   }

       #endregion
    }
}
