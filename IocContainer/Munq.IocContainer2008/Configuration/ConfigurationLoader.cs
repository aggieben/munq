using System;
using System.IO;
using System.Linq;
using System.Web;

namespace Munq.Configuration
{
    public class ConfigurationLoader
    {
        /// <summary>
        /// Find all the types that implement the IMunqConfig interface, create an instance and 
        /// then call the RegisterIn method on the type.
        /// </summary>
        /// <param name="container">The Munq IOC container to register class factories in.</param>
        public static void FindAndRegisterDependencies(IIocContainer container)
        {
            // get all the assemblies in the bin directory
            var assemblies = Directory.GetFiles(HttpContext.Current.Server.MapPath("/bin"), "*.dll")
                             .Select(fn => System.Reflection.Assembly.LoadFile(fn));

            foreach ( var assembly in assemblies)
            {
                // find all the types that implements IMunqConfig ...
                var registrars = assembly.GetExportedTypes()
                     .Where(type => type.GetInterface(typeof(IMunqConfig).ToString()) != null);

                // and call the RegisterIn method on each
                foreach( var registrar in registrars)
                   (Activator.CreateInstance(registrar) as IMunqConfig).RegisterIn(container);
            }                            
        }
    }
}
