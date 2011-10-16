// --------------------------------------------------------------------------------------------------
// © Copyright 2011 by Matthew Dennis.
// Released under the Microsoft Public License (Ms-PL) http://www.opensource.org/licenses/ms-pl.html
// --------------------------------------------------------------------------------------------------

using System;

namespace Munq
{
	internal class Registration : IRegistration
	{
		internal ILifetimeManager LifetimeManager;
		internal Func<IDependencyResolver, object> Factory;
        private readonly string _key;
        private readonly Type _type;

		public object Instance;
        private readonly IDependencyResolver Container;
        private readonly object _lock = new object();


		public Registration(IDependencyResolver container, string name, Type type, 
							Func<IDependencyResolver, object> factory)
		{
			LifetimeManager = null;
			Container       = container;
			Factory         = factory;
			Name            = name;
			_type           = type;
            _key            = String.Format("[{0}]:{1}", (name ?? "null"), type.FullName);
		}

		public string Key
		{
			get { return _key; }
		}

		public Type ResolvesTo
		{
			get { return _type; }
		}

		public string Name { get; private set; }

		public IRegistration WithLifetimeManager(ILifetimeManager manager)
		{
			LifetimeManager = manager;
			return this;
		}

		public object GetCachedInstance()
		{
			if (Instance == null)
				lock (_lock)
				{
					if (Instance == null)
						Instance = Factory(Container);
				}
			return Instance;
		}

		public object CreateInstance()
		{
			return Factory(Container);
		}

		public object GetInstance()
		{
			return Instance ??
				((LifetimeManager != null) ? LifetimeManager.GetInstance(this) : Factory(Container));
		}

		public void InvalidateInstanceCache()
		{
			Instance = null;
			if (LifetimeManager != null)
				LifetimeManager.InvalidateInstanceCache(this);
		}
	}
}
