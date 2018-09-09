using RestaurantBAL;
using RestaurantDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestaurantProject.Controllers
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NoDirectAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.UrlReferrer == null ||
                        filterContext.HttpContext.Request.Url.Host != filterContext.HttpContext.Request.UrlReferrer.Host)
            {
                filterContext.Result = new RedirectToRouteResult(new
                               RouteValueDictionary(new { controller = "Home", action = "Index", area = "" }));
            }
        }
    }



    [HandleError]
    
    public class HomeController : Controller
    {
        DatabaseBL restaurantBAL = new DatabaseBL();
        public ActionResult Index()
        {
            try { 
            if (Session["userType"] == null)
            {
                List<Restaurant> restaurants = restaurantBAL.GetRestaurants();
                return View(restaurants);
            }
            else if ("Restaurant" == Session["userType"].ToString())
            {
                return RedirectToAction("Index", "RestaurantMain");
            }
            else if (Session["userType"].ToString() == "Customer")
            {
                return RedirectToAction("Home", "CustomerMain");
            }
            else if (Session["userType"].ToString() == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                throw new Exception();
            }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }

        public ActionResult About()
        {
            try { 
           
                ViewBag.Message = "Your application description page.";

                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }

        }

        public ActionResult Contact()
        {
            try { 
               ViewBag.Message = "Your contact page.";

                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        public ActionResult UserAgreement()
        {
            try { 
            if (Session["userType"] == null)
            {
                return View();
            }
            else if ("Restaurant" == Session["userType"].ToString())
            {
                return RedirectToAction("Index", "RestaurantMain");
            }
            else if (Session["userType"].ToString() == "Customer")
            {
                return RedirectToAction("Home", "CustomerMain");
            }
            else if (Session["userType"].ToString() == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                throw new Exception();
            }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        public ActionResult PrivacyPolicy()
        {
            try { 
            if (Session["userType"] == null)
            {
                return View();
            }
            else if ("Restaurant" == Session["userType"].ToString())
            {
                return RedirectToAction("Index", "RestaurantMain");
            }
            else if (Session["userType"].ToString() == "Customer")
            {
                return RedirectToAction("Home", "CustomerMain");
            }
            else if (Session["userType"].ToString() == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                throw new Exception();
            }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }

        

    }
}