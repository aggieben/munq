// --------------------------------------------------------------------------------------------------
// © Copyright 2011 by Matthew Dennis.
// Released under the Microsoft Public License (Ms-PL) http://www.opensource.org/licenses/ms-pl.html
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Munq
{
	public partial class IocContainer : IDependencyResolver
	{
		#region Resolve Members
		/// <include file='XmlDocumentation/IDependencyResolver.xml' path='IDependencyResolver/Members[@name="Resolve1"]/*' />
		public TType Resolve<TType>() where TType : class
		{
			return Resolve(null, typeof(TType)) as TType;
		}

		/// <include file='XmlDocumentation/IDependencyResolver.xml' path='IDependencyResolver/Members[@name="Resolve2"]/*' />
		public TType Resolve<TType>(string name) where TType : class
		{
			return Resolve(name, typeof(TType)) as TType;
		}

		/// <include file='XmlDocumentation/IDependencyResolver.xml' path='IDependencyResolver/Members[@name="Resolve3"]/*' />
		public object Resolve(Type type)
		{
			return Resolve(null, type);
		}

		/// <include file='XmlDocumentation/IDependencyResolver.xml' path='IDependencyResolver/Members[@name="Resolve4"]/*' />
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

		#region CanResolve Members

		/// <include file='XmlDocumentation/IDependencyResolver.xml' path='IDependencyResolver/Members[@name="CanResolve1"]/*' />
		public bool CanResolve<TType>()
				where TType : class
		{
			return CanResolve(null, typeof(TType));
		}

		/// <include file='XmlDocumentation/IDependencyResolver.xml' path='IDependencyResolver/Members[@name="CanResolve2"]/*' />
		public bool CanResolve<TType>(string name)
				where TType : class
		{
			return CanResolve(name, typeof(TType));
		}

		/// <include file='XmlDocumentation/IDependencyResolver.xml' path='IDependencyResolver/Members[@name="CanResolve3"]/*' />
		public bool CanResolve(Type type)
		{
			return CanResolve(null, type);
		}

		/// <include file='XmlDocumentation/IDependencyResolver.xml' path='IDependencyResolver/Members[@name="CanResolve4"]/*' />
		public bool CanResolve(string name, Type type)
		{
			return typeRegistry.ContainsKey(name, type);
		}
		#endregion

		#region Resolve All Methods
		/// <include file='XmlDocumentation/IDependencyResolver.xml' path='IDependencyResolver/Members[@name="ResolveAll1"]/*' />
		public IEnumerable<TType> ResolveAll<TType>() where TType : class
		{
			return ResolveAll(typeof(TType)).Cast<TType>();
		}

		/// <include file='XmlDocumentation/IDependencyResolver.xml' path='IDependencyResolver/Members[@name="ResolveAll2"]/*' />
		public IEnumerable<object> ResolveAll(Type type)
		{
			var registrations = typeRegistry.All(type);
			var instances = new List<object>();
			foreach (var reg in registrations)
			{
				instances.Add(reg.GetInstance());
			}
			return instances;
		}
		#endregion
	
		#region LazyResolve Members
		/// <include file='XmlDocumentation/IDependencyResolver.xml' path='IDependencyResolver/Members[@name="LazyResolve1"]/*' />
		public Func<TType> LazyResolve<TType>() where TType : class
		{
			return LazyResolve<TType>(null);
		}

		/// <include file='XmlDocumentation/IDependencyResolver.xml' path='IDependencyResolver/Members[@name="LazyResolve2"]/*' />
		public Func<TType> LazyResolve<TType>(string name) where TType : class
		{
			return () => Resolve<TType>(name);
		}

		/// <include file='XmlDocumentation/IDependencyResolver.xml' path='IDependencyResolver/Members[@name="LazyResolve3"]/*' />
		public Func<Object> LazyResolve(Type type)
		{
			return LazyResolve(null, type);
		}

		/// <include file='XmlDocumentation/IDependencyResolver.xml' path='IDependencyResolver/Members[@name="LazyResolve4"]/*' />
		/// <inheritdoc />
		public Func<Object> LazyResolve(string name, Type type)
		{
			return () => Resolve(name, type);
		}
		#endregion
	}
}
