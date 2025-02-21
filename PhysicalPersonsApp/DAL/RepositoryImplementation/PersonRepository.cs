using DAL.DBSetup.AppDbContextRepo;
using DAL.IGeneralRepository.IPersonModel;
using Microsoft.EntityFrameworkCore;
using Shared_Layer.Models;

namespace DAL.RepositoryImplementation
{
    public class PersonRepository : IPersonModel
    {
        private readonly DbSet<PersonModel> _setPerson;

        public PersonRepository(AppDbContext context)
        {
            _setPerson = context.PersonModel;
        }
        public async Task AddPerson(PersonModel person)
        {
            await _setPerson.AddAsync(person);
        }

        public async Task DeletePerson(int Id)
        {
            await _setPerson.Where(x => x.Id == Id).ExecuteDeleteAsync();
        }

        public async Task<List<PersonModel>> FastSearch(string searchCriteria)
        {
            return await _setPerson.Where(x => x.Name == searchCriteria || x.Surname == searchCriteria
            || x.PrivateId == searchCriteria || x.Gender == searchCriteria).ToListAsync();
        }

        public async Task<List<PersonModel>> GetAllPerson()
        {
            return await _setPerson.ToListAsync();
        }

        public async Task<PersonModel?> GetPersonById(int Id)
        {
            return await _setPerson.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<PersonModel>> GetPersonByName(string name)
        {
            var list = await _setPerson.Where(x => x.Name == name).ToListAsync();
            return list;
        }

        public async Task<PersonModel?> GetPersonByPrivateId(string privateId)
        {

            var person = await _setPerson.FirstOrDefaultAsync(x => x.PrivateId == privateId);
            return person;
        }

        public async Task<List<PersonModel>> GetPersonBySurname(string surname)
        {
            var list = await _setPerson.Where(x => x.Surname == surname).ToListAsync();
            return list;
        }

        public async Task<int> GetPersonsIdByPrivateId(string privateId)
        {
            return await _setPerson.Where(x => x.PrivateId == privateId).Select(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task UpdateBirthDate(DateOnly birthDate, int Id)
        {
            await _setPerson.Where(x => x.Id == Id).ExecuteUpdateAsync(x => x.SetProperty(x => x.BirthDate, birthDate));
        }

        public async Task UpdateCity(string city, int id)
        {
            await _setPerson.Where(x => x.Id == id).ExecuteUpdateAsync(x => x.SetProperty(x => x.City, city));
        }

        public async Task UpdateGender(string gender, int id)
        {
            await _setPerson.Where(x => x.Id == id).ExecuteUpdateAsync(x => x.SetProperty(x => x.Gender, gender));
        }

        public async Task UpdateImage(string path, int id)
        {
            await _setPerson.Where(x => x.Id == id).ExecuteUpdateAsync(x => x.SetProperty(x => x.ImagePath, path));
        }

        public async Task UpdateName(string name, int id)
        {
            await _setPerson.Where(x => x.Id == id).ExecuteUpdateAsync(x => x.SetProperty(x => x.Name, name));
        }

        public async Task UpdatePrivateId(string privateId, int id)
        {
            await _setPerson.Where(x => x.Id == id).ExecuteUpdateAsync(x => x.SetProperty(x => x.PrivateId, privateId));
        }

        public async Task UpdateSurname(string surname, int id)
        {
            await _setPerson.Where(x => x.Id == id).ExecuteUpdateAsync(x => x.SetProperty(x => x.Surname, surname));
        }
    }
}
