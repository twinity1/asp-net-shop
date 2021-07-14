using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DyShop.Helpers.Validation
{
    [AttributeUsage( AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ValidateIfAttribute : ValidationAttribute, IPropertyValidationFilter
    {
        private readonly string _property;

        public ValidateIfAttribute(string property)
        {
            _property = property;
        }
    
        public bool ShouldValidateEntry(ValidationEntry entry, ValidationEntry parentEntry)
        {
            object? instance = parentEntry.Model;
            Type? type = instance?.GetType();

            bool.TryParse(type?.GetProperty(_property)?.GetValue(instance)?.ToString(), out bool propertyValue);
            
            return propertyValue;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            return ValidationResult.Success;
        }
    }
}