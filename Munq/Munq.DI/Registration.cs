using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.DI
{
    public class Registration<TType> : IRegistration where TType:class
    {
        internal Func<Container, TType> Factory;
        internal ILifetimeManager LifetimeManager;
        private readonly string _ID;
        public static readonly ILifetimeManager AlwayNewLifeTimeManager = new LifetimeManagers.AlwaysNewLifetime();
        public static readonly ILifetimeManager ContainerLifeTimeManager = new LifetimeManagers.ContainerLifetime();

        internal Registration(Func<Container, TType> f)
        {
            Factory = f;
            LifetimeManager = AlwayNewLifeTimeManager;
            _ID = Guid.NewGuid().ToString();
        }

        public string ID { get { return _ID; } }

        public object Instance { get; set; }

        internal TType GetInstance(Container container)
        {
            return (TType)LifetimeManager.GetInstance(container, this);
        }

        #region IRegistration Members

        public IRegistration WithLifetimeManager(ILifetimeManager ltm)
        {
            if (ltm == null)
                throw new ArgumentNullException();

            LifetimeManager = ltm;
            return this;
        }


        public object CreateInstance(Container container)
        {
            return Factory(container);
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
