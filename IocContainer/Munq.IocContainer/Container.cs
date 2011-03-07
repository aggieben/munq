using System;

namespace Munq
{
	public partial class IocContainer : IContainerFluent, IDisposable
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

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <remarks>
		/// Disposes of all Container scoped (ContainerLifetime) instances cached in the type registry, and
		/// disposes of the type registry itself.
		/// </remarks>
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

		~IocContainer() { Dispose(false); }

		#endregion
	}
}
