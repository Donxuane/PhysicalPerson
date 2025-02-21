using System.ComponentModel.DataAnnotations;

namespace PhysicalPersonsApp.CustomAttributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]   
public class CitiIdentifierAttribute : ValidationAttribute
{
}
