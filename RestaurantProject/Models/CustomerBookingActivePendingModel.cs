using RestaurantDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantProject.Models
{
    public class CustomerBookingActivePendingModel
    {
        public List<Booking> Active { get; set; }
        public List<Booking> Pending { get; set; }
        public List<Booking> Complete { get; set; }
        public List<Booking> Cancelled { get; set; }
        public List<Booking> OrderPlaced { get; set; }
    }
}