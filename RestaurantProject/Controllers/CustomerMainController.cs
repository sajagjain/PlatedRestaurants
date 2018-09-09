using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantBAL;
using RestaurantDAL;
using RestaurantProject.Models;

namespace RestaurantProject.Controllers
{
    
    public class CustomerMainController : Controller
    {
        DatabaseBL restaurantBAL = new DatabaseBL();

        [HandleError]
        public ActionResult Home()
        {
            try
            {
                CustomerHomeModel model = new CustomerHomeModel();
                model.restaurants = restaurantBAL.GetRestaurants();
                model.events = restaurantBAL.GetEvents();
                model.offers = restaurantBAL.GetOffers();
                model.bookings = restaurantBAL.GetOrderPlacedBookingsOfACustomer((int)Session["userId"]);
                model.bookingActive = restaurantBAL.GetActiveBookingsOfACustomer((int)Session["userId"]);
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        
        [HandleError]
        [NoDirectAccess]
        public ActionResult ShowRestaurantDetails(int resId)
        {
           
                try
                {
                    Restaurant restaurant = restaurantBAL.FindRestaurant(resId);
                    if (restaurant != null)
                    {
                        RestaurantDetailCustomerModel model = new RestaurantDetailCustomerModel();
                        model.Offer = restaurantBAL.GetOffersOfARestaurant(resId);
                        model.Events = restaurantBAL.GetEventsOfARestaurant(resId);
                        model.Food = restaurantBAL.GetFoodItemsOfARestaurant(resId);
                        model.Feedback = new Feedback();

                        model.PrevBookingInThisRestaurant = restaurantBAL
                            .GetCompletedBookingsOfACustomerInRestaurant((int)Session["userId"],resId);
                        model.Restaurant = restaurant;

                        return View(model);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception ex)
                {
                return RedirectToAction("Index","Error",ex);
                }
            
        }
        [HandleError]
        public ActionResult MyAccount()
        {
            try {
                MyAccountModel model = new MyAccountModel();
                model.customer = restaurantBAL.FindCustomer((int)Session["userId"]);
                model.activeBookings= restaurantBAL.GetActiveBookingsOfACustomer((int)Session["userId"]);
                model.orderPlacedBooking = restaurantBAL.GetOrderPlacedBookingsOfACustomer((int)Session["userId"]);
                model.transactions = restaurantBAL.GetTransactionOfCustomer((int)Session["userId"]);
                model.wallet = restaurantBAL.FindWallet((int)Session["walletId"]);
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }

        [HandleError]
        public ActionResult ShowOffers()
        {
            try { 
                List<Offer> list = restaurantBAL.GetOffers();
                return View(list);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }

        [HandleError]
        public ActionResult ShowEvents()
        {
            try { 
                List<Event> list = restaurantBAL.GetEvents();
                return View(list);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }

        
        public ActionResult CustomerNotification()
        {
            try
            {
                List<Notification> notifications = restaurantBAL.GetNotificationsForCustomer((int)Session["userId"]);
                return View(notifications);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }

        }
        [NoDirectAccess]
        public ActionResult DeleteCustomerNotification(int nid)
        {
            try
            {
                restaurantBAL.DeleteNotification(nid);
                return RedirectToAction("CustomerNotification");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }

        }

        
    }
}