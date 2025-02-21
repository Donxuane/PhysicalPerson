using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PhysicalPersonsApp.Models;
using PhysicalPersonsApp.ViewModel;
using Business_Layer.ServiceRepository.PersonServices;
using Business_Layer.ServiceRepository.StorageService;
using Shared_Layer.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
namespace PhysicalPersonsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceRepo service;
        private readonly ILogger<HomeController> _logger;
        private readonly IUploadImge _image;
        private List<PersonModel> _list;
        public HomeController(IServiceRepo service, ILogger<HomeController> logger, IUploadImge image)
        {
            _logger = logger;
            this.service = service;
            _image = image;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> FastSearch(string criteria)
        //{
        //    var list = await service.FastSearch(criteria);
        //    return View("Index", list);
        //}
        [HttpGet]
        public async Task FastSearch(string criteria)
        {
            _list = await service.FastSearch(criteria);
        }
        [HttpGet]
        public async Task<IActionResult> Paging(string criteria, int page)
        {
            var listMain = await service.FastSearch(criteria);
            var list = await service.Paging(page,listMain,2);
            var model = new IndexViewModel
            {
                list = list.Items.ToList(),
                PageAmount = list.TotalPages
            };
            return View("Index", model);
        }
        [HttpPost]
        public async Task<IActionResult> DeletePerson(int personId)
        {
            await service.DeletePerson(personId);
            return View("Index");
        }
        [HttpGet]
        public async Task<IActionResult> ViewDetailedInfo(int personId)
        {
            var person = await service.GetPersonById(personId);
            var relatedPerson = await service.GetRelatedPerson((int)person.RelatedPersonsId);
            var phoneNumber = await service.GetPhoneNumberList(person);
            var relatedPersoStatistics = await service.StatisticsRelatedPersonsType((int)person.RelatedPersonsId);
            return View("Details", new DetailsViewModel() { Person = person, PhoneNumbers = phoneNumber, Related
            = relatedPerson, Statistics = relatedPersoStatistics});
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRelatedPerson(int personId, int relatedPersonId)
        {
            await service.DeletePersonsRelatedPerson(relatedPersonId);
            return await ViewDetailedInfo(personId);
        }
        public IActionResult AddPerson()
        {
            return View(new AddViewModels());
        }

        [HttpPost]
        public async Task<IActionResult> AddPersonDb(AddViewModels models)
        {
            if (!ModelState.IsValid)
            {
                return View("AddPerson", models);
            }
            else
            {
                if (models.Phone != null)
                {
                    await service.AddPhoneNumber(models.person, models.Phone);
                }
                if (models.Related != null)
                {
                    await service.AddPersonsRelatedPerson(models.person, models.Related);
                }
                if (models.file != null)
                {
                    await _image.UploadImageService(models.file);
                    var image = await _image.GetImagePath();
                    models.person.ImagePath = image;
                }
                else
                {
                    models.person.ImagePath = null;
                }
                await service.AddNewPerson(models.person);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDetails(UpdateDetailsViewModel model)
        {
            if (!model.Name.IsNullOrEmpty())
            {
                await service.UpdatePersonsName(model.Name, model.Id);
            }
            if (!model.Surname.IsNullOrEmpty())
            {
                await service.UpdatePersonsSurname(model.Surname, model.Id);
            }
            if (model.Image != null)
            {
                await _image.UploadImageService(model.Image);
                var image = await _image.GetImagePath();
                await service.UpdatePersonsPhoto(model.Id, image);
            }
            if (model.RelatedPersons != null)
            {
                await service.AddMoreReltedPersons((int)model.RelatedPersonId, model.RelatedPersons);
            }
            var action = await ViewDetailedInfo(model.Id);
            return action;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
