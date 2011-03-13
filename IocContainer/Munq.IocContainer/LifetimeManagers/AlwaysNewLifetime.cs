namespace Munq.LifetimeManagers
{
	/// <summary>
	/// A Lifetime Manager that always returns a new instance.
	/// </summary>
	public class AlwaysNewLifetime : ILifetimeManager 
	{
		#region ILifetimeManager Members

		/// <summary>
		/// Always creates a new instance.
		/// </summary>
		/// <param name="registration">The creator (registration) that can create an instance</param>
		/// <returns>The new instance.</returns>
		public object GetInstance(IRegistration registration)
		{
			return registration.CreateInstance();
		}

		/// <summary>
		/// Does nothing as this lifetime manager does not cache.
		/// </summary>
		/// <param name="registration">The registration.</param>
		public void InvalidateInstanceCache(IRegistration registration)
		{
		   // there is no instance cache ...
		}
	   #endregion
	}
}
