using System;

namespace Munq.DI
{
	public class RegistrationKey
	{
        internal Type	InstanceType;
        internal string	Name;
        
        internal RegistrationKey(string name, Type type)
        { 
            Name			= name;
            InstanceType	= type;
       }
        // comparison methods
        public override bool Equals(object obj)
        {
            var r = obj as RegistrationKey;
            return (r != null) &&
                (InstanceType == r.InstanceType) &&
                (Name == r.Name);
        }

        public override int GetHashCode()
        {
            var hc = InstanceType.GetHashCode();
            return hc;
        }
	}

    public class Registration: IRegistration
    {
        internal ILifetimeManager			LifetimeManager;
        internal Func<Container, object>	Factory;
        internal object						Instance;
        

		internal Registration(Type type, Func<Container, object> factory)
        {
            LifetimeManager = null;
            Factory			= factory;
            Id				= Guid.NewGuid().ToString();
        }

        public string Id { get; private set; }

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
