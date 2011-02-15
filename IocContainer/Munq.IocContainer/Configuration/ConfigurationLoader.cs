using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Reflection;

namespace Munq.Configuration
{
    public class ConfigurationLoader
    {
        /// <summary>
        /// Find all the types that implement the IMunqConfig interface, create an instance and 
        /// then call the RegisterIn method on the type.
        /// </summary>
        /// <param name="container">The Munq IOC container to register class factories in.</param>
		public static void FindAndRegisterDependencies(Container container)
		{
			// get all the assemblies in the bin directory
			string binPath = HttpContext.Current != null ? HttpContext.Current.Server.MapPath("/bin")
														 : Environment.CurrentDirectory;
			CallRegistrarsInDirectory(container, binPath);
		}

		public static void CallRegistrarsInDirectory(Container container, string binPath, string filePattern = "*.dll")
		{
			var assemblyNames = Directory.GetFiles(binPath, filePattern);

			foreach (var filename in assemblyNames)
				CallRegistrarsInAssembly(container, filename);

		}
		public static void CallRegistrarsInAssembly(Container container, string filename)
		{
			var assembly = Assembly.LoadFile(filename);
			// find all the types that implements IMunqConfig ...
			var registrars = assembly.GetExportedTypes()
				 .Where(type => type.GetInterface(typeof(IMunqConfig).ToString()) != null);

			// and call the RegisterIn method on each
			foreach (var registrar in registrars)
				(Activator.CreateInstance(registrar) as IMunqConfig).RegisterIn(container);
		}
	}
}
