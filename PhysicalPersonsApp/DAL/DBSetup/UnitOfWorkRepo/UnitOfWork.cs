using DAL.DBSetup.AppDbContextRepo;
using DAL.IGeneralRepository.IPersonModel;
using DAL.IGeneralRepository.IPhoneNumberRepo;
using DAL.IGeneralRepository.IRelatedPersonsRepo;
using DAL.RepositoryImplementation;
using Microsoft.EntityFrameworkCore;
using PhysicalPersonsApp.IGeneralRepository.ICityReferanceRepository;
using System.Data;
using System.Data.Common;


namespace DAL.DBSetup.UnitOfWorkRepo;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IPersonModel _person;
    private readonly IPhoneNumberRepository _personPhoneNumber;
    private readonly IRelatedPersonsRepository _relatedPersons;
    private readonly ICItyAddRepo _city;

    public UnitOfWork(AppDbContext context, IPersonModel person, IPhoneNumberRepository phoneNumber,
        IRelatedPersonsRepository relatedPersons,ICItyAddRepo city)
    {
        _context = context;
        _person = person;
        _personPhoneNumber = phoneNumber;
        _relatedPersons = relatedPersons;
        _city = city;
    }
    public IPersonModel Person
    {
        get
        {
            return _person;
        }
    }
    public IPhoneNumberRepository PersonsPhoneNumber
    {
        get
        {
            return _personPhoneNumber;
        }
    }

    public IRelatedPersonsRepository RelatedPersons
    {
        get
        {
            return _relatedPersons;
        }
    }

    public ICItyAddRepo CityManageAdmin => _city;



    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
