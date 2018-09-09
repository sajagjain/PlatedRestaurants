using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantBAL;
using RestaurantDAL;
using System.Web.Routing;

namespace RestaurantProject.Controllers
{
    [NoDirectAccess]
    public class CustomerEnquiryController : Controller
    {

        // GET: Enquiry
        DatabaseBL restaurantBAL = new DatabaseBL();

        public ActionResult Create(int resId)
        {
            try { 
                Enquiry enquiry = new Enquiry();
                enquiry.Res_Id = resId;
                return View(enquiry);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create(Enquiry enquiry, int resId)
        {
            try { 
                if (ModelState.IsValid)
                {
                    enquiry.Res_Id = resId;
                    int flag = restaurantBAL.CreateEnquiryEntry(enquiry);
                    if (flag == 1)
                    {
                        Response.Write("<div class=\"well\">We Have Sent The Feedback To Restaurant</div>");
                        return RedirectToAction("ShowRestaurantDetails", "CustomerMain", new { resId = resId });
                    }
                    else
                    {
                        Response.Write("<div class=\"well\">Some Error Occoured While Sending Feedback Please Try Again</div>");
                        throw new Exception();
                    }

                }
                else
                {
                    enquiry.Res_Id = resId;
                    return View(enquiry);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
    }
}