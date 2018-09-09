using RestaurantDAL;
using System.Collections.Generic;
using System.Web.Mvc;
using RestaurantBAL;
using System;

namespace RestaurantProject.Controllers
{
    [NoDirectAccess]
    public class RestaurantOwnerEventController : Controller
    {
        DatabaseBL restaurantBAL = new DatabaseBL();
        // GET: Event

        public ActionResult GetEvents()
        {
            try { 
                List<Event> events = restaurantBAL.GetEvents();
                return View(events);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }

        }
        //public ActionResult Edit(int id)
        //{
        //    if (Session["userType"] == null)
        //    {
        //        return RedirectToAction("Login", "Registration");
        //    }
        //    else if ("Admin" == Session["userType"].ToString())
        //    {
        //        return RedirectToAction("Index", "Admin");
        //    }
        //    else if (Session["userType"].ToString() == "Customer")
        //    {
        //        return RedirectToAction("Home", "CustomerMain");
        //    }
        //    else
        //    {
        //        Event obj = restaurantBAL.FindEvent(id);
        //        return View(obj);
        //    }
        //}
        //[HttpPost]
        //[ActionName("Edit")]
        //public ActionResult EditEvent(Event obj)
        //{
        //    if (Session["userType"] == null)
        //    {
        //        return RedirectToAction("Login", "Registration");
        //    }
        //    else if ("Admin" == Session["userType"].ToString())
        //    {
        //        return RedirectToAction("Index", "Admin");
        //    }
        //    else if (Session["userType"].ToString() == "Customer")
        //    {
        //        return RedirectToAction("Home", "CustomerMain");
        //    }
        //    else
        //    {
        //        restaurantBAL.EditEvent(obj);
        //        return RedirectToAction("GetEvents");
        //    }
        //}

        public ActionResult Delete(int id)
        {
            try { 
                restaurantBAL.DeleteEvent(id);
                return RedirectToAction("ShowEvents", "RestaurantMain");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
    }
}