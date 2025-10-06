using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace RentACar.Models
{
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _startDateProperty;

        public DateGreaterThanAttribute(string startDateProperty)
        {
            _startDateProperty = startDateProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var startDateProperty = validationContext.ObjectType.GetProperty(_startDateProperty);
            if (startDateProperty == null)
                return new ValidationResult($"Unknown property: {_startDateProperty}");

            var startDateValue = startDateProperty.GetValue(validationContext.ObjectInstance, null) as DateTime?;

            if (startDateValue == null)
                return new ValidationResult("Start date must be provided.");

            if (value is DateTime endDate)
            {
                if (endDate <= startDateValue)
                {
                    return new ValidationResult(ErrorMessage ?? "End date must be after the start date.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
