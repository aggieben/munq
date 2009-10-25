using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Hosting;
using Munq.Sample.DI.Models;

namespace Munq.Sample.DI.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
			var s =  MvcApplication.Container.Resolve<TestConfig>("Default");
            ViewData["Message"] = "Welcome to ASP.NET MVC!" + s.Name;
            s.Name += "*";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        
        public ActionResult Theme()
        {
            VirtualPathProvider vpp = HostingEnvironment.VirtualPathProvider;
            VirtualDirectory themDir = vpp.GetDirectory("~/App_Themes");

            List<string> themes = themDir.Directories
										.OfType<VirtualDirectory>()
										.Select(vd=>vd.Name)
										.ToList();
										
            var sv =WebProfile.Current.theme;
            var sl = new SelectList(themes,sv);
            ViewData["theme"] = sl;

            return View();
        }

        public ActionResult SetTheme(string theme)
        {
            if(!String.IsNullOrEmpty(theme))
                WebProfile.Current.theme = theme;

            return RedirectToAction("Theme");
        }
    }
}
