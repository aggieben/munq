using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq
{
	public partial class IocContainer : IDependecyRegistrar
	{
		#region Register Func Members
		/// <inheritdoc />
		public IRegistration Register<TType>(Func<IDependencyResolver, TType> func)
			where TType : class
		{
			return Register(null, typeof(TType), c => (func(c) as Object));
		}

		/// <inheritdoc />
		public IRegistration Register<TType>(string name, Func<IDependencyResolver, TType> func)
			where TType : class
		{
			return Register(name, typeof(TType), c => (func(c) as Object));
		}

		/// <inheritdoc />
		public IRegistration Register(Type type, Func<IDependencyResolver, object> func)
		{
			return Register(null, type, func);
		}

		/// <inheritdoc />
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
		/// <inheritdoc />
		public IRegistration Register<TType, TImpl>()
			where TType : class
			where TImpl : class, TType
		{
			return Register(typeof(TType), typeof(TImpl));
		}

		/// <inheritdoc />
		public IRegistration Register<TType, TImpl>(string name)
			where TType : class
			where TImpl : class, TType
		{
			return Register(name, typeof(TType), typeof(TImpl));
		}

		/// <inheritdoc />
		public IRegistration Register(Type tType, Type tImpl)
		{
			return Register(null, tType, tImpl);
		}

		/// <inheritdoc />
		public IRegistration Register(string name, Type tType, Type tImpl)
		{
			return Register(name, tType, CreateInstanceDelegateFactory.Create(tImpl));
		}
		#endregion

		#region RegisterInstance Members
		/// <inheritdoc />
		public IRegistration RegisterInstance<TType>(TType instance) where TType : class
		{
			return Register<TType>(c => instance).WithLifetimeManager(null);
		}

		/// <inheritdoc />
		public IRegistration RegisterInstance<TType>(string name, TType instance) where TType : class
		{
			return Register<TType>(name, c => instance).WithLifetimeManager(null);
		}

		/// <inheritdoc />
		public IRegistration RegisterInstance(Type type, object instance)
		{
			return Register(type, c => instance).WithLifetimeManager(null);
		}

		/// <inheritdoc />
		public IRegistration RegisterInstance(string name, Type type, object instance)
		{
			return Register(name, type, c => instance).WithLifetimeManager(null);
		}
		#endregion

		/// <inheritdoc />
		public void Remove(IRegistration ireg)
		{
			typeRegistry.Remove(ireg);
		}

		#region GetRegistration Members
		/// <inheritdoc />
		public IRegistration GetRegistration<TType>() where TType : class
		{
			return GetRegistration(null, typeof(TType));
		}

		/// <inheritdoc />
		public IRegistration GetRegistration<TType>(string name) where TType : class
		{
			return GetRegistration(name, typeof(TType));
		}

		/// <inheritdoc />
		public IRegistration GetRegistration(Type type)
		{
			return GetRegistration(null, type);
		}

		/// <inheritdoc />
		public IRegistration GetRegistration(string name, Type type)
		{
			return typeRegistry.Get(name, type);
		}

		/// <inheritdoc />
		public IEnumerable<IRegistration> GetRegistrations<TType>() where TType : class
		{
			return GetRegistrations(typeof(TType));
		}

		/// <inheritdoc />
		public IEnumerable<IRegistration> GetRegistrations(Type type)
		{
			return typeRegistry.All(type).Cast<IRegistration>();
		}
		#endregion
	}
}
