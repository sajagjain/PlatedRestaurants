using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantBAL;
using RestaurantDAL;

namespace RestaurantProject.Controllers
{
    [NoDirectAccess]
    public class RestaurantOwnerFeedbackController : Controller
    {
        DatabaseBL restaurantBAL = new DatabaseBL();
        // GET: Feedback

        public ActionResult GetFeedbacks()
        {
            try { 
                List<Feedback> feedbacks = restaurantBAL.GetFeedbacksForARestaurant((int)Session["userId"]);
                return View(feedbacks);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        public ActionResult Delete(int id)
        {
            try { 
                restaurantBAL.DeleteFeedback(id);
                return RedirectToAction("ShowFeedback", "RestaurantMain");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }


    }
}