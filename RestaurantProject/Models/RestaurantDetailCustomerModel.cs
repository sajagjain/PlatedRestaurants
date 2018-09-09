using RestaurantDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantProject.Models
{
    public class RestaurantDetailCustomerModel
    {
        public Restaurant Restaurant { get; set; }
        public List<Event> Events { get; set; }
        public List<Food> Food { get; set; }
        public List<Offer> Offer { get; set; }
        public List<Booking> PrevBookingInThisRestaurant { get; set; }
        public Feedback Feedback { get; set; }
        
    }
}