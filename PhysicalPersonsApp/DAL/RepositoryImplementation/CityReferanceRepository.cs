using DAL.DBSetup.AppDbContextRepo;
using Microsoft.EntityFrameworkCore;
using PhysicalPersonsApp.IGeneralRepository.ICityReferanceRepository;
using Shared_Layer.Models.AdditionalModels;

namespace DAL.RepositoryImplementation
{
    public class CityReferanceRepository : ICItyAddRepo
    {
        private readonly DbSet<CityReferance> _set;
        public CityReferanceRepository(AppDbContext context) => _set = context.CityReferance;
        public async Task AddCityReferanceAdmin(CityReferance city)
        {
            await _set.AddAsync(city);
        }
    }
}
