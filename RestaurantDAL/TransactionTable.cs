//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RestaurantDAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class TransactionTable
    {
        public int Trans_Id { get; set; }
        public string Trans_From { get; set; }
        public Nullable<int> Trans_From_Id { get; set; }
        public string Trans_To { get; set; }
        public Nullable<int> Trans_To_Id { get; set; }
        public Nullable<decimal> Trans_Amount { get; set; }
        public string Trans_Type { get; set; }
        public Nullable<System.DateTime> Trans_Time { get; set; }
    }
}
