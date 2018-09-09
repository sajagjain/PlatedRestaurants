using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantDAL;
using RestaurantProject.Models;

namespace RestaurantBAL
{
    public class DatabaseBL
    {
        
        Database restaurantDAL = new Database();
        

        //MethodForChart
        public List<DataPoint> GetEarningChartDataOfAdmin()
        {
            
            List<TransactionTable> adminTransactions= GetAdminTransactions();

            decimal amount = 0;
            string month = "";

            List<DataPoint> dataPoints = new List<DataPoint>();

            var result = adminTransactions.GroupBy(g => Convert.ToDateTime(g.Trans_Time).Month)
            .Select(s => new {
                month = s.Key,
                earning = s.Sum(t => t.Trans_Amount)
            }).ToList();

            //Alternate Method
           /* from r in adminTransactions
                         group adminTransactions by Convert.ToDateTime(r.Trans_Time).Month into earningchart
                         select new {
                             Month = earningchart.Key,
                             Earnings = earningchart.Sum(t => t.trans)
                         };
           */
            
            foreach (var item in result)
            {
                
                amount = Convert.ToDecimal(item.earning);
                month = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(item.month);

                dataPoints.Add(new DataPoint((double)amount, month));
            }

            return dataPoints;
        }
        //Method To Check If The Order Is Empty or not
        public bool IsOrderEmpty(int BID)
        { 
            try{
            List<Order> orders = restaurantDAL.GetOrders().ToList();
            var result = (from r in orders
                         where r.BID == BID
                         select r).ToList();
            if (result.Count>0)
            {
                return true;
            }
            return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        
        //Additional Logic Methods
        public int DisableRestaurantAccount(int resId)
        {
            try
            {
                Restaurant restaurant = restaurantDAL.FindRestaurant(resId);
                restaurant.Res_Status = "Inactive";
                int flag = restaurantDAL.EditRestaurant(restaurant);
                return flag;
            }catch(Exception e)
            {
                throw e;
            }
        }
        public int EnableRestaurantAccount(int resId)
        {
            try { 
            Restaurant restaurant = restaurantDAL.FindRestaurant(resId);
            restaurant.Res_Status = "Active";
            int flag = restaurantDAL.EditRestaurant(restaurant);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //Get Data Methods
        public List<Customer> GetCustomers()
        {
            try { 
            List<Customer> Customers = restaurantDAL.GetCustomers();
            return Customers;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string GetCustomersEmail(int custId)
        {
            try
            {
                List<Customer> Customers = restaurantDAL.GetCustomers();
                var result = (from r in Customers
                             where r.Customer_Id == custId
                             select r).ToList();
                return result.First().Email;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string GetRestaurantEmail(int resId)
        {
            try
            {
                List<Restaurant> Customers = restaurantDAL.GetRestaurants();
                var result = (from r in Customers
                              where r.Res_Id == resId
                              select r).ToList();
                return result.First().Owner_Email;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Restaurant> GetRestaurants()
        {
            try {
                
                
            List<Restaurant> Restaurants = restaurantDAL.GetRestaurants();
            return Restaurants;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Booking> GetBookings()
        {
            try { 
            List<Booking> Bookings = restaurantDAL.GetBookings();
            return Bookings;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Booking> GetBookingsOfARestaurant(int resId)
        {
            try { 
            List<Booking> Bookings = restaurantDAL.GetBookings();
            var result = (from r in Bookings
                         where r.res_id == resId
                         select r).ToList();
            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<TransactionTable> GetTransactionOfCustomer(int custId)
        {
            List<TransactionTable> transactionTable = restaurantDAL.GetTransactionsTable();
            var result = (from r in transactionTable
                         where r.Trans_From == "Customer"
                         select r).ToList();
            result = (from r in transactionTable
                     where r.Trans_From_Id == custId
                     select r).ToList();
            var result2 = from r in transactionTable
                          where r.Trans_To == "Customer"
                          select r;
            result2=(from r in result
                    where r.Trans_To_Id==custId
                    select r).ToList();
            transactionTable =(List<TransactionTable>)result;
            transactionTable.AddRange((List<TransactionTable>)result);
            return result;
        }
        public List<Booking> GetActiveBookingsOfARestaurant(int resId)
        {
            try { 
            List<Booking> Bookings = restaurantDAL.GetBookings();
            var result = (from r in Bookings
                          where r.res_id == resId
                          select r).ToList();
            result = (from r in result
                      where r.Booking_Status == "Active"
                      select r).ToList();
            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Booking> GetPendingBookingsOfARestaurant(int resId)
        {
            try { 
            List<Booking> Bookings = restaurantDAL.GetBookings();
            var result = (from r in Bookings
                          where r.res_id == resId
                          select r).ToList();
            result = (from r in result
                      where r.Booking_Status == "Pending"
                      select r).ToList();
            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Booking> GetCompletedBookingsOfARestaurant(int resId)
        {
            try { 
            List<Booking> Bookings = restaurantDAL.GetBookings();
            var result = (from r in Bookings
                          where r.res_id == resId
                          select r).ToList();
            result = (from r in result
                      where r.Booking_Status == "Completed"
                      select r).ToList();
            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Booking> GetOrderPlacedBookingsOfARestaurant(int resId)
        {
            try { 
            List<Booking> Bookings = restaurantDAL.GetBookings();
            var result = (from r in Bookings
                          where r.res_id == resId
                          select r).ToList();
            result = (from r in result
                      where r.Booking_Status == "Order Placed"
                      select r).ToList();
            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<EarningReportModel> GetEarningReportOfARestuarant(int resId)
        {
            try { 
            
                //return GetRestaurantTransactions(resId);
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<TransactionTable> GetRestaurantTransactions(int resId)
        {
            try { 
            List<TransactionTable> transactions = GetTransactionsTable();
            var result = from r in transactions
                         where (r.Trans_To == "Restaurant")
                         select r;
            return transactions;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<TransactionTable> GetAdminTransactions()
        {
            try { 
            List<TransactionTable> transactions = GetTransactionsTable();
            var result = from r in transactions
                         where (r.Trans_To == "Admin")
                         select r;
            return transactions;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Booking> GetActiveBookingsOfACustomer(int custId)
        {
            try { 
            List<Booking> Bookings = restaurantDAL.GetBookings();
            var result = (from r in Bookings
                          where r.Customer_Id == custId
                          select r).ToList();
            result = (from r in result
                      where r.Booking_Status == "Active"
                      select r).ToList();
            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Booking> GetPendingBookingsOfACustomer(int custId)
        {
            try { 
            List<Booking> Bookings = restaurantDAL.GetBookings();
            var result = (from r in Bookings
                          where r.Customer_Id == custId
                          select r).ToList();
            result = (from r in result
                      where r.Booking_Status == "Pending"
                      select r).ToList();
            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Booking> GetPendingBookingsOfARestuarant(int resId)
        {
            try { 
            List<Booking> Bookings = restaurantDAL.GetBookings();
            var result = (from r in Bookings
                          where r.res_id == resId
                          select r).ToList();
            result = (from r in result
                      where r.Booking_Status == "Pending"
                      select r).ToList();
            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Booking> GetCompletedBookingsOfACustomer(int custId)
        {
            try { 
            List<Booking> Bookings = restaurantDAL.GetBookings();
            string temp = "Completed";
            var result = (from r in Bookings
                          where r.Customer_Id == custId
                          select r).ToList();
            result = (from r in result
                      where r.Booking_Status == temp.ToString()
                      select r).ToList();
            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Booking> GetCompletedBookingsOfACustomerInRestaurant(int custId,int resId)
        {
            try { 
            List<Booking> Bookings = restaurantDAL.GetBookings();
            string temp = "Completed";
            var result = (from r in Bookings
                          where r.Customer_Id == custId
                          select r).ToList();
            result = (from r in result
                      where r.Booking_Status == temp.ToString()
                      select r).ToList();
            result = (from r in result
                      where r.res_id == resId
                      select r
                    ).ToList();
            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Booking> GetCancelledBookingsOfACustomer(int custId)
        {
            try { 
            List<Booking> Bookings = restaurantDAL.GetBookings();
            var result = (from r in Bookings
                          where r.Customer_Id == custId
                          select r).ToList();
            result = (from r in result
                      where r.Booking_Status == "Cancelled"
                      select r).ToList();
            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Booking> GetOrderPlacedBookingsOfACustomer(int custId)
        {
            try { 
            List<Booking> Bookings = restaurantDAL.GetBookings();
            var result = (from r in Bookings
                          where r.Customer_Id == custId
                          select r).ToList();
            result = (from r in result
                      where r.Booking_Status == "Order Placed"
                      select r).ToList();
            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Offer> GetOffers()
        {
            try { 
            List<Offer> Offers = restaurantDAL.GetOffers();
            return Offers;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Offer> GetOffersOfARestaurant(int resId)
        {
            try { 
            List<Offer> Offers = restaurantDAL.GetOffers();
            var result = (from r in Offers
                          where r.Res_Id == resId
                          select r).ToList();
            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Event> GetEvents()
        {
            try { 
            List<Event> Events = restaurantDAL.GetEvents();
            return Events;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Event> GetEventsOfARestaurant(int resId)
        {
            try { 
            List<Event> Events = restaurantDAL.GetEvents();
            var result = (from r in Events
                          where r.Res_Id == resId
                          select r).ToList();
            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Food> GetFoodItems()
        {
            try { 
            List<Food> Foods = restaurantDAL.GetFoodItems();
            return Foods;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Food> GetFoodItemsOfARestaurant(int resId)
        {
            try { 
            List<Food> FoodItems = restaurantDAL.GetFoodItems();
            var result = (from r in FoodItems
                          where r.res_id == resId
                          select r).ToList();
            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Food> GetStarterFoodItemsOfARestaurant(int resId)
        {
            try { 
            List<Food> FoodItems = restaurantDAL.GetFoodItems();
            var result = (from r in FoodItems
                          where r.res_id == resId
                          select r).ToList();
            result = (from r in result
                          where r.Food_Type == "Starter"
                          select r).ToList();
            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Food> GetMainCourseFoodItemsOfARestaurant(int resId)
        {
            try { 
            List<Food> FoodItems = restaurantDAL.GetFoodItems();
            var result = (from r in FoodItems
                          where r.res_id == resId
                          select r).ToList();
            result = (from r in result
                      where r.Food_Type == "Main Course"
                      select r).ToList();
            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Food> GetDessertFoodItemsOfARestaurant(int resId)
        {
            try { 
            List<Food> FoodItems = restaurantDAL.GetFoodItems();
            var result = (from r in FoodItems
                          where r.res_id == resId
                          select r).ToList();
            result = (from r in result
                      where r.Food_Type == "Dessert"
                      select r).ToList();
            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<Order> GetOrders()
        {
            try { 
            List<Order> Orders = restaurantDAL.GetOrders();
            return Orders;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Order> GetOrdersOfABooking(int bookingId)
        {
            try { 
            List<Order> Orders = restaurantDAL.GetOrders();
            var result = (from r in Orders
                         where (r.BID == bookingId)
                         select r).ToList();

            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Order> GetOrdersOfARestaurant(int resId)
        {
            try { 
            List<Order> Orders = restaurantDAL.GetOrders();
            var result = (from r in Orders
                         where (r.Booking.res_id == resId)
                         select r).ToList();

            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Enquiry> GetEnquiries()
        {
            try { 
            List<Enquiry> Enquiries = restaurantDAL.GetEnquiries();
            return Enquiries;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Enquiry> GetEnquiriesForARestaurant(int resId)
        {
            try { 
            List<Enquiry> Enquiries = restaurantDAL.GetEnquiries();
            var result = (from r in Enquiries
                         where (r.Res_Id == resId)
                         select r).ToList();
            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Feedback> GetFeedbacks()
        {
            try { 
            List<Feedback> Feedbacks = restaurantDAL.GetFeedbacks();
            return Feedbacks;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Feedback> GetFeedbacksForARestaurant(int resId)
        {
            try { 
            List<Feedback> Feedbacks = restaurantDAL.GetFeedbacks();
            var result = (from r in Feedbacks
                          where (r.Res_Id == resId)
                          select r).ToList();
            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Wallet> GetWallets()
        {
            try { 
            List<Wallet> Wallets = restaurantDAL.GetWallets();
            return Wallets;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public List<Notification> GetNotifications()
        {
            try { 
            List<Notification> Notifications = restaurantDAL.GetNotifications();
            return Notifications;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Notification> GetNotificationsForCustomer(int custId)
        {
            try { 
            List<Notification> Notifications = restaurantDAL.GetNotifications();
            var result = (from r in Notifications
                          where (r.Notify_To_Id == custId)
                          select r).ToList();
            result = (from r in result
                      where r.Notify_To == "Customer"
                      select r).ToList();
            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Notification> GetNotificationsFromCustomer(int custId)
        {
            try { 
            List<Notification> Notifications = restaurantDAL.GetNotifications();
            var result = (from r in Notifications
                          where (r.Notify_From_Id == custId)
                          select r).ToList();
            result = (from r in result
                      where r.Notify_From == "Customer"
                      select r).ToList();
            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Notification> GetNotificationsForRestaurantOwnerAdmin(int adminId)
        {
            try { 
            List<Notification> Notifications = restaurantDAL.GetNotifications();
            var result = (from r in Notifications
                          where (r.Notify_To_Id == adminId)
                          select r).ToList();
            result = (from r in result
                      where r.Notify_To == "Restaurant"
                      select r).ToList();
            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Notification> GetNotificationsFromRestaurantOwnerAdmin(int adminId)
        {
            try { 
            List<Notification> Notifications = restaurantDAL.GetNotifications();
            var result = (from r in Notifications
                          where (r.Notify_From_Id == adminId)
                          select r).ToList();
            result = (from r in result
                      where r.Notify_From == "Restaurant"
                      select r).ToList();
            return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<TransactionTable> GetTransactionsTable()
        {
            try { 
            List<TransactionTable> Transactions = restaurantDAL.GetTransactionsTable();
            return Transactions;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Create Data Methods
        public int CreateCustomerEntry(Customer customer)
        {
            try { 
            int flag=restaurantDAL.CreateCustomerEntry(customer);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int CreateRestaurantEntry(Restaurant restaurant)
        {
            try { 
            int flag = restaurantDAL.CreateRestaurantEntry(restaurant);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int CreateBookingEntry(Booking booking)
        {
            try { 
            int flag = restaurantDAL.CreateBookingEntry(booking);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int CreateOfferEntry(Offer offer)
        {
            try { 
            int flag = restaurantDAL.CreateOfferEntry(offer);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int CreateEventEntry(Event events)
        {
            try { 
            int flag = restaurantDAL.CreateEventEntry(events);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int CreateFoodItemsEntry(Food food)
        {
            try { 
            int flag = restaurantDAL.CreateFoodItemsEntry(food);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int CreateOrdersEntry(Order order)
        {
            try { 
            int flag = restaurantDAL.CreateOrdersEntry(order);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int CreateEnquiryEntry(Enquiry enquiry)
        {
            try { 
            int flag = restaurantDAL.CreateEnquiryEntry(enquiry);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int CreateFeedbackEntry(Feedback feedback)
        {
            try { 
            int flag = restaurantDAL.CreateFeedbackEntry(feedback);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int CreateWalletEntry(Wallet wallet)
        {
            try { 
            int flag = restaurantDAL.CreateWalletEntry(wallet);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int CreateNotificationEntry(Notification notification)
        {
            try { 
            int flag = restaurantDAL.CreateNotificationEntry(notification);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int CreateTransactionsTableEntry(TransactionTable transaction)
        {
            try { 
            int flag = restaurantDAL.CreateTransactionsTableEntry(transaction);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Find Methods
        public Customer FindCustomer(int customerId)
        {
            try { 
            Customer customer = restaurantDAL.FindCustomer(customerId);
            return customer;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Restaurant FindRestaurant(int restaurantId)
        {
            try { 
            Restaurant restaurant = restaurantDAL.FindRestaurant(restaurantId);
            return restaurant;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Booking FindBooking(int bookingId)
        {
            try { 
            Booking booking = restaurantDAL.FindBooking(bookingId);
            return booking;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Offer FindOffer(int offerId)
        {
            try { 
            Offer offer = restaurantDAL.FindOffer(offerId);
            return offer;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Event FindEvent(int eventsId)
        {
            try { 
            Event events = restaurantDAL.FindEvent(eventsId);
            return events;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Food FindFoodItems(int foodId)
        {
            try { 
            Food foodItem = restaurantDAL.FindFoodItems(foodId);
            return foodItem;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Order FindOrders(int orderId)
        {
            try { 
            Order order = restaurantDAL.FindOrders(orderId);
            return order;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Enquiry FindEnquiry(int enquiryId)
        {
            try { 
            Enquiry enquiry = restaurantDAL.FindEnquiry(enquiryId);
            return enquiry;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Feedback FindFeedback(int feedbackId)
        {
            try { 
            Feedback feedback = restaurantDAL.FindFeedback(feedbackId);
            return feedback;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Wallet FindWallet(int walletId)
        {
            try { 
            Wallet wallet = restaurantDAL.FindWallet(walletId);
            return wallet;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Notification FindNotification(int notificationId)
        {
            try { 
            Notification notification = restaurantDAL.FindNotification(notificationId);
            return notification;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public TransactionTable FindTransactionsTable(int transactionId)
        {
            try { 
            TransactionTable transaction = restaurantDAL.FindTransactionsTable(transactionId);
            return transaction;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Delete Methods
        public int DeleteCustomer(int customerId)
        {
            try { 
            int flag = restaurantDAL.DeleteCustomer(customerId);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int DeleteRestaurant(int restaurantId)
        {
            try { 
            int flag = restaurantDAL.DeleteRestaurant(restaurantId);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int DeleteBooking(int bookingId)
        {
            try { 
            int flag = restaurantDAL.DeleteBooking(bookingId);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int DeleteOffer(int offerId)
        {
            try { 
            int flag = restaurantDAL.DeleteOffer(offerId);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int DeleteEvent(int eventsId)
        {
            try { 
            int flag = restaurantDAL.DeleteEvent(eventsId);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int DeleteFoodItem(int foodId)
        {
            try { 
            int flag = restaurantDAL.DeleteFoodItems(foodId);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int DeleteOrder(int orderId)
        {
            try { 
            int flag = restaurantDAL.DeleteOrders(orderId);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int DeleteEnquiry(int enquiryId)
        {
            try { 
            int flag = restaurantDAL.DeleteEnquiry(enquiryId);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int DeleteFeedback(int feedbackId)
        {
            try { 
            int flag = restaurantDAL.DeleteFeedback(feedbackId);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int DeleteWallet(int walletId)
        {
            try { 
            int flag = restaurantDAL.DeleteWallet(walletId);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int DeleteNotification(int notificationId)
        {
            try { 
            int flag = restaurantDAL.DeleteNotification(notificationId);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int DeleteTransactionsTable(int transactionId)
        {
            try { 
            int flag = restaurantDAL.DeleteTransactionsTable(transactionId);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Edit Methods
        public int EditCustomer(Customer customer)
        {
            try { 
            int flag = restaurantDAL.EditCustomer(customer);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int EditRestaurant(Restaurant restaurant)
        {
            try { 
            int flag = restaurantDAL.EditRestaurant(restaurant);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int EditBooking(Booking booking)
        {
            try { 
            int flag = restaurantDAL.EditBooking(booking);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int EditOffer(Offer offer)
        {
            try { 
            int flag = restaurantDAL.EditOffer(offer);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int EditEvent(Event events)
        {
            try { 
            int flag = restaurantDAL.EditEvent(events);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int EditFoodItems(Food food)
        {
            try { 
            int flag = restaurantDAL.EditFoodItems(food);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int EditOrders(Order order)
        {
            try { 
            int flag = restaurantDAL.EditOrders(order);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int EditEnquiry(Enquiry enquiry)
        {
            try { 
            int flag = restaurantDAL.EditEnquiry(enquiry);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int EditFeedback(Feedback feedback)
        {
            try { 
            int flag = restaurantDAL.EditFeedback(feedback);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int EditWallet(Wallet wallet)
        {
            try { 
            int flag = restaurantDAL.EditWallet(wallet);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int EditNotification(Notification notification)
        {
            try { 
            int flag = restaurantDAL.EditNotification(notification);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int EditTransactionsTable(TransactionTable transaction)
        {
            try { 
            int flag = restaurantDAL.EditTransactionsTable(transaction);
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
