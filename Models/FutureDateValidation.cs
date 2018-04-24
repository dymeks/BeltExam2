using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeltExam2
{
    public class FutureDateAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime date = (DateTime)value;

            if (date.Date < DateTime.Today)
            {
                return new ValidationResult("Activity Must be a Future Date.");
            }

            return ValidationResult.Success;
        }
    }
}