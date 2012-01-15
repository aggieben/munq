using System;
using System.Runtime.CompilerServices;
using System.Web;
using Munq.LifetimeManagers;

namespace Munq
{
	public static class LifetimeExtensions
	{
		readonly static CachedLifetime             cachedLifetime      = new CachedLifetime();
		readonly static ContainerLifetime          containerLifetime   = new ContainerLifetime();
		readonly static RequestLifetime            requestLifetime     = new RequestLifetime();
		readonly static SessionLifetime            sessionLifetime     = new SessionLifetime();
		readonly static ThreadLocalStorageLifetime threadLocalLifetime = new ThreadLocalStorageLifetime();

		public static IRegistration AsAlwaysNew(this IRegistration reg)
		{
			return reg.WithLifetimeManager(null);
		}

		public static IRegistration AsCached(this IRegistration reg)
		{
			return reg.WithLifetimeManager(cachedLifetime);
		}

		public static IRegistration AsContainerSingleton(this IRegistration reg)
		{
			return reg.WithLifetimeManager(containerLifetime);
		}

		public static IRegistration AsRequestSingleton(this IRegistration reg)
		{
			return reg.WithLifetimeManager(requestLifetime);
		}

		public static IRegistration AsSessionSingleton(this IRegistration reg)
		{
			return reg.WithLifetimeManager(sessionLifetime);
		}

		public static IRegistration AsThreadSingleton(this IRegistration reg)
		{
			return reg.WithLifetimeManager(threadLocalLifetime);
		}

		// for testing only
		public static HttpContextBase HttpContext
		{
			set
			{
				requestLifetime.SetContext(value);
				sessionLifetime.SetContext(value);
			} 
		}
	}
}
