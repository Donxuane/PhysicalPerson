using DAL.DBSetup.AppDbContextRepo;
using DAL.IGeneralRepository.IRelatedPersonsRepo;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Shared_Layer.Models.AdditionalModels;
using System.Data;
namespace DAL.RepositoryImplementation;

public class RelatedPersonsRepository : IRelatedPersonsRepository
{
    private readonly DbSet<RelatedPersons> _set;
    public RelatedPersonsRepository(AppDbContext context)
    {
        _set = context.RelatedPersons;
    }
    public async Task AddRelatedPerson(RelatedPersons relatedPerson)
    {
       await _set.AddAsync(relatedPerson);
    }

    public async Task DeleteRelatedPerson(int Id)
    {
        await _set.Where(x => x.Id == Id).ExecuteDeleteAsync();
    }

    public async Task<List<RelatedPersons>> GetRelatedPersons(int personId)
    {
        var list = await _set.Where(x => x.PersonId == personId).ToListAsync();
        return list;
    }

    public async Task<List<RelatedPersons>> GetRelatedPersonsByType(string type, int personId)
    {
        var list = await _set.Where(x => x.PersonId == personId && x.RelationType == type).ToListAsync();
        return list;
    }
}
