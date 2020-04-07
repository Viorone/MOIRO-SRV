using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MOIRO_SRV.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                //var result = "Ваш логин: " + User.Identity.Name;
                ViewBag.Title = "Home Page";
                return View();
            }
            

            return RedirectToAction("../Account/Login");
        }
    }
}
