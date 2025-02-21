using DAL.IGeneralRepository.IPersonModel;
using DAL.IGeneralRepository.IPhoneNumberRepo;
using DAL.IGeneralRepository.IRelatedPersonsRepo;
using PhysicalPersonsApp.IGeneralRepository.ICityReferanceRepository;

namespace DAL.DBSetup.UnitOfWorkRepo;

public interface IUnitOfWork
{
    public IPersonModel Person { get; }
    public IPhoneNumberRepository PersonsPhoneNumber { get; }
    public IRelatedPersonsRepository RelatedPersons { get; }
    public ICItyAddRepo CityManageAdmin { get; }
    public Task SaveChangesAsync();
}
