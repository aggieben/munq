using System;

namespace Munq.DI
{
    public class Registration: IRegistration
    {
        internal ILifetimeManager	LifetimeManager;
        internal Type				InstanceType;
        internal string				Name;
        internal object				Factory;

        private readonly string		ID;

        internal Registration(string name, Type type, object factory)
        {
            LifetimeManager = null;
            Name = name;
            InstanceType	= type;
            Factory			= factory;
            this.ID			= Guid.NewGuid().ToString();
        }

        internal Registration(Type type, object factory) : this(null, type, factory) {}

        public string Id { get { return this.ID; } }

        public object Instance { get; set; }

        public IRegistration WithLifetimeManager(ILifetimeManager manager)
        {
            this.LifetimeManager = manager;
            return this;
        }

        public object CreateInstance(Container container)
        {
            return ((Func<Container, object>)this.Factory)(container);
        }

        public object GetInstance(Container container)
        {
            return (this.LifetimeManager != null)
                ? this.LifetimeManager.GetInstance(container, this)
                : CreateInstance(container);
        }

        // comparison methods
        public override bool Equals(object obj)
        {
            var r = obj as Registration;
            return (r != null) &&
                InstanceType.Equals(r.InstanceType) &&
                ((Name == null && r.Name == null) || (Name != null && Name.Equals(r.Name)));
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
