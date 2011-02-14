using System;
using System.Collections.Specialized;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Munq
{
	internal class TypeRegistry : IDisposable
	{
		// Track whether Dispose has been called.
		private const int INITIAL_SIZE = 257; // a prime number greater than the initial size
		private readonly object _lock = new object();
		private bool disposed;
		private readonly IDictionary<IRegistrationKey, Registration> typeRegistrations =
			new ConcurrentDictionary<IRegistrationKey, Registration>(Environment.ProcessorCount * 2,
																		INITIAL_SIZE);

		public void Add(Registration reg)
		{
			IRegistrationKey key = MakeKey(reg.Name, reg.ResolvesTo);
			typeRegistrations[key] = reg;
		}

		public Registration Get(string name, Type type)
		{
			IRegistrationKey key = MakeKey(name, type);
			return typeRegistrations[key];
		}

		public IEnumerable<Registration> All(Type type)
		{
			return typeRegistrations.Values.Where(reg => reg.ResolvesTo == type);
		}

		public void Remove(IRegistration ireg)
		{
			IRegistrationKey key = MakeKey(ireg.Name, ireg.ResolvesTo);
			typeRegistrations.Remove(key);
			ireg.InvalidateInstanceCache();
		}

		private static IRegistrationKey MakeKey(string name, Type type)
		{
			IRegistrationKey key;
			if (name == null)
				key = new UnNamedRegistrationKey(type);
			else
				key = new NamedRegistrationKey(name, type);
			return key;
		}

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
					foreach (Registration reg in typeRegistrations.Values)
					{
						var instance = reg.Instance as IDisposable;
						if (instance != null)
						{
							instance.Dispose();
							reg.Instance = null;
						}
						reg.InvalidateInstanceCache();
					}
				}
			}
			disposed = true;
		}
		~TypeRegistry() { Dispose(false); }
	}
}
