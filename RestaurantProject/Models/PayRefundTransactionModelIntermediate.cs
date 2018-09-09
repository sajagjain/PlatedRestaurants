using RestaurantDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantProject.Models
{
    public class PayRefundTransactionModelIntermediate
    {
        public int walletId { get; set; }
        public decimal amount { get; set; }
        public int transToId { get; set; }
        public string transToUserType { get; set; }


    }
}