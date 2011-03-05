namespace Munq
{
    public interface ILifetimeManager
    {
        object GetInstance(IRegistration creator);
        void   InvalidateInstanceCache(IRegistration registration);
    }
}
