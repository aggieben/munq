// --------------------------------------------------------------------------------------------------
// © Copyright 2011 by Matthew Dennis.
// Released under the Microsoft Public License (Ms-PL) http://www.opensource.org/licenses/ms-pl.html
// --------------------------------------------------------------------------------------------------

using System;
using System.Web.Mvc;

using System.Web.Routing;



namespace Munq.MVC
{
    public class MunqControllerFactory : IControllerFactory
    {
        public IocContainer IOC { get; private set; }

        public MunqControllerFactory(IocContainer container)
        {
            IOC = container;
        }

        #region IControllerFactory Members

        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            try
            {
                return IOC.Resolve<IController>(controllerName);
            }
            catch
            {
                return null;
            }
        }

        public void ReleaseController(IController controller)
        {
            var disposable = controller as IDisposable;

            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        #endregion
    }
}
