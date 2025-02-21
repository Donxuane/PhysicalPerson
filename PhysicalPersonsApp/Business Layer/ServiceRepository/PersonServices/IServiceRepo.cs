using Shared_Layer.Models;
using Shared_Layer.Models.AdditionalModels;
namespace Business_Layer.ServiceRepository.PersonServices;

public interface IServiceRepo
{
    public Task AddPhoneNumber(PersonModel person, PhoneNumber number);
    public Task<List<PhoneNumber>?> GetPhoneNumberList(PersonModel person);
    public Task AddNewPerson(PersonModel person);
    public Task UpdatePersonsPhoto(int Id, string path);
    public Task AddPersonsRelatedPerson(PersonModel person, RelatedPersons relatedPersons);
    public Task DeletePersonsRelatedPerson(int relatedPersonsId);
    public Task DeletePerson(int personId);
    public Task<PersonModel> GetPersonById(int personId);
    public Task<List<PersonModel>> FastSearch(string searchCriteria);
    public Task<List<RelatedPersonsTypeStatistics>> StatisticsRelatedPersonsType(int PersonId);
    public Task UpdatePersonsName(string name, int Id);
    public Task UpdatePersonsSurname(string surname, int Id);
    public Task UpdatePersonsGender(string gender, int Id);
    public Task UpdatePersonsPrivateId(string privateId, int Id);
    public Task UpdatePersonsBirthDate(DateOnly birthDate, int Id);
    public Task UpdatePersonsCity(string city, int Id);
    public Task<List<RelatedPersons>> GetRelatedPerson(int personId);
    public Task AddMoreReltedPersons(int RelatedPersonId,  RelatedPersons relatedPersons);
    public Task<(IEnumerable<PersonModel> Items, int TotalPages)> Paging(int page, List<PersonModel> model, int itemsPerPage);
}
