using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tourism.Models;

namespace tourism.Controllers
{
    public class HomeController : Controller
    {
        private Tourism_DB1Entities db = new Tourism_DB1Entities();

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
      
    }
}