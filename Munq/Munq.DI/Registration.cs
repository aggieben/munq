using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.DI
{
    public class Registration<TType> : IRegistration<TType> where TType:class
    {
        internal Func<Container, TType> Factory;
        internal TType Instance = null;
        internal ILifetimeManager<TType> LifetimeManager;
        internal readonly Guid ID;
        public static readonly ILifetimeManager<TType> AlwayNewLifeTimeManager = new LifetimeManagers.AlwaysNewLifetime<TType>();
        public static readonly ILifetimeManager<TType> ContainerLifeTimeManager = new LifetimeManagers.ContainerLifetime<TType>();

        internal Registration(Func<Container, TType> f)
        {
            Factory = f;
            LifetimeManager = AlwayNewLifeTimeManager;
            ID = Guid.NewGuid();
        }

        internal TType GetInstance(Container container)
        {
            return LifetimeManager.GetInstance(container, this);
        }

        #region IRegistration<TType> Members

        public IRegistration<TType> WithLifetimeManager(ILifetimeManager<TType> ltm)
        {
            LifetimeManager = ltm;
            return this;
        }

        #endregion
    }

    internal class RegistrationKey<TType> : IRegistrationKey where TType:class
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
        private string Name;

        public NamedRegistrationKey(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            var other = obj as NamedRegistrationKey<TType>;
            return (obj != null) && (Name == other.Name);
        }

        public override int GetHashCode()
        {
            return typeof(TType).GetHashCode();
        }
    }

}
