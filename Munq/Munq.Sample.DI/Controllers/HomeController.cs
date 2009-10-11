using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Hosting;

namespace Munq.Sample.DI.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
		class Test{
		public string Name { get; set;}
		}
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

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

            List<string> themes = new List<string>();

            foreach (VirtualDirectory vdir in themDir.Directories)
            {
                themes.Add(vdir.Name);
            }
            var sv =ControllerContext.HttpContext.Profile.GetPropertyValue("Theme");
            var sl = new SelectList(themes,sv);
            ViewData["theme"] = sl;

            return View();
        }

        public ActionResult SetTheme(string theme)
        {
            if(!String.IsNullOrEmpty(theme))
                ControllerContext.HttpContext.Profile.SetPropertyValue("Theme", theme);

            return RedirectToAction("Theme");
        }
    }
}
