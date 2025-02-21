using Shared_Layer.Models;

namespace DAL.IGeneralRepository.IPersonModel;

public interface IPersonModel : IGetPersonModel, IUpdatePersonModel
{
    public Task AddPerson(PersonModel person);
    public Task DeletePerson(int Id);
}

