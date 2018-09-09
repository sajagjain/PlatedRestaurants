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
    public class WalletController : Controller
    {
        DatabaseBL restaurantBAL = new DatabaseBL();
        TransactionLogic transactionLogic = new TransactionLogic();
        EmailBL emailBL = new EmailBL();

        public ActionResult ViewWallet(int walletId)
        {
            try { 
            Wallet wallet = restaurantBAL.FindWallet(walletId);
            return View(wallet);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        public ActionResult Pay(int resId, int BID, decimal totalAmount)
        {
            try {


                Customer customer = restaurantBAL.FindCustomer((int)Session["userId"]);
                Restaurant restaurant = restaurantBAL.FindRestaurant(resId);

                int transToId = resId;
                int walletId = (int)restaurant.wallet_id;

            Wallet walletRes = restaurantBAL.FindWallet(walletId);
            Wallet walletCust = restaurantBAL.FindWallet((int)customer.wallet_id);
            Wallet walletAdmin = restaurantBAL.FindWallet(100);

            walletCust.Wallet_Amount -= totalAmount;
            if (walletCust.Wallet_Amount > 0)
            {
                walletRes.Wallet_Amount += totalAmount;
                walletAdmin.Wallet_Amount += ((totalAmount * (decimal)5) / 100);
                walletRes.Wallet_Amount-= ((totalAmount * (decimal)5) / 100);
                Booking booking = restaurantBAL.FindBooking(BID);
                booking.Booking_Status = "Order Placed";
                int flag = restaurantBAL.EditBooking(booking);
                if (flag > 1)
                {

                    string resEmail = restaurantBAL.GetRestaurantEmail(resId);
                    string custEmail = customer.Email;
                    EmailModel custEmailModel = new EmailModel();
                        custEmailModel.To = custEmail;
                        custEmailModel.From = "platedrestaurants@gmail.com";
                        custEmailModel.Subject = "Booking is Confirmed and Order Has Been Successfully Placed";
                        custEmailModel.Body = "Hello " + customer.Name + "\n\t Your Booking Has Been Placed In " + restaurant.Res_Name + " for "+booking.No_Of_People+" People From"+booking.Time_Arrival+" To "+booking.Time_Departure+" You can contact the restaurant owner on "+restaurant.Owner_Email+ "\n\nRegards\nPlated Restaurants\nBlock 4, DLF IT Park, SEZ Campus, 1 / 124, Shivaji Gardens, Mount \nPoonamalle High Road, Manapakkam, Chennai, Tamil Nadu \n600089";
                        //Sending Mail To Customer
                        emailBL.SendMail(custEmailModel);
                    EmailModel restaurantEmailModel = new EmailModel();
                        restaurantEmailModel.To = resEmail;
                        restaurantEmailModel.From = "platedrestaurants@gmail.com";
                        restaurantEmailModel.Subject = "Order Placed On The Approved Booking";
                        restaurantEmailModel.Body = "Hello " + restaurant.Owner_Name + "\n\t Booking Has Been Placed In Your Restaurant By " + booking.Customer_Name + " For " + booking.No_Of_People + " People From" + booking.Time_Arrival + " To " + booking.Time_Departure+" \nYou can contact the customer on "+booking.Contact_No+ "\n\nRegards\nPlated Restaurants\nBlock 4, DLF IT Park, SEZ Campus, 1/124, Shivaji Gardens, Mount \nPoonamalle High Road, Manapakkam, Chennai, Tamil Nadu \n600089";
                        emailBL.SendMail(restaurantEmailModel);
                        //Sending Mail To Restaurant
                        restaurantBAL.CreateTransactionsTableEntry(new TransactionTable()
                    {
                        Trans_From = "Customer",
                        Trans_From_Id = 101,
                        Trans_To = "Restaurant",
                        Trans_To_Id = resId,
                        Trans_Amount = totalAmount,
                        Trans_Time = DateTime.Now,
                        Trans_Type = "Pay"
                    });
                    restaurantBAL.CreateTransactionsTableEntry(new TransactionTable()
                    {
                        Trans_From = "Restaurant",
                        Trans_From_Id = 101,
                        Trans_To = "Admin",
                        Trans_To_Id = 100,
                        Trans_Amount = ((totalAmount * (decimal)5) / 100),
                        Trans_Time = DateTime.Now,
                        Trans_Type = "Commision"
                    });

                    return RedirectToAction("PaymentSuccess");
                }
                else
                    //PaymentFailedException
                    throw new System.Exception();
            }
            else
            {
                walletCust.Wallet_Amount += totalAmount;
                return RedirectToAction("WalletHasInSufficientBalance", "CustomerRestaurantOrder");
            }
            //Error Not Found Exception
            throw new Exception();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        

        public ActionResult Refund(PayRefundTransactionModelIntermediate refundMoney)
        {
            try { 
            //We have to add session parameters with current session user account type
            Wallet wallet = restaurantBAL.FindWallet(refundMoney.walletId);

            PayRefundTransactionModelMain mRefund = null;

            return View(mRefund);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }

        
        public ActionResult PaymentSuccess()
        {
            try { 
            return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }

    }
}