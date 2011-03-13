// --------------------------------------------------------------------------------------------------
// © Copyright 2011 by Matthew Dennis.
// Released under the Microsoft Public License (Ms-PL) http://www.opensource.org/licenses/ms-pl.html
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Munq.MVC3
{
	public class MunqDependencyResolver : System.Web.Mvc.IDependencyResolver
	{
		private readonly static Munq.IocContainer _container = new IocContainer();

		public static IocContainer Container { get { return _container; }}

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