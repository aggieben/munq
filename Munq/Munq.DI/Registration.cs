using System;

namespace Munq.DI
{
    public class Registration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ILifetimeManager AlwaysNewLifetimeManager = new LifetimeManagers.AlwaysNewLifetime();
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ILifetimeManager ContainerLifetimeManager = new LifetimeManagers.ContainerLifetime();

        internal ILifetimeManager LifetimeManager;

        private readonly string ID;

        internal Registration()
        {
           // LifetimeManager = null;
            this.ID = Guid.NewGuid().ToString();
        }

        public string Id 
        { 
            get { return this.ID; } 
        }

        public object Instance { get; set; }
   }

    public class Registration<TType> : Registration, IRegistration where TType : class
    {
        internal Func<Container, TType> Factory;

        internal Registration(Func<Container, TType> func)
        {
            this.Factory = func;
        }

        public IRegistration WithLifetimeManager(ILifetimeManager manager)
        {
            this.LifetimeManager = manager;
            return this;
        }

        public object CreateInstance(Container container)
        {
            return this.Factory(container);
        }
         
        internal TType GetInstance(Container container)
        {
            if (this.LifetimeManager != null)
            {
                return (TType)this.LifetimeManager.GetInstance(container, this);
            }
            else
            {
                return this.Factory(container);
            }
        }
    }

   internal class RegistrationKey<TType> : IRegistrationKey where TType : class
    {
        public override bool Equals(object obj)
        {
            return obj is RegistrationKey<TType>;
        }

        public override int GetHashCode()
        {
            return typeof(TType).GetHashCode();
        }
    }

    internal class NamedRegistrationKey<TType> : IRegistrationKey where TType : class
    {
        private string name;

        public NamedRegistrationKey(string name)
        {
            this.name = name;
        }

        public override bool Equals(object obj)
        {
            var other = obj as NamedRegistrationKey<TType>;
            return (other != null) && (this.name == other.name);
        }

        public override int GetHashCode()
        {
            return typeof(TType).GetHashCode();
        }
    }
}
