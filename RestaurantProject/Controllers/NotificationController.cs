using RestaurantBAL;
using RestaurantDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantProject.Controllers
{
    [NoDirectAccess]
    public class NotificationController : Controller
    {
        DatabaseBL restaurantBAL = new DatabaseBL();
        // GET: Notification
        public ActionResult ViewCustomerNotification(int custId)
        {
            try
            {
                List<Notification> customerNotifications = restaurantBAL.GetNotificationsForCustomer(custId);
                return View(customerNotifications);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        public ActionResult ViewRestaurantNotification(int resId)
        {
            try { 
            List<Notification> restaurantNotifications = restaurantBAL.GetNotificationsForRestaurantOwnerAdmin(resId);
            return View(restaurantNotifications);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        
        public ActionResult Create()
        {
            try { 
            return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreateAdminNotification(Notification notification)
        {
            try { 
            if (ModelState.IsValid)
            {
                restaurantBAL.CreateNotificationEntry(notification);
                return RedirectToAction("ViewAdminNotification", new { id = notification.Notify_Id });
            }
            else
            {
                return View();
            }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
    }
}