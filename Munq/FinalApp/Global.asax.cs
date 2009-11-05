using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Routing;

using Munq.DI;
using Munq.MVC;

using FinalApp.Controllers;
using FinalApp.Interfaces;
using FinalApp.AccountMembership;
using FinalApp.Authentication;

namespace FinalApp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static Container IOC { get; private set; }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            InitializeIOC();
            RegisterRoutes(RouteTable.Routes);
        }

        private void InitializeIOC()
        {
            // Create the IOC container
            IOC = new Container();

            // Create the Default Factory
            var controllerFactory = new MunqControllerFactory(IOC);

            // set the controller factory
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            // Register the dependencies
            // Article3
            // The dependencies to the concrete implementation should be externalized
            new AccountMembershipRegistrar().Register(IOC);
            new AuthenticationRegistrar().Register(IOC);

            // Register the Controllers
            IOC.Register<IController>("Home", ioc => new HomeController());
            IOC.Register<IController>("Account",
                    ioc => new AccountController(ioc.Resolve<IFormsAuthentication>(),
                                                  ioc.Resolve<IMembershipService>())
            );


        }
    }
}