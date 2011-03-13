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
	public class MunqCommonServiceLocator : IServiceLocator
	{
		private readonly Munq.IDependencyResolver _resolver;

		public MunqCommonServiceLocator(Munq.IDependencyResolver _munqResolver)
		{
			_resolver = _munqResolver;
		}

		public IEnumerable<TService> GetAllInstances<TService>()
		{
			return GetAllInstances(typeof(TService)).Cast<TService>();
		}

		public IEnumerable<object> GetAllInstances(Type serviceType)
		{
			return _resolver.ResolveAll(serviceType);
		}

		public TService GetInstance<TService>(string key)
		{
			return (TService)GetInstance(typeof(TService), key);
		}

		public TService GetInstance<TService>()
		{
			return (TService)GetInstance(typeof(TService), null);
		}

		public object GetInstance(Type serviceType, string key)
		{
			try
			{
				return _resolver.Resolve(key, serviceType);
			}
			catch
			{
				throw new ActivationException();
			}
		}

		public object GetInstance(Type serviceType)
		{
			return GetInstance(serviceType, null);
		}
	
		public object  GetService(Type serviceType)
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
	}
}
