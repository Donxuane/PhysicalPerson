using Shared_Layer.Models.AdditionalModels;

namespace DAL.IGeneralRepository.IPhoneNumberRepo;

public interface IPhoneNumberRepository
{
    public Task<int> GetPhoneNumberId(string phoneNumber);
    public Task<List<PhoneNumber>?> GetPhoneNumberById(int id);
    public Task UpdatePhoneNumber(string phoneNumber, int id);
    public Task DeletePhoneNumber(int id);
    public Task AddNumber(PhoneNumber number);
    public Task<bool> CheckPhoneId(int phoneId);
}
