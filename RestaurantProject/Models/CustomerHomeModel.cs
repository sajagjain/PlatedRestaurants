using RestaurantDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantProject.Models
{
    public class CustomerHomeModel
    {
        public List<Restaurant> restaurants { get; set; }
        public List<Offer> offers { get; set; }
        public List<Event> events{ get;set; }
        public List<Booking> bookings { get; set; }
        public List<Booking> bookingActive { get; set; }
            
    }
}