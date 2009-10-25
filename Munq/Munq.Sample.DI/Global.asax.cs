using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Munq.Sample.DI.Controllers;
using Munq.DI;
using Munq.DI.LifetimeManagers;
using Munq.Sample.DI.Models;
using Munq.MVC.DI;

namespace Munq.Sample.DI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
		static public Container Container { get; private set;}
		
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
            var controllerFactory = new MunqDIControllerFactory();
            Container = controllerFactory.DIContainer;

            // register the Home Controller
            Container.Register<IController>("Home", c => new HomeController());

            // register the Account Controller and its dependencies
            Container.Register<IFormsAuthentication>(c => new FormsAuthenticationService());
            Container.Register<IMembershipService>(c => new AccountMembershipService());
            Container.Register<IController>("Account",
                c => new AccountController(c.Resolve<IFormsAuthentication>(), c.Resolve<IMembershipService>()));
                
            Container.Register<TestConfig>("Default", c=>new TestConfig { Name="This is a test"}).
            WithLifetimeManager(new CachedLifetime().ExpiresAfterNotAccessedFor(new TimeSpan(0,1,0)));

            // set the controller factory
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

        }
    }
}