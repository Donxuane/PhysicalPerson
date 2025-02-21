using Shared_Layer.Models.AdditionalModels;

namespace PhysicalPersonsApp.IGeneralRepository.ICityReferanceRepository
{
    public interface ICItyAddRepo
    {
        public Task AddCityReferanceAdmin(CityReferance city);
    }
}
