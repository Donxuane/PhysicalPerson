using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PhysicalPersonsApp.CustomAttributes;

[AttributeUsage(AttributeTargets.Property,AllowMultiple =true, Inherited = false)]
public class InputIdentifierAttribute : ValidationAttribute
{
    private string? name;

    public InputIdentifierAttribute(string name)
    {
        this.name = name;
    }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        string errorMessage = $"{name} Must Contain Only English Or Only Georgian Letters!";
        if (value is string input)
        {
            var GeorgianLetters = new Regex(@"^[\u10A0-\u10FF\u1C90-\u1CBF]+$");
            var EnglishLetters = new Regex(@"^[A-Za-z]+$");
            if(!GeorgianLetters.IsMatch(input) && !EnglishLetters.IsMatch(input))
            {
                return new ValidationResult(errorMessage);
            }
        }
        return ValidationResult.Success;
    }
}
