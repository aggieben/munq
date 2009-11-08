using System;

namespace Munq.DI
{
    public class UnNamedRegistrationKey : IRegistrationKey
	{
        internal Type	InstanceType;
        
        internal UnNamedRegistrationKey(Type type)
        { 
            InstanceType	= type;
        }
       
        public Type GetInstanceType() { return InstanceType; }

        // comparison methods
        public override bool Equals(object obj)
        {
			var r = obj as UnNamedRegistrationKey;
			return (r != null) && (InstanceType == r.InstanceType);
        }
        
        public override int GetHashCode()
        {
			return InstanceType.GetHashCode();
        }
	}

    public class NamedRegistrationKey : IRegistrationKey
	{
        internal Type	InstanceType;
        internal string	Name;
        
        internal NamedRegistrationKey(string name, Type type)
        { 
            Name			= name;
            InstanceType	= type;
       }
       
       public Type GetInstanceType() { return InstanceType; }
       
        // comparison methods
        public override bool Equals(object obj)
        {
            var r = obj as NamedRegistrationKey;
            return (r != null) &&
                (InstanceType == r.InstanceType) &&
                (Name == r.Name);
        }

        public override int GetHashCode()
        {
			return InstanceType.GetHashCode();
        }
	}

    public class Registration: IRegistration
    {
        internal ILifetimeManager			LifetimeManager;
        internal Func<Container, object>	Factory;
        internal object						Instance;

		internal Registration(string name, Type type, Func<Container, object> factory)
        {
            LifetimeManager = null;
            Factory			= factory;
            Id				= Guid.NewGuid().ToString();
            Name            = name;
        }

        public string Id { get; private set; }
        
        public string Name { get; private set; }

        public IRegistration WithLifetimeManager(ILifetimeManager manager)
        {
            this.LifetimeManager = manager;
            return this;
        }

        internal object CreateInstance(Container container)
        {
            return this.Factory(container);
        }

        internal object GetInstance(Container container)
        {
            return (LifetimeManager != null)
                ? LifetimeManager.GetInstance(container, this)
                : CreateInstance(container);
        }

   }
}
