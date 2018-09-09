using RestaurantBAL;
using RestaurantDAL;
using RestaurantProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantProject.Controllers
{
    [HandleError]
    public class RestaurantMainController : Controller
    {
        DatabaseBL restaurantBAL = new DatabaseBL();
        // GET: RestaurantMain
        public ActionResult Index()
        {
            try { 
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        public ActionResult ViewBookings()
        {
            try { 
                RestaurantBookingDetailsModel model = new RestaurantBookingDetailsModel();
                model.ActiveBookings = restaurantBAL.GetActiveBookingsOfARestaurant((int)Session["userId"]);
                model.CompletedBookings = restaurantBAL.GetCompletedBookingsOfARestaurant((int)Session["userId"]);
                model.OrderPlaced = restaurantBAL.GetOrderPlacedBookingsOfARestaurant((int)Session["userId"]);
                model.PendingBookings = restaurantBAL.GetPendingBookingsOfARestaurant((int)Session["userId"]);
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [NoDirectAccess]
        public ActionResult ApproveBooking(int bid)
        {
            try { 
            Booking booking = restaurantBAL.FindBooking(bid);
            booking.Booking_Status = "Active";
            int flag = restaurantBAL.EditBooking(booking);
            if (flag == 1)
                return RedirectToAction("ViewBookings");
            else
                throw new Exception();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [NoDirectAccess]
        public ActionResult CancelBooking(int bid)
        {
            try { 
            Booking booking = restaurantBAL.FindBooking(bid);
            booking.Booking_Status = "Cancelled";
            int flag = restaurantBAL.EditBooking(booking);
            if (flag == 1)
                return RedirectToAction("ViewBookings");
            else
                throw new Exception();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [NoDirectAccess]
        public ActionResult ViewRestaurantMenu()
        {
            try { 
                List<Food> food = restaurantBAL.GetFoodItemsOfARestaurant((int)Session["userId"]);
                return View(food);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [NoDirectAccess]
        public ActionResult ShowOffers()
        {
            try { 
                List<Offer> offers = restaurantBAL.GetOffersOfARestaurant((int)Session["userId"]);
                return View(offers);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [NoDirectAccess]
        public ActionResult ShowEvents()
        {
            try { 
                List<Event> events = restaurantBAL.GetEventsOfARestaurant((int)Session["userId"]);
                return View(events);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [NoDirectAccess]
        public ActionResult ShowEnquiries()
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
        [NoDirectAccess]
        public ActionResult ShowFeedback()
        {
            try { 
                List<Feedback> feedback = restaurantBAL.GetFeedbacksForARestaurant((int)Session["userId"]);
                return View(feedback);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }

        [NoDirectAccess]
        public ActionResult RestaurantNotification()
        {
            try
            {
                List<Notification> notifications = restaurantBAL.GetNotificationsForRestaurantOwnerAdmin((int)Session["userId"]);
                return View(notifications);
            
            } catch (Exception ex)
            {
                return RedirectToAction("Index","Error",ex);
            }

        }
        [NoDirectAccess]
        public ActionResult DeleteRestaurantNotification(int nid)
        {
            try { 
            restaurantBAL.DeleteNotification(nid);
            return RedirectToAction("RestaurantNotification");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }


        [NoDirectAccess]
        public ActionResult ViewOrderDetails(int bid)
        {

            try { 
                List<Order> order = restaurantBAL.GetOrdersOfABooking(bid);
                return View(order);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }

        }





        //Confirm Booking

        [NoDirectAccess]
        public ActionResult CompleteBooking(int BID)
        {
            try { 
            
                Booking booking = restaurantBAL.FindBooking(BID);
                booking.Booking_Status = "Completed";
                restaurantBAL.EditBooking(booking);
                return RedirectToAction("ViewBookings");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }


        //Cancel Order Booking


        [NoDirectAccess]
        public ActionResult CancelOrderBooking(int BID)
        {
            try { 
                Booking booking = restaurantBAL.FindBooking(BID);
                booking.Booking_Status = "Cancelled";
                restaurantBAL.EditBooking(booking);
                return RedirectToAction("ViewBookings");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }

        }
        [NoDirectAccess]
        public ActionResult CreateOffer(int resId)
        {
            try { 
            
                Offer offer = new Offer();
                offer.Res_Id = resId;
                return View(offer);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [HttpPost]
        [NoDirectAccess]
        public ActionResult CreateOffer(Offer offer, int resId)
        {
            try { 
                if (ModelState.IsValid)
                {
                    offer.Res_Id = resId;

                    int flag = restaurantBAL.CreateOfferEntry(offer);
                    if (flag == 1)
                    {
                        return RedirectToAction("ShowOffers", "RestaurantMain", new { resId = resId });
                    }
                    else
                    {
                        Response.Write("<div class=\"well\">Some Error Occoured While creating Offer, Please Try Again</div>");
                        throw new Exception();
                    }

                }
                else
                {
                    offer.Res_Id = resId;
                    return View(offer);
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [NoDirectAccess]
        public ActionResult CreateEvent(int resId)
        {
            try { 
                Event events = new Event();
                events.Res_Id = resId;
                return View(events);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [HttpPost]
        [NoDirectAccess]
        public ActionResult CreateEvent(Event events, int resId)
        {
            try { 
                if (ModelState.IsValid)
                {
                    events.Res_Id = resId;
                    events.Event_Image = "~/ImagesFolder/eventdefault.png";

                    int flag = restaurantBAL.CreateEventEntry(events);
                    if (flag == 1)
                    {
                        return RedirectToAction("ShowEvents", "RestaurantMain", new { resId = resId });
                    }
                    else
                    {
                        Response.Write("<div class=\"well\">Some Error Occoured While creating Event, Please Try Again</div>");
                        throw new Exception();
                    }

                }
                else
                {
                    events.Res_Id = resId;
                    return View(events);
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [NoDirectAccess]
        public ActionResult CreateMenu(int resId)
        {
            try { 
            
                Food food = new Food();
                food.res_id = resId;
                return View(food);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [HttpPost]
        [NoDirectAccess]
        public ActionResult CreateMenu(Food food, int resId)
        {
            try { 
                if (ModelState.IsValid)
                {
                    food.res_id = resId;
                    food.Food_Image = "~/ImagesFolder/fooddefault.jpg";

                    int flag = restaurantBAL.CreateFoodItemsEntry(food);
                    if (flag == 1)
                    {
                        return RedirectToAction("ViewRestaurantMenu", "RestaurantMain", new { resId = resId });
                    }
                    else
                    {
                        Response.Write("<div class=\"well\">Some Error Occoured While creating menu, Please Try Again</div>");
                        throw new Exception();
                    }

                }
                else
                {
                    food.res_id = resId;
                    return View(food);
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }




    }
}