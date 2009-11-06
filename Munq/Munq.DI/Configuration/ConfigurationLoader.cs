using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace Munq.DI.Configuration
{
    public class ConfigurationLoader
    {
        public static void FindAndRegisterDependencies(Container container)
        {
            var assemblies = Directory.GetFiles(HttpContext.Current.Server.MapPath("/bin"), "*.dll")
                             .Select(fn => System.Reflection.Assembly.LoadFile(fn));
            foreach ( var assembly in assemblies)
            {
                var registrars = assembly.GetExportedTypes()
                     .Where(type => type.GetInterface("Munq.DI.Configuration.IMunqConfig") != null)
                     ;
                foreach( var registrar in registrars)
                   (Activator.CreateInstance(registrar) as IMunqConfig).RegisterIn(container);
            }                            
        }
    }
}
