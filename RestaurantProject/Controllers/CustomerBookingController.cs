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
    [NoDirectAccess]
    public class CustomerBookingController : Controller
    {
        DatabaseBL restaurantBAL = new DatabaseBL();
        // GET: Booking
        public ActionResult GetAllBookings(int custId)
        {
            try { 
                CustomerBookingActivePendingModel bookingModel = new CustomerBookingActivePendingModel();
                bookingModel.Active = restaurantBAL.GetActiveBookingsOfACustomer(custId);
                bookingModel.Pending = restaurantBAL.GetPendingBookingsOfACustomer(custId);
                bookingModel.Complete = restaurantBAL.GetCompletedBookingsOfACustomer(custId);
                bookingModel.Cancelled = restaurantBAL.GetCancelledBookingsOfACustomer(custId);
                bookingModel.OrderPlaced = restaurantBAL.GetOrderPlacedBookingsOfACustomer(custId);
                return View(bookingModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        public ActionResult CreateMyBooking()
        {
            try {
                    int resId = (int)Session["resIdForBooking"];
                    Customer customer = restaurantBAL.FindCustomer((int)Session["userId"]);
                    if (customer != null)
                    {
                        Booking booking = new Booking();
                        booking.Customer_Id = customer.Customer_Id;
                        booking.Customer_Name = customer.Name;
                        booking.Contact_No = customer.MobileNo;
                        booking.res_id = resId;
                        booking.Time_Arrival = new DateTime();
                        booking.Time_Departure = new DateTime();
                        return View(booking);
                    }
                    else
                    {
                    
                        //ViewBag["ErrorMessageCreateBookingGet"] = "Some Error Occoured. Try Again!";
                        return RedirectToAction("CreateMyBooking", new { custId = Session["userId"], resId = resId });
                    }

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }

        [HttpPost]
        public ActionResult CreateMyBooking(Booking booking)
        {
            try { 
            if (ModelState.IsValid)
            {
                if (booking != null)
                {

                    booking.Order_Id = null;
                    booking.Table_No = -1;
                    booking.Booking_Time_And_Date = DateTime.Now;
                    booking.Booking_Status = "Pending";
                    booking.res_id = (int)Session["resIdForBooking"];
                    booking.Customer_Id =(int)Session["userId"];
                    restaurantBAL.CreateBookingEntry(booking);

                        //return View(booking);
                        return RedirectToAction("GetAllBookings", new { custId = Session["userId"] });

                }
                else
                {
                    //ViewBag["ErrorMessageCreateMyBookingPost"] = "Some Error Occoured. Try Again!";
                    return RedirectToAction("GetAllBookings", new { custId = Session["userId"] });
                }
            }
            else
            {
                    return View(booking);
                    //return RedirectToAction("CreateMyBooking", new { custId = Session["userId"], resId = booking.res_id });
             }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }

        public ActionResult CancelBooking(int bid)
        {
            try { 
                Booking booking = restaurantBAL.FindBooking(bid);
                booking.Booking_Status = "Cancelled";
                int flag = restaurantBAL.EditBooking(booking);
                if (flag == 1)
                {
                    //Remove Static Value
                    return RedirectToAction("GetAllBookings", new { custId = Session["userId"] });
                }
                else
                {
                    // ViewBag["CancelBooking"] = "Error Occoured";
                    return RedirectToAction("GetAllBookings", new { custId = Session["userId"] });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }



        public ActionResult DeleteBooking(int bid)
        {
            try { 
                Booking booking = restaurantBAL.FindBooking(bid);
                if (booking != null)
                {

                    if (booking.Booking_Status == "Complete" || booking.Booking_Status == "Cancelled")
                    {
                        restaurantBAL.DeleteBooking(bid);
                        //Remove Static Entries
                        return RedirectToAction("GetAllBookings", new { custId = Session["userId"] });
                    }
                    else
                    {
                        // ViewBag["FromCustomerBookingDelete"] = "Please Try Deleting After Sometime";
                        return RedirectToAction("GetAllBookings", new { custId = Session["userId"] });
                    }
                }
                else
                {
                    ViewBag["FromCustomerBookingDelete"] = "Booking Yet In Progress";
                    return RedirectToAction("GetAllBookings", new { custId = Session["userId"] });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
    }
}