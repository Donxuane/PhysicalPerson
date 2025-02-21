using System.ComponentModel.DataAnnotations;

namespace Shared_Layer.Models.AdditionalModels;

public class PhoneNumber
{
    [Key]
    public int? Id { get; set; } 
    public int? PhoneId { get; set; }
    public string? PhoneType { get; set; }
    [MinLength(4)]
    [MaxLength(50)]
    public string? Number { get; set; }
}
