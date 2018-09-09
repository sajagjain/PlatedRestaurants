using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDAL
{
    public class Database
    {

        //restaurantEntities restaurantDB = new restaurantEntities();
        restaurantEntities restaurantDB = new restaurantEntities();

        //Get Data Methods

        public List<Customer> GetCustomers()
        {
            try
            {
                List<Customer> Customers = restaurantDB.Customers.ToList();
                return Customers;
            }catch(Exception e)
            {
                throw e;
            }

        }
        public List<Restaurant> GetRestaurants()
        {
            try { 
            List<Restaurant> Restaurants = restaurantDB.Restaurants.ToList();
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
            List<Booking> Bookings = restaurantDB.Bookings.ToList();
            if (Bookings == null)
            {
                Bookings = new List<Booking>();
            }
            return Bookings;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Offer> GetOffers()
        {
            try
            {
            List<Offer> Offers = restaurantDB.Offers.ToList();
            return Offers;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public List<Event> GetEvents()
        {
            try
            {
                List<Event> Events = restaurantDB.Events.ToList();
                return Events;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public List<Food> GetFoodItems()
        {
            try { 
            List<Food> Foods = restaurantDB.Foods.ToList();
            return Foods;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Order> GetOrders()
        {
            try { 
            List<Order> Orders = restaurantDB.Orders.ToList();
            return Orders;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Enquiry> GetEnquiries()
        {
            try { 
            List<Enquiry> Enquiries = restaurantDB.Enquiries.ToList();
            return Enquiries;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Feedback> GetFeedbacks()
        {
            try
            {
                List<Feedback> Feedbacks = restaurantDB.Feedbacks.ToList();
                return Feedbacks;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Wallet> GetWallets()
        {
            try { 
            List<Wallet> Wallets = restaurantDB.Wallets.ToList();
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
            List<Notification> Notifications = restaurantDB.Notifications.ToList();
            return Notifications;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<TransactionTable> GetTransactionsTable()
        {
            try { 
            List<TransactionTable> Transactions = restaurantDB.TransactionTables.ToList();
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
            restaurantDB.Customers.Add(customer);
            int flag=restaurantDB.SaveChanges();
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
            restaurantDB.Restaurants.Add(restaurant);
            int flag = restaurantDB.SaveChanges();
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
            restaurantDB.Bookings.Add(booking);
            int flag = restaurantDB.SaveChanges();
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
            restaurantDB.Offers.Add(offer);
            int flag = restaurantDB.SaveChanges();
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int CreateEventEntry(Event events)
        {
            try
            {
                restaurantDB.Events.Add(events);
                int flag = restaurantDB.SaveChanges();
                return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int CreateFoodItemsEntry(Food food)
        {
            try
            {
                restaurantDB.Foods.Add(food);
                int flag = restaurantDB.SaveChanges();
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
            restaurantDB.Orders.Add(order);
            int flag = restaurantDB.SaveChanges();
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
            restaurantDB.Enquiries.Add(enquiry);
            int flag = restaurantDB.SaveChanges();
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
            restaurantDB.Feedbacks.Add(feedback);
            int flag = restaurantDB.SaveChanges();
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
            restaurantDB.Wallets.Add(wallet);
            int flag = restaurantDB.SaveChanges();
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
            restaurantDB.Notifications.Add(notification);
            int flag = restaurantDB.SaveChanges();
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
            restaurantDB.TransactionTables.Add(transaction);
            int flag = restaurantDB.SaveChanges();
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //FindDataMethods
        public Customer FindCustomer(int customerId)
        {
            try
            {
                Customer customer = restaurantDB.Customers.Find(customerId);
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
            Restaurant restaurant=restaurantDB.Restaurants.Find(restaurantId);
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
            Booking booking=restaurantDB.Bookings.Find(bookingId);
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
            Offer offer=restaurantDB.Offers.Find(offerId);
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
            Event events=restaurantDB.Events.Find(eventsId);
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
            Food foodItem=restaurantDB.Foods.Find(foodId);
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
            Order order=restaurantDB.Orders.Find(orderId);
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
            Enquiry enquiry=restaurantDB.Enquiries.Find(enquiryId);
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
            Feedback feedback=restaurantDB.Feedbacks.Find(feedbackId);
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
            Wallet wallet=restaurantDB.Wallets.Find(walletId);
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
            Notification notification=restaurantDB.Notifications.Find(notificationId);
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
            TransactionTable transaction=restaurantDB.TransactionTables.Find(transactionId);
            return transaction;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //For Delete
        public int DeleteCustomer(int customerId)
        {
            try { 
            Customer customer = restaurantDB.Customers.Find(customerId);
            restaurantDB.Customers.Remove(customer);
            int flag = restaurantDB.SaveChanges();
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
            Restaurant restaurant = restaurantDB.Restaurants.Find(restaurantId);
            restaurantDB.Restaurants.Remove(restaurant);
            int flag = restaurantDB.SaveChanges();
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
            Booking booking = restaurantDB.Bookings.Find(bookingId);
            restaurantDB.Bookings.Remove(booking);
            int flag = restaurantDB.SaveChanges();
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
            Offer offer = restaurantDB.Offers.Find(offerId);
            restaurantDB.Offers.Remove(offer);
            int flag = restaurantDB.SaveChanges();
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
            Event events = restaurantDB.Events.Find(eventsId);
            restaurantDB.Events.Remove(events);
            int flag = restaurantDB.SaveChanges();
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int DeleteFoodItems(int foodId)
        {
            try { 
            Food foodItem = restaurantDB.Foods.Find(foodId);
            restaurantDB.Foods.Remove(foodItem);
            int flag = restaurantDB.SaveChanges();
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int DeleteOrders(int orderId)
        {
            try { 
            Order order = restaurantDB.Orders.Find(orderId);
            restaurantDB.Orders.Remove(order);
            int flag = restaurantDB.SaveChanges();
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
            Enquiry enquiry = restaurantDB.Enquiries.Find(enquiryId);
            restaurantDB.Enquiries.Remove(enquiry);
            int flag = restaurantDB.SaveChanges();
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
            Feedback feedback = restaurantDB.Feedbacks.Find(feedbackId);
            restaurantDB.Feedbacks.Remove(feedback);
            int flag = restaurantDB.SaveChanges();
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
            Wallet wallet = restaurantDB.Wallets.Find(walletId);
            restaurantDB.Wallets.Remove(wallet);
            int flag = restaurantDB.SaveChanges();
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
            Notification notification = restaurantDB.Notifications.Find(notificationId);
            restaurantDB.Notifications.Remove(notification);
            int flag = restaurantDB.SaveChanges();
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
            TransactionTable transaction = restaurantDB.TransactionTables.Find(transactionId);
            restaurantDB.TransactionTables.Remove(transaction);
            int flag = restaurantDB.SaveChanges();
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //For Edit
        public int EditCustomer(Customer customer)
        {
            try { 
            restaurantDB.Entry(customer).State = EntityState.Modified;
            int flag = restaurantDB.SaveChanges();
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
            restaurantDB.Entry(restaurant).State = EntityState.Modified;
            int flag = restaurantDB.SaveChanges();
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
            restaurantDB.Entry(booking).State = EntityState.Modified;
            int flag = restaurantDB.SaveChanges();
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
            restaurantDB.Entry(offer).State = EntityState.Modified;
            int flag = restaurantDB.SaveChanges();
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
            restaurantDB.Entry(events).State = EntityState.Modified;
            int flag = restaurantDB.SaveChanges();
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
            restaurantDB.Entry(food).State = EntityState.Modified;
            int flag = restaurantDB.SaveChanges();
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
            restaurantDB.Entry(order).State = EntityState.Modified;
            int flag = restaurantDB.SaveChanges();
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
            restaurantDB.Entry(enquiry).State = EntityState.Modified;
            int flag = restaurantDB.SaveChanges();
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int EditFeedback(Feedback feedback)
        {
            try
            {
                restaurantDB.Entry(feedback).State = EntityState.Modified;
                int flag = restaurantDB.SaveChanges();
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
            restaurantDB.Entry(wallet).State = EntityState.Modified;
            int flag = restaurantDB.SaveChanges();
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
            restaurantDB.Entry(notification).State = EntityState.Modified;
            int flag = restaurantDB.SaveChanges();
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
            restaurantDB.Entry(transaction).State = EntityState.Modified;
            int flag = restaurantDB.SaveChanges();
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
