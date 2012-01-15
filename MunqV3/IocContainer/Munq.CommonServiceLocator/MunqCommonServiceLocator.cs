// --------------------------------------------------------------------------------------------------
// © Copyright 2011 by Matthew Dennis.
// Released under the Microsoft Public License (Ms-PL) http://www.opensource.org/licenses/ms-pl.html
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;

namespace Munq.CommonServiceLocator
{
	/// <summary>
	/// An implementation of the Common Service Locator interface using the Munq IOC Container.
	/// Requires a reference Munq.CommonServiceLocator.dll.
	/// </summary>
	public class MunqCommonServiceLocator : IServiceLocator, IDisposable
	{
		private Munq.IocContainer _container;

		/// <summary>
		/// Initializes an instance of the MunqCommonServiceLocator class.  Registers the instance
		/// to resolve IServiceLocator.
		/// </summary>
		public MunqCommonServiceLocator(Munq.IocContainer container)
		{
			container.RegisterInstance<IServiceLocator>(this);
			_container = container;
		}

		/// <summary>
		/// Initializes an instance of the MunqCommonServiceLocator class.  Registers the instance
		/// to resolve IServiceLocator.
		/// </summary>
		public MunqCommonServiceLocator() : this (new Munq.IocContainer())
		{
		}

		/// <summary>
		/// Gets the IOC Container used by the service locator.
		/// </summary>
		public Munq.IocContainer Container { get { return _container; } }

		/// <summary>
		/// Gets a list of a instances that resolve the specified type.
		/// </summary>
		/// <typeparam name="TService">The type of the service to resolve instances of.</typeparam>
		/// <returns>A list of instances.</returns>
		public IEnumerable<TService> GetAllInstances<TService>()
		{
			return GetAllInstances(typeof(TService)).Cast<TService>();
		}

		/// <summary>
		/// Gets a list of a instances that resolve the specified type.
		/// </summary>
		/// <param name="serviceType">The type of the service to resolve instances of.</param>
		/// <returns>A list of instances.</returns>
		public IEnumerable<object> GetAllInstances(Type serviceType)
		{
			return _container.ResolveAll(serviceType);
		}

		/// <summary>
		/// Resolve the specified type using a named instance.
		/// </summary>
		/// <typeparam name="TService">The type of the service to resolve instances of.</typeparam>
		/// <param name="key">The name to qualify the resolution.</param>
		/// <returns>The resolved instance, or throws and ActivationException.</returns>
		public TService GetInstance<TService>(string key)
		{
			return (TService)GetInstance(typeof(TService), key);
		}

		/// <summary>
		/// Resolve the specified type using a unnamed instance.
		/// </summary>
		/// <typeparam name="TService">The type of the service to resolve instances of.</typeparam>
		/// <returns>The resolved instance, or throws and ActivationException.</returns>
		public TService GetInstance<TService>()
		{
			return (TService)GetInstance(typeof(TService), null);
		}

		/// <summary>
		/// Resolve the specified type using a named instance.
		/// </summary>
		/// <param name="serviceType">The type of the service to resolve instances of.</param>
		/// <param name="key">The name to qualify the resolution.</param>
		/// <returns>The resolved instance, or throws and ActivationException.</returns>
		public object GetInstance(Type serviceType, string key)
		{
			try
			{
				return _container.Resolve(key, serviceType);
			}
			catch
			{
				throw new ActivationException();
			}
		}

		/// <summary>
		/// Resolve the specified type using a unnamed instance.
		/// </summary>
		/// <param name="serviceType">The type of the service to resolve instances of.</param>
		/// <returns>The resolved instance, or throws and ActivationException.</returns>
		public object GetInstance(Type serviceType)
		{
			return GetInstance(serviceType, null);
		}

		/// <summary>
		/// Resolve the specified type using a unnamed instance.
		/// </summary>
		/// <param name="serviceType">The type of the service to resolve instances of.</param>
		/// <returns>The resolved instance, or null.</returns>
		public object GetService(Type serviceType)
		{
				try
				{
				return GetInstance(serviceType);
				}
				catch (ActivationException)
				{
					return null;
				}
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
		~MunqCommonServiceLocator()
		{
			Dispose(false);
		}
	}
}
