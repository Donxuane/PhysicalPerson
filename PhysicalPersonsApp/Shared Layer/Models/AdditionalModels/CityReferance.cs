using System.ComponentModel.DataAnnotations;

namespace Shared_Layer.Models.AdditionalModels;

public class CityReferance
{
    [Key]
    public int CityId { get; set; }
    [Required]
    public string Country { get; set; }
    public string City { get; set; }
}
