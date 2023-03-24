using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Challenge.Repositories;

namespace Challenge.Models.Attributes
{
    public class UniqueEmailOnUpdateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext
        )
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var repository =
                validationContext.GetService(typeof(IUserRepository)) as IUserRepository;
            var idProperty = validationContext.ObjectType.GetProperty("Id");

            if (idProperty == null)
            {
                throw new ArgumentException("Object does not have an Id property");
            }

            var id = (int)idProperty.GetValue(validationContext.ObjectInstance);

            if (
                repository.GetByEmail(value.ToString())?.Id == id
                || !repository.IsEmailExist(value.ToString())
            )
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Email address is already in use.");
        }
    }
}
