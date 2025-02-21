using Shared_Layer.Models.AdditionalModels;

namespace DAL.IGeneralRepository.IRelatedPersonsRepo;

public interface IRelatedPersonsRepository
{
    public Task AddRelatedPerson(RelatedPersons relatedPerson);
    public Task<List<RelatedPersons>> GetRelatedPersons(int personId);
    public Task DeleteRelatedPerson(int Id);
    public Task<List<RelatedPersons>> GetRelatedPersonsByType(string type, int tableName);
}
