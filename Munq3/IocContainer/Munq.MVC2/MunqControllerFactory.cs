// --------------------------------------------------------------------------------------------------
// © Copyright 2011 by Matthew Dennis.
// Released under the Microsoft Public License (Ms-PL) http://www.opensource.org/licenses/ms-pl.html
// --------------------------------------------------------------------------------------------------

using System;
using System.Web.Mvc;

using System.Web.Routing;

namespace Munq.MVC2
{
	/// <summary>
	/// Implements the MVC2 IControllerFactory interface using the Munq IOC Container.
	/// Requires a reference to Munq.MVC2.dll.
	/// </summary>
	public class MunqControllerFactory : IControllerFactory
	{
		/// <summary>
		/// Gets the IOC container used by the Controller Factory.
		/// </summary>
		public IocContainer IOC { get; private set; }

		/// <inheritdoc />
		public MunqControllerFactory(IocContainer container)
		{
			IOC = container;
		}

		#region IControllerFactory Members

		/// <summary>
		/// Uses the Munq IOC Container to create an instance of the requested controller.
		/// </summary>
		/// <param name="requestContext">The current Http Request Context.</param>
		/// <param name="controllerName">The name of the controller, without the 'Controller' suffix.</param>
		/// <returns>The controller instance if resolved, null otherwise.</returns>
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

		/// <summary>
		/// Disposes of the controller.
		/// </summary>
		/// <param name="controller">The controller to release.</param>
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
