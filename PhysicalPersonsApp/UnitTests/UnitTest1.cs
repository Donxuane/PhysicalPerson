using Business_Layer.ServiceRepository.ErrorLogingService;
using Business_Layer.ServiceRepository.PersonServices;
using DAL.DBSetup.UnitOfWorkRepo;
using DAL.IGeneralRepository.IPersonModel;
using DAL.IGeneralRepository.IPhoneNumberRepo;
using DAL.IGeneralRepository.IRelatedPersonsRepo;
using Moq;
using Shared_Layer.Models;
using Shared_Layer.Models.AdditionalModels;
using System.ComponentModel.DataAnnotations;

namespace UnitTests;

public class UnitTest1
{
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private Mock<IFileLogService> _mockFileLogService;
    private ServiceRepo _service;
    private readonly PersonModel person = new()
    {
        Id = 1,
        Name = "vakho",
        Surname = "chitashvili",
        PrivateId = "11111111111",
        BirthDate = new DateOnly(2004, 2, 12),
        RelatedPersonsId = null,
        PhoneNumberId = null
    };
    private readonly RelatedPersons related = new()
    {
        Id = 1,
        PersonName = "vaxo",
        PersonSurname = "chit",
        RelationType = "bro"
    };


    public UnitTest1()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockFileLogService = new Mock<IFileLogService>();
        _service = new ServiceRepo(_mockUnitOfWork.Object, _mockFileLogService.Object);
    }
    //Testing Log Service
    [Fact]
    public async Task UpdatePersonsCity_ShouldNotUpdateConcretePersonsLivingCityWhenCityIsNull()
    {
        string? city = null;
        Assert.Null(city);
        _mockFileLogService.Setup(x => x.LogErrorTextFile(It.IsAny<string>()));
        await _service.UpdatePersonsCity(city, It.IsAny<int>());
        _mockUnitOfWork.Verify(x => x.Person.UpdateCity(city, It.IsAny<int>()), Times.Never);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Never);
        _mockFileLogService.Verify(x => x.LogErrorTextFile(It.IsAny<string>()), Times.Once);
    }
    [Fact]
    public async Task AddNewPerson_ShouldAddNewPerson()
    {
        _mockUnitOfWork.Setup(x => x.Person.AddPerson(person)).Verifiable();
        await _service.AddNewPerson(person);
        _mockUnitOfWork.Verify(x => x.Person.AddPerson(person), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
    [Fact]
    public async Task DeletePerson_ShouldDeletePerson()
    {
        _mockUnitOfWork.Setup(x => x.Person.GetPersonById(person.Id)).ReturnsAsync(person);
        await _service.DeletePerson(person.Id);
        _mockUnitOfWork.Verify(x => x.Person.GetPersonById(person.Id), Times.Once);
        _mockUnitOfWork.Verify(x => x.RelatedPersons.DeleteRelatedPerson(It.IsAny<int>()), Times.Never);
        _mockUnitOfWork.Verify(x => x.PersonsPhoneNumber.DeletePhoneNumber(It.IsAny<int>()), Times.Never);
        _mockUnitOfWork.Verify(x => x.Person.DeletePerson(person.Id), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Exactly(1));
    }

    [Fact]
    public async Task AddMoreRelatedPersons_ShouldAddRelatedPerson()
    {
        _mockUnitOfWork.Setup(x => x.RelatedPersons.AddRelatedPerson(related)).Verifiable();
        await _service.AddMoreReltedPersons(2, related);
        related.PersonId = 2;
        _mockUnitOfWork.Verify(x => x.RelatedPersons.AddRelatedPerson(related), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task AddRelatedPerson_ShouldAddRelatedPerso()
    {
        _mockUnitOfWork.Setup(x => x.RelatedPersons.AddRelatedPerson(related)).Verifiable();
        await _service.AddPersonsRelatedPerson(person, related);
        person.RelatedPersonsId = 100;
        Assert.NotNull(person.RelatedPersonsId);
        related.PersonId = person.RelatedPersonsId;
        _mockUnitOfWork.Verify(x => x.RelatedPersons.AddRelatedPerson(related), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task AddPhoneNumber_ShouldAddPhoneNumber()
    {
        var phoneNumber = new PhoneNumber() { Id = 1, Number = "555555555", PhoneId = 2, PhoneType = "mobile" };
        PersonModel person1 = new()
        {
            Id = 1,
            Name = "vakho",
            Surname = "chitashvili",
            PrivateId = "11111111111",
            BirthDate = new DateOnly(2004, 2, 12),
            RelatedPersonsId = null,
            PhoneNumberId = 2
        };
        _mockUnitOfWork.Setup(x => x.PersonsPhoneNumber.AddNumber(phoneNumber)).Verifiable();
        await _service.AddPhoneNumber(person1, phoneNumber);
        _mockUnitOfWork.Setup(x => x.PersonsPhoneNumber.CheckPhoneId(3)).ReturnsAsync(true);
        _mockUnitOfWork.Verify(x => x.PersonsPhoneNumber.AddNumber(phoneNumber), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
    [Fact]
    public async Task DeletePersonsRelatedPerson_ShouldDeleteRelatedperson()
    {
        var Id = 123;
        _mockUnitOfWork.Setup(x => x.RelatedPersons.DeleteRelatedPerson(Id)).Verifiable();
        await _service.DeletePersonsRelatedPerson(Id);
        _mockUnitOfWork.Verify(x => x.RelatedPersons.DeleteRelatedPerson(Id), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
    [Fact]
    public async Task FastSearch_ShouldReturnAllWhileNullValue()
    {
        var list = new List<PersonModel>();
        string? searchCriteria = null;
        Assert.Null(searchCriteria);
        _mockUnitOfWork.Setup(x => x.Person.GetAllPerson()).ReturnsAsync(list);
        await _service.FastSearch(searchCriteria);
        _mockUnitOfWork.Verify(x => x.Person.GetAllPerson(), Times.Once);
    }
    [Fact]
    public async Task FastSearch_ShouldReturnListOfPersonsWhichHaveCommonCriteriaWhileValueIsNotNull()
    {
        var list = new List<PersonModel>();
        string searchCriteria = "name";
        Assert.NotNull(searchCriteria);
        _mockUnitOfWork.Setup(x => x.Person.FastSearch(searchCriteria)).ReturnsAsync(list);
        await _service.FastSearch(searchCriteria);
        _mockUnitOfWork.Verify(x => x.Person.FastSearch(searchCriteria), Times.Once);
    }
    [Fact]
    public async Task GetPersonById_ShouldReturnPersonWithConcreteId()
    {
        _mockUnitOfWork.Setup(x => x.Person.GetPersonById(1)).ReturnsAsync(person).Verifiable();
        await _service.GetPersonById(1);
        _mockUnitOfWork.Verify(x => x.Person.GetPersonById(1), Times.Once);
    }
    [Fact]
    public async Task GetPhoneNumberList_ShouldReturnAllPhoneNumbersPersonHasIncluded()
    {
        List<PhoneNumber> list = [new PhoneNumber { Id = 1, PhoneId = 12 }];
        PersonModel model = new() { Id = 2, PhoneNumberId = 12 };
        _mockUnitOfWork.Setup(x => x.PersonsPhoneNumber.GetPhoneNumberById(12)).ReturnsAsync(list).Verifiable();
        await _service.GetPhoneNumberList(model);
        _mockUnitOfWork.Verify(x => x.PersonsPhoneNumber.GetPhoneNumberById(12), Times.Once);
    }
    [Fact]
    public async Task GetRelatedPerson_ShouldReturnAllRelatedPeopleConcretePersonHas()
    {
        List<RelatedPersons> list = [];
        int id = 1;
        _mockUnitOfWork.Setup(x => x.RelatedPersons.GetRelatedPersons(id)).ReturnsAsync(list).Verifiable();
        await _service.GetRelatedPerson(id);
        _mockUnitOfWork.Verify(x => x.RelatedPersons.GetRelatedPersons(id), Times.Once);
    }
    [Fact]
    public async Task StatisticsRelatedPersonsType_shouldReturnStatisticsBasedOnTheRelationType()
    {
        List<RelatedPersons> relatedPerson = [
            new RelatedPersons { RelationType = "Friend"},
            new RelatedPersons { RelationType = "Friend"},
            new RelatedPersons { RelationType = "GrandMother"},
            new RelatedPersons { RelationType = "GrandMother"}
        ];

        _mockUnitOfWork.Setup(x => x.RelatedPersons.GetRelatedPersons(It.IsAny<int>())).ReturnsAsync(relatedPerson);
        var statistics = await _service.StatisticsRelatedPersonsType(It.IsAny<int>());
        _mockUnitOfWork.Verify(x => x.RelatedPersons.GetRelatedPersons(It.IsAny<int>()), Times.Once);
        Assert.Equal(2, statistics.Count);
        Assert.Contains(statistics, x => x.RelationType == "Friend" && x.Amount == 2);
        Assert.Contains(statistics, x => x.RelationType == "GrandMother" && x.Amount == 2);
    }
    [Fact]
    public async Task UpdatePersonsBirthDate_ShouldUpdatePersonsBirthDate()
    {
        var birthDate = new DateOnly(2000, 1, 1);
        _mockUnitOfWork.Setup(x => x.Person.UpdateBirthDate(birthDate, It.IsAny<int>())).Verifiable();
        await _service.UpdatePersonsBirthDate(birthDate, It.IsAny<int>());
        _mockUnitOfWork.Verify(x => x.Person.UpdateBirthDate(birthDate, It.IsAny<int>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
    
    [Fact]
    public async Task UpdatePersonsCity_ShouldUpdateConcretePersonsLivingCityWhenValueIsNotNull()
    {
        string city = "Tbilisi";
        _mockUnitOfWork.Setup(x => x.Person.UpdateCity(city, It.IsAny<int>())).Verifiable();
        await _service.UpdatePersonsCity(city, It.IsAny<int>());
        _mockUnitOfWork.Verify(x => x.Person.UpdateCity(city, It.IsAny<int>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
    [Fact]
    public async Task UpdatePersonsGender_ShouldUpdatePersonsGender()
    {
        _mockUnitOfWork.Setup(x => x.Person.UpdateGender(It.IsAny<string>(), It.IsAny<int>())).Verifiable();
        await _service.UpdatePersonsGender(It.IsAny<string>(), It.IsAny<int>());
        _mockUnitOfWork.Verify(x => x.Person.UpdateGender(It.IsAny<string>(), It.IsAny<int>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
    [Fact]
    public async Task UpdatePersonsName_ShouldUpdatePersonsName()
    {
        _mockUnitOfWork.Setup(x => x.Person.UpdateName(It.IsAny<string>(), It.IsAny<int>())).Verifiable();
        await _service.UpdatePersonsName(It.IsAny<string>(), It.IsAny<int>());
        _mockUnitOfWork.Verify(x => x.Person.UpdateName(It.IsAny<string>(), It.IsAny<int>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(),Times.Once);
    }
    [Fact]
    public async Task UpdatePersonsPhoto_ShouldUpdatePersonsPhotoPath()
    {
        string imagePath = "image/path";
        _mockUnitOfWork.Setup(x => x.Person.UpdateImage(imagePath, It.IsAny<int>())).Verifiable();
        await _service.UpdatePersonsPhoto(It.IsAny<int>(), imagePath);
        _mockUnitOfWork.Verify(x => x.Person.UpdateImage(imagePath, It.IsAny<int>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
    [Fact]
    public async Task UpdatePersonsPrivateId_ShouldUpdatePersonsPrivateId()
    {
        string privateId = "11111111111";
        _mockUnitOfWork.Setup(x => x.Person.UpdatePrivateId(privateId, It.IsAny<int>())).Verifiable();
        await _service.UpdatePersonsPrivateId(privateId, It.IsAny<int>());
        _mockUnitOfWork.Verify(x => x.Person.UpdatePrivateId(privateId, It.IsAny<int>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        _mockFileLogService.Verify(x=>x.LogErrorTextFile(privateId), Times.Never);
    }
    [Fact]
    public async Task UpdatePersonsSurname_ShouldUpdatePersonsSurname()
    {
        string surname = "surname";
        _mockUnitOfWork.Setup(x=>x.Person.UpdateSurname(surname, It.IsAny<int>())).Verifiable();
        await _service.UpdatePersonsSurname(surname, It.IsAny<int>());
        _mockUnitOfWork.Verify(x => x.Person.UpdateSurname(surname, It.IsAny<int>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
    [Fact]
    public async Task Paging_ShoudReturnInfoAccordingThePageNumberAndContentAmount()
    {
        List<PersonModel> list = [new PersonModel(), new PersonModel(), new PersonModel(), new PersonModel()];
        var check = await _service.Paging(1, list, 2);
        Assert.Equal(2, check.TotalPages);
        Assert.Equal(2, check.Items.Count());
    }
}