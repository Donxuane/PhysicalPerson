using Shared_Layer.Models;
using Shared_Layer.Models.AdditionalModels;
namespace PhysicalPersonsApp.ViewModel;

public class AddViewModels
{
    public PersonModel person { get; set; }
    public PhoneNumber? Phone {  get; set; }
    public RelatedPersons? Related { get; set; }
    public IFormFile? file {  get; set; }

    public List<string> Gender;

    public AddViewModels()
    {
        person = new PersonModel();
        Gender = ["Male", "Female"];
    }
}
