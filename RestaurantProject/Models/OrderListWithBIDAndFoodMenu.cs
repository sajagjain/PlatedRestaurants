using RestaurantDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantProject.Models
{
    public class OrderListWithBIDAndFoodMenu
    {
        public List<Order> Order { get; set; }
        public List<Food> FoodStarters { get; set; }
        public List<Food> FoodMainCourse { get; set; }
        public List<Food> FoodDesserts { get; set; }
        public int bid { get; set; }
        public int resId { get; set; }


    }
}