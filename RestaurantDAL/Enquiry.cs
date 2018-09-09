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
    using System.ComponentModel.DataAnnotations;

    public partial class Enquiry
    {
        public int En_id { get; set; }

        [Required, StringLength(250)]
        public string Enquiry_Desc { get; set; }

        [Required(ErrorMessage = "Enter your Phone No")]
        [StringLength(10, ErrorMessage = "Phone No should Contain 10 Digits Only", MinimumLength = 10)]
        [DataType(DataType.PhoneNumber,ErrorMessage ="Enter Valid Phone Number")]
        public string Phone_No { get; set; }
        public Nullable<int> Res_Id { get; set; }
    
        public virtual Restaurant Restaurant { get; set; }
    }
}
