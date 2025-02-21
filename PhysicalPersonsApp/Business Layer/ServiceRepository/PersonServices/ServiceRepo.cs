using Business_Layer.ServiceRepository.ErrorLogingService;
using DAL.DBSetup.UnitOfWorkRepo;
using Microsoft.IdentityModel.Tokens;
using Shared_Layer.Models;
using Shared_Layer.Models.AdditionalModels;

namespace Business_Layer.ServiceRepository.PersonServices
{
    public class ServiceRepo : IServiceRepo
    {
        private readonly IUnitOfWork _work;
        private readonly IFileLogService _log;
        public ServiceRepo(IUnitOfWork work, IFileLogService log)
        {
            _work = work;
            _log = log;
        }

        public async Task AddMoreReltedPersons(int RelatedPersonId, RelatedPersons relatedPersons)
        {
            try
            {
                relatedPersons.PersonId = RelatedPersonId;
                await _work.RelatedPersons.AddRelatedPerson(relatedPersons);
                await _work.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                await _log.LogErrorTextFile($"Add Related Person Error: {ex}");
            }
        }

        public async Task AddNewPerson(PersonModel person)
        {
            try
            {
                await _work.Person.AddPerson(person);
                await _work.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _log.LogErrorTextFile($"Add Person Error: {ex}");
            }
        }

        public async Task AddPersonsRelatedPerson(PersonModel person, RelatedPersons relatedPersons)
        {
            try
            {
                if(person.RelatedPersonsId == null)
                {
                    var random = new Random();
                    person.RelatedPersonsId = random.Next(100, 1000);
                }
                relatedPersons.PersonId = person.RelatedPersonsId;
                await _work.RelatedPersons.AddRelatedPerson(relatedPersons);
                await _work.SaveChangesAsync();
            }
            catch(Exception ex)
            {
               
                await _log.LogErrorTextFile($"Add Error: {ex}");
            }
        }

        public async Task AddPhoneNumber(PersonModel person, PhoneNumber number)
        {
            var random = new Random();
            while (true)
            {
                var randomId = random.Next(0, 1000);
                if (await _work.PersonsPhoneNumber.CheckPhoneId(randomId) != true)
                {
                    person.PhoneNumberId = randomId;
                    number.PhoneId = randomId;
                    break;
                }
            }
            try
            {
                await _work.PersonsPhoneNumber.AddNumber(number);
                await _work.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _log.LogErrorTextFile($"Add Phone Number Error: {ex}");
            }
        }

        public async Task DeletePerson(int personId)
        {
            try
            {
                var person = await _work.Person.GetPersonById(personId);
                if (person.RelatedPersonsId != null)
                {
                    await _work.RelatedPersons.DeleteRelatedPerson((int)person.RelatedPersonsId);
                    await _work.SaveChangesAsync();
                }
                if (person.PhoneNumberId != null)
                {
                    await _work.PersonsPhoneNumber.DeletePhoneNumber(personId);
                    await _work.SaveChangesAsync();
                }
                await _work.Person.DeletePerson(personId);
                await _work.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                await _log.LogErrorTextFile($"Delete Error: {ex}");
            }
        }

        public async Task DeletePersonsRelatedPerson(int relatedPersonsId)
        {
            try
            {
                await _work.RelatedPersons.DeleteRelatedPerson(relatedPersonsId);
                await _work.SaveChangesAsync();
            }catch(Exception ex)
            {
                await _log.LogErrorTextFile($"Delete Error: {ex}");
            }
        }

        public async Task<List<PersonModel>> FastSearch(string searchCriteria)
        {
            if (searchCriteria.IsNullOrEmpty())
            {
                return await _work.Person.GetAllPerson();
            }
            return await _work.Person.FastSearch(searchCriteria);
        }

        public async Task<PersonModel> GetPersonById(int personId)
        {
            return await _work.Person.GetPersonById(personId);
        }

        public async Task<List<PhoneNumber>?> GetPhoneNumberList(PersonModel person)
        {
            var id = person.PhoneNumberId;
            return await _work.PersonsPhoneNumber.GetPhoneNumberById((int)id);
        }

        public async Task<List<RelatedPersons>> GetRelatedPerson(int personId)
        {
            return await _work.RelatedPersons.GetRelatedPersons(personId);
        }

        public async Task<(IEnumerable<PersonModel> Items, int TotalPages)> Paging(int page, List<PersonModel> model, int itemsPerPage)
        {
            
            int totalPages = (int)Math.Ceiling((double)model.Count / itemsPerPage);
            if (page > totalPages)
                return (Enumerable.Empty<PersonModel>(), totalPages);
            int dataAmount = (page - 1) * itemsPerPage;
            var list = model.Skip(dataAmount).Take(itemsPerPage).ToList();
            return await Task.FromResult((list, totalPages));
        }

        public async Task<List<RelatedPersonsTypeStatistics>> StatisticsRelatedPersonsType(int personId)
        {
            List<RelatedPersonsTypeStatistics> finalStatistics = [];
            var relatedPersonsList = await _work.RelatedPersons.GetRelatedPersons(personId);
            var so = relatedPersonsList.Select(x => x.RelationType).ToList();
            var si = so.Distinct().ToList();

            if (si.Count != 0 || si != null)
            {
                for (int i = 0; i < si.Count; i++)
                {
                    int count = 0;
                    foreach (var per in relatedPersonsList)
                    {
                        if (si[i] == per.RelationType)
                        {
                            count++;
                        }
                    }
                    finalStatistics.Add(new RelatedPersonsTypeStatistics { RelationType = si[i], Amount = count });
                }
            }
            return finalStatistics;
        }

        public async Task UpdatePersonsBirthDate(DateOnly birthDate, int Id)
        {
            try
            {
                await _work.Person.UpdateBirthDate(birthDate, Id);
                await _work.SaveChangesAsync();
            }catch(Exception ex)
            {
                await _log.LogErrorTextFile($"Update Info Error: {ex}");
            }
        }

        public async Task UpdatePersonsCity(string city, int Id)
        {
            try
            {
                await _work.Person.UpdateCity(city, Id);
                await _work.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _log.LogErrorTextFile($"Update Info Error: {ex}");
            }
        }

        public async Task UpdatePersonsGender(string gender, int Id)
        {
            try
            {
                await _work.Person.UpdateGender(gender, Id);
                await _work.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _log.LogErrorTextFile($"Update Info Error: {ex}");
            }
        }

        public async Task UpdatePersonsName(string name, int Id)
        {
            try
            {
                await _work.Person.UpdateName(name, Id);
                await _work.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _log.LogErrorTextFile($"Update Info Error: {ex}");
            }
        }

        public async Task UpdatePersonsPhoto(int Id, string path)
        {
            try
            {
                await _work.Person.UpdateImage(path, Id);
                await _work.SaveChangesAsync();
            }catch(Exception ex)
            {
                await _log.LogErrorTextFile($"Upload Image Error: {ex}");
            }
        }

        public async Task UpdatePersonsPrivateId(string privateId, int Id)
        {
            try
            {
                await _work.Person.UpdatePrivateId(privateId, Id);
                await _work.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _log.LogErrorTextFile($"Update Info Error: {ex}");
            }
        }

        public async Task UpdatePersonsSurname(string surname, int Id)
        {
            try
            {
                await _work.Person.UpdateSurname(surname, Id);
                await _work.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _log.LogErrorTextFile($"Update Info Error: {ex}");
            }
        }
    }
}
