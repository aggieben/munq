using System;
namespace Munq
{
    public enum ContainerCaching
    {
        InstanceCachedInContainer,
        InstanceNotCachedInContainer
    }

    public interface IInstanceCreator
    {
        string Key { get; }
        object CreateInstance(ContainerCaching containerCache);
    }
}
