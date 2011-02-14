using System;
using System.Collections.Generic;
using System.Linq;

namespace Munq
{
	public partial class Container : IContainerFluent, IDisposable
	{
		private readonly TypeRegistry typeRegistry = new TypeRegistry();

		// Track whether Dispose has been called.
		private bool disposed;

		// null for the lifetime manager is the same as AlwaysNew, but slightly faster.
		public ILifetimeManager DefaultLifetimeManager { get; set; }

		#region Fluent Interface Members
		public IContainerFluent UsesDefaultLifetimeManagerOf(ILifetimeManager lifetimeManager)
		{
			DefaultLifetimeManager = lifetimeManager;
			return this;
		}
		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			// Check to see if Dispose has already been called.
			if (!disposed)
			{
				// If disposing equals true, dispose all ContainerLifetime instances
				if (disposing)
				{
					typeRegistry.Dispose();
				}
			}
			disposed = true;
		}

		~Container() { Dispose(false); }

		#endregion

		#region Resolve All Methods
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

		public IEnumerable<TType> ResolveAll<TType>() where TType : class
		{
			return ResolveAll(typeof(TType)).Cast<TType>();
		}
		#endregion

	}
}
