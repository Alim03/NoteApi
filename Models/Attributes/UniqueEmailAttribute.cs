using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Challenge.Repositories;

namespace Challenge.Models.Attributes
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var repository = validationContext.GetService(typeof(INoteRepository)) as INoteRepository;

            if (repository.IsEmailExist(value.ToString()))
            {
                return new ValidationResult("Email address is already in use.");
            }

            return ValidationResult.Success;
        }
    }
}
