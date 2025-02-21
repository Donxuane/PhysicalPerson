using DAL.DBSetup.AppDbContextRepo;
using DAL.IGeneralRepository.IPhoneNumberRepo;
using Microsoft.EntityFrameworkCore;
using Shared_Layer.Models.AdditionalModels;

namespace DAL.RepositoryImplementation
{
    public class PhoneNumberRepository : IPhoneNumberRepository
    {
        private readonly DbSet<PhoneNumber> _setPhone;

        public PhoneNumberRepository(AppDbContext context)
        {
            _setPhone = context.PhoneNumber;
        }

        public async Task AddNumber(PhoneNumber number)
        {
            await _setPhone.AddAsync(number);
        }

        public async Task<bool> CheckPhoneId(int phoneId)
        {
            var id = await _setPhone.FindAsync(phoneId);
            if (id != null)
            {
                return true;
            }
            return false;
        }

        public async Task DeletePhoneNumber(int id)
        {
            await _setPhone.Where(x => x.PhoneId == id).ExecuteDeleteAsync();
        }

        public async Task<List<PhoneNumber>?> GetPhoneNumberById(int id)
        {
            return await _setPhone.Where(x => x.PhoneId == id).ToListAsync();
        }

        public async Task<int> GetPhoneNumberId(string phoneNumber)
        {
            return (int)await _setPhone.Where(x => x.Number == phoneNumber).Select(x => x.PhoneId).FirstOrDefaultAsync();
        }

        public async Task UpdatePhoneNumber(string phoneNumber, int id)
        {
            await _setPhone.Where(x => x.PhoneId == id).ExecuteUpdateAsync(x => x.SetProperty(x => x.Number, phoneNumber));
        }
    }
}
