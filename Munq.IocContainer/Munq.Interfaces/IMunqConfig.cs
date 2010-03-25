

namespace Munq.Configuration
{
    public interface IMunqConfig
    {
        /// <summary>
        /// Classes that implement this interface are automatically called to
        /// register type factories in the Munq IOC container
        /// </summary>
        /// <param name="container">The Munq Container.</param>
        void RegisterIn(IIocContainer container);
    }
}
