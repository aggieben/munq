using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq
{
	public partial class IocContainer : IDependecyRegistrar
	{
		#region Register Func Members
		public IRegistration Register<TType>(Func<IDependencyResolver, TType> func)
			where TType : class
		{
			return Register(null, typeof(TType), c => (func(c) as Object));
		}

		public IRegistration Register<TType>(string name, Func<IDependencyResolver, TType> func)
			where TType : class
		{
			return Register(name, typeof(TType), c => (func(c) as Object));
		}

		public IRegistration Register(Type type, Func<IDependencyResolver, object> func)
		{
			return Register(null, type, func);
		}

		public IRegistration Register(string name, Type type, Func<IDependencyResolver, object> func)
		{
			if (func == null)
				throw new ArgumentNullException("func");

			var entry = new Registration(this, name, type, func);
			entry.WithLifetimeManager(DefaultLifetimeManager);
			typeRegistry.Add(entry);
			return entry;
		}
		#endregion

		#region Register Type Methods
		public IRegistration Register<TType, TImpl>()
			where TType : class
			where TImpl : class, TType
		{
			return Register(typeof(TType), typeof(TImpl));
		}

		public IRegistration Register<TType, TImpl>(string name)
			where TType : class
			where TImpl : class, TType
		{
			return Register(name, typeof(TType), typeof(TImpl));
		}

		public IRegistration Register(Type tType, Type tImpl)
		{
			return Register(null, tType, tImpl);
		}

		public IRegistration Register(string name, Type tType, Type tImpl)
		{
			return Register(name, tType, CreateInstanceDelegateFactory.Create(tImpl));
		}
		#endregion

		#region RegisterInstance Members
		public IRegistration RegisterInstance<TType>(TType instance) where TType : class
		{
			return Register<TType>(c => instance).WithLifetimeManager(null);
		}

		public IRegistration RegisterInstance<TType>(string name, TType instance) where TType : class
		{
			return Register<TType>(name, c => instance).WithLifetimeManager(null);
		}

		public IRegistration RegisterInstance(Type type, object instance)
		{
			return Register(type, c => instance).WithLifetimeManager(null);
		}

		public IRegistration RegisterInstance(string name, Type type, object instance)
		{
			return Register(name, type, c => instance).WithLifetimeManager(null);
		}
		#endregion

		public void Remove(IRegistration ireg)
		{
			typeRegistry.Remove(ireg);
		}

		#region GetRegistration Members
		/// <summary>
		/// Returns an Registration of a registered type
		/// </summary>
		/// <typeparam name="TType">The type to get the Registration for</typeparam>
		/// <returns>An Registration for the type.  Throws a KeyNoFoundException if not registered.</returns>
		public IRegistration GetRegistration<TType>() where TType : class
		{
			return GetRegistration(null, typeof(TType));
		}

		public IRegistration GetRegistration<TType>(string name) where TType : class
		{
			return GetRegistration(name, typeof(TType));
		}

		public IRegistration GetRegistration(Type type)
		{
			return GetRegistration(null, type);
		}

		public IRegistration GetRegistration(string name, Type type)
		{
			return typeRegistry.Get(name, type);
		}

		public IEnumerable<IRegistration> GetRegistrations<TType>() where TType : class
		{
			return GetRegistrations(typeof(TType));
		}

		public IEnumerable<IRegistration> GetRegistrations(Type type)
		{
			return typeRegistry.All(type).Cast<IRegistration>();
		}
		#endregion
	}
}
