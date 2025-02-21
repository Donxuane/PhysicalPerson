using Shared_Layer.Models;

namespace DAL.IGeneralRepository.IPersonModel;

public interface IGetPersonModel
{
    public Task<List<PersonModel>> GetPersonByName(string name);
    public Task<List<PersonModel>> GetPersonBySurname(string surname);
    public Task<PersonModel?> GetPersonByPrivateId(string privateId);
    public Task<PersonModel?> GetPersonById(int Id);
    public Task<int> GetPersonsIdByPrivateId(string privateId);
    public Task<List<PersonModel>> GetAllPerson();
    public Task<List<PersonModel>> FastSearch(string searchCriteria);
}
