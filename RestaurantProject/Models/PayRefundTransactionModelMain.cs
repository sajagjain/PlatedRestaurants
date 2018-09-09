using RestaurantDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantProject.Models
{
    public class PayRefundTransactionModelMain
    {
        public Wallet wallet { get; set; }
        public TransactionTable transaction { get; set; }
    }
}