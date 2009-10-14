using System;

namespace Munq.DI
{
    public class Registration: IRegistration
    {
        internal ILifetimeManager			LifetimeManager;
        internal Type						InstanceType;
        internal string						Name;
        internal Func<Container, object>	Factory;
        internal object						Instance;

		internal Registration(string name, Type type, Func<Container, object> factory)
        {
            LifetimeManager = null;
            Name			= name;
            InstanceType	= type;
            Factory			= factory;
            Id				= Guid.NewGuid().ToString();
        }

		internal Registration(Type type, Func<Container, object> factory) : this(null, type, factory) { }

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

        // comparison methods
        public override bool Equals(object obj)
        {
            var r = obj as Registration;
            return (r != null) &&
                (InstanceType== r.InstanceType) &&
                (Name == r.Name);
				//((Name == null && r.Name == null) || (Name != null && Name.Equals(r.Name)));
        }

        public override int GetHashCode()
        {
            var hc = InstanceType.GetHashCode();
            if(Name != null)
                hc ^= Name.GetHashCode();

            return hc;
        }
   }
}
