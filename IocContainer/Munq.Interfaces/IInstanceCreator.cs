using System;
namespace Munq
{
    public interface IInstanceCreator
    {
        string Key { get; }
        object CreateInstance(ContainerCaching containerCache);
    }
    
public enum ContainerCaching
    {
        InstanceCachedInContainer,
        InstanceNotCachedInContainer
    }}
