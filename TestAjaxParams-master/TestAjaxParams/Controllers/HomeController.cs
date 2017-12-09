using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestAjaxParams.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult TestParams()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AjaxCall(int[] values)
        {
            ViewData["values"] = values;
            return PartialView("_AjaxCall");
        }

        //[HttpPost]
        //public ActionResult AjaxCall(string[] values)
        //{
        //    ViewData["values"] = values;
        //    return PartialView("_AjaxCall");
        //}
    }
}