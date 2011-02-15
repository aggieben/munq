using System;

namespace Munq
{
	internal class UnNamedRegistrationKey : IRegistrationKey
	{
		internal Type InstanceType;

		public UnNamedRegistrationKey(Type type)
		{
			InstanceType = type;
		}

		public Type GetInstanceType() { return InstanceType; }

		// comparison methods
		public override bool Equals(object obj)
		{
			var r = obj as UnNamedRegistrationKey;
			return (r != null) && Object.ReferenceEquals(InstanceType, r.InstanceType);
		}

		public override int GetHashCode()
		{
			return InstanceType.GetHashCode();
		}
	}

	internal class NamedRegistrationKey : IRegistrationKey
	{
		internal Type InstanceType;
		internal string Name;

		public NamedRegistrationKey(string name, Type type)
		{
			Name = name ?? String.Empty;
			InstanceType = type;
		}

		public Type GetInstanceType() { return InstanceType; }

		// comparison methods
		public override bool Equals(object obj)
		{
			var r = obj as NamedRegistrationKey;
			return (r != null) &&
				Object.ReferenceEquals(InstanceType, r.InstanceType) &&
				String.Compare(Name, r.Name, true) == 0; // ignore case
		}

		public override int GetHashCode()
		{
			return InstanceType.GetHashCode();
		}
	}

	internal class Registration : IRegistration
	{
		internal ILifetimeManager LifetimeManager;
		internal Func<IDependencyResolver, object> Factory;
		private string _key;
		private Type _type;

		public object Instance;
		IDependencyResolver Container;
		object _lock = new object();


		public Registration(IDependencyResolver container, string name, Type type, Func<IDependencyResolver, object> factory)
		{
			LifetimeManager = null;
			Container       = container;
			Factory         = factory;
			Name            = name;
			_type           = type;
			_key            = "[" + (name ?? "null") + "]:" + type.Name;
		}

		public string Key
		{
			get
			{
				return _key;
			}
		}

		public Type ResolvesTo
		{
			get
			{
				return _type;
			}
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
			return (LifetimeManager != null)
				? LifetimeManager.GetInstance(this)
				: Factory(Container);
		}

		public void InvalidateInstanceCache()
		{
			if (LifetimeManager != null)
				LifetimeManager.InvalidateInstanceCache(this);
			else
				Instance = null;
		}
	}
}
