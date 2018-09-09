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
    public class CustomerFeedbackController : Controller
    {
        // GET: CustomerFeedback
        DatabaseBL restaurantBAL = new DatabaseBL();
        // GET: Feedback

        public ActionResult Create(int resId)
        {
            try {   
                Feedback feedback = new Feedback();
                feedback.Res_Id = resId;
                return View(feedback);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create(Feedback feedback,int resId)
        {
            try { 

            if (ModelState.IsValid)
                {
                    feedback.Res_Id = resId;
                    int flag = restaurantBAL.CreateFeedbackEntry(feedback);
                    if (flag == 1)
                    {
                        Response.Write("<div class=\"well\">We Have Sent The Feedback To Restaurant</div>");
                        return RedirectToAction("ShowRestaurantDetails", "CustomerMain", new { resId =resId });
                    }
                    else
                    {
                        Response.Write("<div class=\"well\">Some Error Occoured While Sending Feedback Please Try Again</div>");
                        return View();
                    }

                }
                else
                {
                    feedback.Res_Id = resId;
                    return View(feedback);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
    }
}