using RestaurantDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantProject.Models
{
    public class MyAccountModel
    {
        public Customer customer { get; set; }
        public List<Booking> activeBookings { get; set; }
        public List<Booking> orderPlacedBooking { get; set; }
        public List<TransactionTable> transactions { get; set; }
        public Wallet wallet { get; set; }
    }
}