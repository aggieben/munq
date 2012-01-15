// --------------------------------------------------------------------------------------------------
// © Copyright 2011 by Matthew Dennis.
// Released under the Microsoft Public License (Ms-PL) http://www.opensource.org/licenses/ms-pl.html
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Munq.MVC3
{
	/// <summary>
	/// Implements the MVC3 IDependencyResolver interface using the Munq IOC Container.
	/// Requires a reference to Munq.MVC3.dll.
	/// </summary>
	public class MunqDependencyResolver : System.Web.Mvc.IDependencyResolver, IDisposable
	{
		private static Munq.IocContainer _container = new IocContainer();

		/// <summary>
		/// Gets the Munq IocContainer used by the MVC3 IDependencyResolver.
		/// </summary>
		public static IocContainer Container { get { return _container; }}

		/// <summary>
		/// Resolves the specified type.
		/// </summary>
		/// <param name="serviceType">The type to resolve.</param>
		/// <returns>The instance the type resolves to, or null.</returns>
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

		/// <summary>
		/// Resolves all possible instances of the specified type.
		/// </summary>
		/// <param name="serviceType">The type to resolve.</param>
		/// <returns>The list of instances the type resolves to, may be empty.</returns>
		public IEnumerable<object> GetServices(Type serviceType)
		{
			return Container.ResolveAll(serviceType);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
				if (_container != null)
				{
					_container.Dispose();
					_container = null;
				}
		}

		~MunqDependencyResolver()
		{
			Dispose(false);
		}
	}
}