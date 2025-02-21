using Shared_Layer.Models.AdditionalModels;

namespace PhysicalPersonsApp.ViewModel;

public class UpdateDetailsViewModel
{
    public int Id { get; set; } 
    public string? Name { get; set; }
    public string? Surname {  get; set; }
    public string? PrivateId { get; set; }
    public string? Gender { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string? City {  get; set; }
    public IFormFile? Image {  get; set; }
    public RelatedPersons? RelatedPersons { get; set; }
    public int? RelatedPersonId { get; set; }
}
