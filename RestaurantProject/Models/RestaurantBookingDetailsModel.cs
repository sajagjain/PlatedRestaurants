using RestaurantDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantProject.Models
{
    public class RestaurantBookingDetailsModel
    {
        public List<Booking> ActiveBookings{ get; set; }
        public List<Booking> PendingBookings { get; set; }
        public List<Booking> CompletedBookings { get; set; }
        public List<Booking> OrderPlaced { get; set; }
    }
}