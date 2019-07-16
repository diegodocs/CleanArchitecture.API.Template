using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Common.Repository.Contracts.Core.Attributes
{
    public class ValidGuidAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var isValidGuid = Guid.TryParse(value.ToString(), out var result);

            if (isValidGuid && result != Guid.Empty)
                return ValidationResult.Success;

            return new ValidationResult("The provided value is not a valid Guid data.");
        }
    }
}