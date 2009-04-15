using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Munq.Sample.DI.Controllers;

namespace Munq.Sample.DI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.IgnoreRoute("favicon.ico");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );

        }


        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
            InitializeContainer();
        }

        private void InitializeContainer()
        {
            // create the Munq.DI.ControllerFactory and get the container
            var controllerFactory = new Munq.MVC.DI.MunqDIControllerFactory();
            var container = controllerFactory.DIContainer;

            // register the Home Controller
            container.Register<IController>("Home", c => new HomeController());

            // register the Account Controller and its dependencies
            container.Register<IFormsAuthentication>(c => new FormsAuthenticationService());
            container.Register<IMembershipService>(c => new AccountMembershipService());
            container.Register<IController>("Account",
                c => new AccountController(c.Resolve<IFormsAuthentication>(), c.Resolve<IMembershipService>()));

            // set the controller factory
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

        }
    }
}