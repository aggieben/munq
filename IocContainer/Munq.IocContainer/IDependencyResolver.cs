using System;
using System.Collections.Generic;

namespace Munq
{
	public interface IDependencyResolver
	{
		//Resolve
		object Resolve(string name, Type type);
		object Resolve(Type type);
		TType Resolve<TType>() where TType : class;
		TType Resolve<TType>(string name) where TType : class;

		//ResolveAll
		IEnumerable<object> ResolveAll(Type type);
		IEnumerable<TType> ResolveAll<TType>() where TType : class;

		//Lazy Resolve
		Func<object> LazyResolve(string name, Type type);
		Func<object> LazyResolve(Type type);
		Func<TType> LazyResolve<TType>() where TType : class;
		Func<TType> LazyResolve<TType>(string name) where TType : class;
	}
}
