using System;
using System.Collections.Generic;
namespace Munq
{
    public interface IIocContainer : IDisposable
    {
        //Register
        IRegistration Register(string name, Type type, Func<IIocContainer, object> func);
        IRegistration Register(Type type, Func<IIocContainer, object> func);
        IRegistration Register<TType>(Func<IIocContainer, TType> func) where TType : class;
        IRegistration Register<TType>(string name, Func<IIocContainer, TType> func) where TType : class;

        //Register Instance
        IRegistration RegisterInstance(string name, Type type, object instance);
        IRegistration RegisterInstance(Type type, object instance);
        IRegistration RegisterInstance<TType>(string name, TType instance) where TType : class;
        IRegistration RegisterInstance<TType>(TType instance) where TType : class;

        //Register Type Implementation
        IRegistration Register<TType, TImpl>() where TType: class where TImpl : class, TType;
        IRegistration Register<TType, TImpl>(string name) where TType: class where TImpl : class, TType;

        // Remove Registration
        void Remove(IRegistration ireg);

        //Resolve
        object Resolve(string name, Type type);
        object Resolve(Type type);
        TType Resolve<TType>() where TType : class;
        TType Resolve<TType>(string name) where TType : class;

        //Lazy Resolve
        Func<object> LazyResolve(string name, Type type);
        Func<object> LazyResolve(Type type);
        Func<TType> LazyResolve<TType>() where TType : class;
        Func<TType> LazyResolve<TType>(string name) where TType : class;

        //Get Registration
        IRegistration GetRegistration(string name, Type type);
        IRegistration GetRegistration(Type type);
        IRegistration GetRegistration<TType>() where TType : class;
        IRegistration GetRegistration<TType>(string name) where TType : class;

        //Get Registrations
        List<IRegistration> GetRegistrations(Type type);
        List<IRegistration> GetRegistrations<TType>() where TType : class;

        //Lifetime Manager
        ILifetimeManager LifeTimeManager { get; }
        IIocContainer UsesDefaultLifetimeManagerOf(ILifetimeManager lifetimeManager);
    }
}
