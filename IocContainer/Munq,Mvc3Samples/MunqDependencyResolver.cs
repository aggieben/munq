using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Munq.Mvc3Samples
{
	public class MunqDependencyResolver : System.Web.Mvc.IDependencyResolver
	{
		private readonly static Munq.Container _container = new Container();

		public static Container Container { get { return _container; }}

		public object GetService(Type serviceType)
		{
			try
			{
				return Container.Resolve(serviceType);
			}
			catch (KeyNotFoundException)
			{
				return null;
			}
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return Container.ResolveAll(serviceType);
		}
	}
}