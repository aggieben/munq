using System;

namespace Munq
{
    public interface IRegistration
    {
        string Name         { get; }
        string Key          { get; }
        Type   ResolvesTo   { get; }
        IRegistration WithLifetimeManager(ILifetimeManager manager);
        void InvalidateInstanceCache();
    }
}
