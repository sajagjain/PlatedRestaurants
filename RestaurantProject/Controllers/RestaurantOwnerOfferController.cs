using RestaurantBAL;
using RestaurantDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantProject.Controllers
{
    [NoDirectAccess]
    public class RestaurantOwnerOfferController : Controller
    {
        DatabaseBL restaurantBAL = new DatabaseBL();
        // GET: Offer

        public ActionResult GetOffers(int resId)
        {
            try {    
                List<Offer> offers = restaurantBAL.GetOffersOfARestaurant(resId);
                return View(offers);
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
        //        Offer obj = restaurantBAL.FindOffer(id);
        //        return View(obj);
        //    }
        //}
        //[HttpPost]
        //[ActionName("Edit")]
        //public ActionResult EditOffer(Offer obj)
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
        //        restaurantBAL.EditOffer(obj);
        //        return RedirectToAction("GetOffers");
        //    }
        //}

        public ActionResult Delete(int id)
        {
            try { 
                restaurantBAL.DeleteOffer(id);
                return RedirectToAction("ShowOffers", "RestaurantMain");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
    }
}