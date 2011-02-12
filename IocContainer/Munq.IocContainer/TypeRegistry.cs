using System;
using System.Collections.Specialized;
using System.Linq;
using System.Collections.Generic;

namespace Munq
{
	internal class TypeRegistry : IDisposable
	{
		// Track whether Dispose has been called.
		private bool disposed;
		private Dictionary<IRegistrationKey, Registration> typeRegistry = 
												new Dictionary<IRegistrationKey, Registration>();

		public void Add(Registration reg)
		{
			typeRegistry[MakeKey(reg.Name, reg.ResolvesTo)] = reg;
		}

		public Registration Get(string name, Type type)
		{
			var entry = (typeRegistry[MakeKey(name, type)]);
			if (entry == null)
				throw new KeyNotFoundException();
			return entry;
		}

		public IQueryable<Registration> All(Type type)
		{
			return typeRegistry.Values
					.AsQueryable()
					.Cast<Registration>()
					.Where(reg => reg.ResolvesTo == type);
		}

		public void Remove(IRegistration ireg)
		{
			typeRegistry.Remove(MakeKey(ireg.Name, ireg.ResolvesTo));

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
					foreach (Registration reg in typeRegistry.Values)
					{
						var instance = reg.Instance as IDisposable;
						if (instance != null)
						{
							instance.Dispose();
							reg.Instance = null;
						}
					}
				}
			}
			disposed = true;
		}
		~TypeRegistry() { Dispose(false); }
	}
}
