using Shared_Layer.Models;
using Shared_Layer.Models.AdditionalModels;

namespace PhysicalPersonsApp.ViewModel
{
    public class DetailsViewModel
    {
        public PersonModel Person { get; set; }
        public List<RelatedPersons?>? Related {  get; set; }   
        public List<PhoneNumber?>? PhoneNumbers { get; set; }
        public List<RelatedPersonsTypeStatistics?>? Statistics { get; set; }
    }
}
