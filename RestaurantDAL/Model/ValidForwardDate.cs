using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDAL.Model
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidForwardDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime _birthJoin = Convert.ToDateTime(value);
                if (_birthJoin < DateTime.Now)
                {
                    return new ValidationResult("Arrival date can not be lesser than current date.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
