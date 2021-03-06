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

    public partial class Restaurant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Restaurant()
        {
            this.Admins = new HashSet<Admin>();
            this.Bookings = new HashSet<Booking>();
            this.Enquiries = new HashSet<Enquiry>();
            this.Events = new HashSet<Event>();
            this.Feedbacks = new HashSet<Feedback>();
            this.Foods = new HashSet<Food>();
            this.Offers = new HashSet<Offer>();
        }
    
        public int Res_Id { get; set; }

        [Required(ErrorMessage = "Please Enter Your Restaurant Name ")]
        [Display(Name = "Restaurant Name")]
        [StringLength(50,ErrorMessage ="Name cannot be greater than 50")]
        [RegularExpression(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Please Enter Valid Name")]
        public string Res_Name { get; set; }

        [Required(ErrorMessage = "Please Enter Your Restaurant Address ")]
        [Display(Name = "Restaurant Address")]
        [StringLength(250,ErrorMessage ="Please Enter A Short Address")]
        public string Res_Address { get; set; }

        [Required(ErrorMessage = "Please Enter Your Restaurant city ")]
        [Display(Name = "Restaurant City ")]
        [RegularExpression(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Please Enter Valid City Name")]
        public string Res_City { get; set; }

        [Required(ErrorMessage = "Please Enter Your Restaurant No of Tables ")]
        [Display(Name = " No of Tables ")]
        public byte Res_No_Of_Tables { get; set; }

        [Required(ErrorMessage = "Please Enter Your Restaurant capacity ")]
        [Display(Name = "Restaurant Capacity")]
        public short Res_Capacity { get; set; }
        public bool Do_Parties { get; set; }

        [Required(ErrorMessage = "Please Enter Your Name")]
        [RegularExpression(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Please Enter Valid Name")]
        [Display(Name = "Name")]
        public string Owner_Name { get; set; }

        [Required(ErrorMessage = "Please Enter Your Email ")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Please Enter Correct Mail Address")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Please Enter Valid Email")]
        public string Owner_Email { get; set; }

        [Required(ErrorMessage = "Please Enter Your MobileNo ")]
        [Display(Name = "MobileNo")]
        [DataType(DataType.PhoneNumber)]
        public string Owner_MobileNo { get; set; }

        [Required(ErrorMessage = "Please Enter Your password")]
        [Display(Name = "password")]
        [StringLength(40,ErrorMessage ="Please choose a password with less than 40 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public Nullable<int> wallet_id { get; set; }
        public string Res_Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Admin> Admins { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Enquiry> Enquiries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Events { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Food> Foods { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Offer> Offers { get; set; }
        public virtual Wallet Wallet { get; set; }
    }
}
