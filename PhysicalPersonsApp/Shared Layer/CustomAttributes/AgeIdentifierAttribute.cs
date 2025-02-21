using System.ComponentModel.DataAnnotations;

namespace PhysicalPersonsApp.CustomAttributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited =false)]
public class AgeIdentifierAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value is DateOnly date)
        {
            var currentDate = DateTime.Now;
            var age = currentDate.Year - date.Year;
            if(currentDate.Month < date.Month || currentDate.Day < date.Day)
            {
                age -= 1;
            }

            if(age < 18)
            {
                return new ValidationResult("Person Must Be At Least 18 Years Old!");
            }
        }
        return ValidationResult.Success;
    }
}
