using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantDAL;
using RestaurantBAL;
using Newtonsoft.Json;
using System.Web.Routing;

namespace RestaurantProject.Controllers
{
    [HandleError]
    public class AdminController : Controller
    {
        DatabaseBL restaurantBAL = new DatabaseBL();
        // GET: Admin
        
        
        public ActionResult Index()
        {
            try {
                return View();
            } catch (Exception ex)
            {
                return RedirectToAction("Index","Error",ex);
            }
        }
        [NoDirectAccess]
        public ActionResult ViewRestaurants()
        {
            try { 
                List<Restaurant> restaurants = restaurantBAL.GetRestaurants();
                return View(restaurants);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [NoDirectAccess]
        public ActionResult DisableRestaurant(int resId)
        {
            try { 
                 restaurantBAL.DisableRestaurantAccount(resId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [NoDirectAccess]
        public ActionResult EnableRestaurant(int resId)
        {
            try
            {
                restaurantBAL.EnableRestaurantAccount(resId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [NoDirectAccess]
        public ActionResult ViewRestaurantMenu(int resId)
        {
            try { 
                List<Food> menu = restaurantBAL.GetFoodItemsOfARestaurant(resId);
                return View(menu);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [NoDirectAccess]
        public ActionResult SendNotification()
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
        [ActionName("SendNotification")]
        [NoDirectAccess]
        public ActionResult SendNotification([Bind(Include = "Notify_Id,Notify_From,Notify_From_Id,Notify_To,Notify_To_Id,Notify_Head,Notify_Description,Notify_Time")] Notification notification)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    restaurantBAL.CreateNotificationEntry(notification);
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }

        [NoDirectAccess]
        public ActionResult ViewEarningReportOfAdmin()
        {
            try { 
                //Below code can be used to include dynamic data in Chart. Check view page and uncomment the line "dataPoints: @Html.Raw(ViewBag.DataPoints)"
                ViewBag.DataPoints = JsonConvert.SerializeObject(restaurantBAL.GetEarningChartDataOfAdmin(), _jsonSetting);

                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [NoDirectAccess]
        public ActionResult ViewTransaction(int transId)
        {
            try { 
                TransactionTable transaction = restaurantBAL.FindTransactionsTable(transId);
                return View(transaction);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [NoDirectAccess]
        public ActionResult ViewTransactions()
        {
            try { 
                List<TransactionTable> transactions = restaurantBAL.GetTransactionsTable();
                return View(transactions);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [NoDirectAccess]
        public ActionResult ViewOffer() {
            try { 
                List<Offer> offers = restaurantBAL.GetOffers();
                return View(offers);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [NoDirectAccess]
        public ActionResult ViewOffersOfARestaurant(int resId)
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
        [NoDirectAccess]
        public ActionResult ViewBookings()
        {
            try { 
                List<Booking> bookings = restaurantBAL.GetBookings();
                return View(bookings);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [NoDirectAccess]
        public ActionResult ViewRestaurantDetails(int resId)
        {
            try { 
                Restaurant restaurant = restaurantBAL.FindRestaurant(resId);
                return View(restaurant);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [NoDirectAccess]
        public ActionResult MyEarnings()
        {
            try { 
                Wallet wallet = restaurantBAL.FindWallet(100);
                return View(wallet);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [NoDirectAccess]
        public ActionResult ViewEarningReportOfRestaurant(int resId)
        {
            try { 
                //Below code can be used to include dynamic data in Chart. Check view page and uncomment the line "dataPoints: @Html.Raw(ViewBag.DataPoints)"
                //ViewBag.DataPoints = JsonConvert.SerializeObject(restaurantBAL.GetEarningChartDataOfAdmin(), _jsonSetting);
                List<TransactionTable> earning = restaurantBAL.GetRestaurantTransactions(resId);
                return View(earning);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }

        [NoDirectAccess]
        public ActionResult SendEmail(int offId)
        {
            try
            {
                Offer offer = restaurantBAL.FindOffer(offId);
                Restaurant restaurant = restaurantBAL.FindRestaurant((int)offer.Res_Id);
                string resEmail = restaurant.Owner_Email;
                EmailModel restaurantEmailModel = new EmailModel();
                restaurantEmailModel.To = resEmail;
                restaurantEmailModel.From = "platedrestaurants@gmail.com";
                restaurantEmailModel.Subject = "Problem in \t" + " " + offer.Offer_name;
                restaurantEmailModel.Body = "Hello " + restaurant.Owner_Name + "\n\t You have been notified that there is a problem in " + offer.Offer_name + " having description " + offer.Offer_Desc + "\nYou can contact the restaurant on platedrestaurants@gmail.com" + "\n\nRegards\nPlated Restaurants\nBlock 4, DLF IT Park, SEZ Campus, 1/124, Shivaji Gardens, Mount \nPoonamalle High Road, Manapakkam, Chennai, Tamil Nadu \n600089";
                new EmailBL().SendMail(restaurantEmailModel);
                return RedirectToAction("ViewOffer", "Admin", null);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }

        [NoDirectAccess]
        public ActionResult SendEmailRestaurantOffer(int offId)
        {
            try
            {
                Offer offer = restaurantBAL.FindOffer(offId);
                Restaurant restaurant = restaurantBAL.FindRestaurant((int)offer.Res_Id);
                string resEmail = restaurant.Owner_Email;
                EmailModel restaurantEmailModel = new EmailModel();
                restaurantEmailModel.To = resEmail;
                restaurantEmailModel.From = "platedrestaurants@gmail.com";
                restaurantEmailModel.Subject = "Problem in \t" + " " + offer.Offer_name;
                restaurantEmailModel.Body = "Hello " + restaurant.Owner_Name + "\n\t You have been notified that the offer name is " + offer.Offer_name + " having description " + offer.Offer_Desc + "\nYou can contact the restaurant on platedrestaurants@gmail.com" + "\n\nRegards\nPlated Restaurants\nBlock 4, DLF IT Park, SEZ Campus, 1/124, Shivaji Gardens, Mount \nPoonamalle High Road, Manapakkam, Chennai, Tamil Nadu \n600089";
                new EmailBL().SendMail(restaurantEmailModel);
                return RedirectToAction("ViewOffersOfARestaurant", "Admin", new { resId = restaurant.Res_Id });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [NoDirectAccess]
        public ActionResult SendEmailRestaurantMenu(int foodId)
        {
            try
            {
                Food food = restaurantBAL.FindFoodItems(foodId);
                Restaurant restaurant = restaurantBAL.FindRestaurant((int)food.res_id);
                string resEmail = restaurant.Owner_Email;
                EmailModel restaurantEmailModel = new EmailModel();
                restaurantEmailModel.To = resEmail;
                restaurantEmailModel.From = "platedrestaurants@gmail.com";
                restaurantEmailModel.Subject = "Menu for\t" + " " + food.Food_Name;
                restaurantEmailModel.Body = "Hello " + restaurant.Owner_Name + "\n\t You have been notified that the menu for " + food.Food_Name + " having description" + "Food Price " + food.Food_Price + ",Food Type" + food.Food_Type + "\nYou can contact the restaurant on platedrestaurants@gmail.com" + "\n\nRegards\nPlated Restaurants\nBlock 4, DLF IT Park, SEZ Campus, 1/124, Shivaji Gardens, Mount \nPoonamalle High Road, Manapakkam, Chennai, Tamil Nadu \n600089";
                new EmailBL().SendMail(restaurantEmailModel);
                return RedirectToAction("ViewRestaurantMenu", "Admin", new { resId = restaurant.Res_Id });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }


        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
    }
}