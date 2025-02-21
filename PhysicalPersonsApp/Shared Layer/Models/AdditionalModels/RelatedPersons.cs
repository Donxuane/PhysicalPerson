using System.ComponentModel.DataAnnotations;

namespace Shared_Layer.Models.AdditionalModels;

public class RelatedPersons
{
    [Key]
    public int? Id { get; set; } 
    public int? PersonId { get; set; }
    public string? RelationType { get; set; }
    public string? PersonName { get; set; }
    public string? PersonSurname { get; set; }
}
