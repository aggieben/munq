using System;
using System.Collections.Generic;

namespace Munq
{

	public interface IDependecyRegistrar
	{
		//Lifetime Manager
		ILifetimeManager DefaultLifetimeManager { get; set; }

		//Register
		IRegistration Register(string name, Type type, Func<IDependencyResolver, object> func);
		IRegistration Register(Type type, Func<IDependencyResolver, object> func);
		IRegistration Register<TType>(Func<IDependencyResolver, TType> func) where TType : class;
		IRegistration Register<TType>(string name, Func<IDependencyResolver, TType> func) where TType : class;

		//Register Instance
		IRegistration RegisterInstance(string name, Type type, object instance);
		IRegistration RegisterInstance(Type type, object instance);
		IRegistration RegisterInstance<TType>(string name, TType instance) where TType : class;
		IRegistration RegisterInstance<TType>(TType instance) where TType : class;

		//Register Type Implementation
		IRegistration Register<TType, TImpl>() where TType: class where TImpl : class, TType;
		IRegistration Register<TType, TImpl>(string name) where TType: class where TImpl : class, TType;
		IRegistration Register(Type tType, Type tImpl);
		IRegistration Register(string name, Type tType, Type tImpl);

		// Remove Registration
		void Remove(IRegistration ireg);

		//Get Registration
		IRegistration GetRegistration(string name, Type type);
		IRegistration GetRegistration(Type type);
		IRegistration GetRegistration<TType>() where TType : class;
		IRegistration GetRegistration<TType>(string name) where TType : class;

		//Get Registrations
		IEnumerable<IRegistration> GetRegistrations(Type type);
		IEnumerable<IRegistration> GetRegistrations<TType>() where TType : class;
	}
}