
namespace Munq
{
    public interface ILifetimeManager
    {
        object GetInstance(IInstanceCreator creator);
        void InvalidateInstanceCache(IRegistration registration);
    }
}
