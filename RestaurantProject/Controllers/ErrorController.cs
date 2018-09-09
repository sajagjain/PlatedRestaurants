using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantProject.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(Exception e)
        {

            return View(e);
        }
    }
}