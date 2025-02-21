using PhysicalPersonsApp.CustomAttributes;
using Shared_Layer.Models.AdditionalModels;
using System.ComponentModel.DataAnnotations;

namespace Shared_Layer.Models;

public class PersonModel
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    [InputIdentifier("Name")]
    public string Name { get; set; }
    [Required]
    [MaxLength(50)]
    [MinLength(2)]
    [InputIdentifier("Surname")]
    public string Surname { get; set; }
    public string? Gender { get; set; }
    [IdIdentifier]
    public string PrivateId { get; set; }
    [Required]
    [AgeIdentifier]
    public DateOnly BirthDate { get; set; }
    //[CitiIdentifier]
    [MaxLength(50)]
    public string? City { get; set; } 
    public int? PhoneNumberId { get; set; }
    [MinLength(4)]
    public string? ImagePath { get; set; }
    public int? RelatedPersonsId { get; set; }
    public List<RelatedPersons?>? RelatedPersons { get; set; }
    public List<PhoneNumber?>? PhoneNumber { get; set; }
}
