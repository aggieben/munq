using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using Munq.DI;

namespace Munq.MVC.DI
{
    public class MunqDIControllerFactory: IControllerFactory
    {
        public Container DIContainer { get; private set; }

        public MunqDIControllerFactory()
        {
            DIContainer = new Container();
        }

        #region IControllerFactory Members

        public IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            try
            {
                return DIContainer.Resolve<IController>(controllerName);
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
