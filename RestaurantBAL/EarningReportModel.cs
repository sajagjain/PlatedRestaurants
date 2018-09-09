using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantProject.Models
{
    public class EarningReportModel
    {
        public int today { get; set; }
        public int thisWeek{get;set;}
        public int thisMonth { get; set; }
        public int thisYear { get; set; }
        public int tillNow { get; set; }

    }
}