using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq
{
	public partial class Container : IDependencyResolver
	{
		#region Resolve Members
		/// <summary>
		/// Returns an instance of a registered type
		/// </summary>
		/// <typeparam name="TType">The type to resolve</typeparam>
		/// <returns>An instance of the type.  Throws a KeyNoFoundException if not registered.</returns>
		public TType Resolve<TType>() where TType : class
		{
			return Resolve(null, typeof(TType)) as TType;
		}

		public TType Resolve<TType>(string name) where TType : class
		{
			return Resolve(name, typeof(TType)) as TType;
		}

		public object Resolve(Type type)
		{
			return Resolve(null, type);
		}

		public object Resolve(string name, Type type)
		{
			try
			{
				return typeRegistry.Get(name, type).GetInstance();
			}
			catch (KeyNotFoundException)
			{
				if (type.IsClass)
				{
					try
					{
						var func = CreateInstanceDelegateFactory.Create(type);
						Register(name, type, func);
						return func(this);
					}
					catch
					{
						throw new KeyNotFoundException();
					}
				}
			}

			throw new KeyNotFoundException();
		}
		#endregion

		#region LazyResolve Members
		//--------------------------------------------------------
		// Lazy Resolve methods returns a delegate that, when called
		// resolves the instance.  Used in case where you wish to delay
		// the actual instantiation of the class as it may use a scarce 
		// resource, or logic may not need to resolve it at all.
		public Func<TType> LazyResolve<TType>() where TType : class
		{
			//return LazyResolve<TType>(null);
			return LazyResolve<TType>(null);
		}

		public Func<TType> LazyResolve<TType>(string name) where TType : class
		{
			return () => Resolve<TType>(name);
		}

		public Func<Object> LazyResolve(Type type)
		{
			return LazyResolve(null, type);
		}

		public Func<Object> LazyResolve(string name, Type type)
		{
			return () => Resolve(name, type);
		}
		#endregion
	}
}
