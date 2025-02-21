using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PhysicalPersonsApp.CustomAttributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple =true, Inherited=false)]   
public class IdIdentifierAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var regex = new Regex(@"^\d{11}$");
        if (value == null || value.GetType() != typeof(string) || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return new ValidationResult("Id Is Required!");
        }
        if (value is string input)
        {           
            if (input.Length != 11)
            {
                return new ValidationResult("ID Must Be 11 Numbers");
            }
            else if (!regex.IsMatch(input))
            {
                return new ValidationResult("ID Must Contain only Numbers");
            }
        }
        return ValidationResult.Success;
    }
}
