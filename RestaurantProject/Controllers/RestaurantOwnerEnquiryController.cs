using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantBAL;
using RestaurantDAL;

namespace RestaurantProject.Controllers
{
    [HandleError]
    [NoDirectAccess]
    public class RestaurantOwnerEnquiryController : Controller
    {
        DatabaseBL restaurantBAL = new DatabaseBL();
        // GET: Enquiry

        public ActionResult GetEnquiries()
        {
            try { 
                List<Enquiry> enquiries = restaurantBAL.GetEnquiriesForARestaurant((int)Session["userId"]);
                return View(enquiries);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        public ActionResult Delete(int id)
        {
            try { 
                restaurantBAL.DeleteEnquiry(id);
                return RedirectToAction("ShowEnquiries","RestaurantMain");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        
    }
}