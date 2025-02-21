using Business_Layer.ServiceRepository.ErrorLogingService;
using Business_Layer.ServiceRepository.PersonServices;
using Business_Layer.ServiceRepository.StorageService;
using DAL.DBSetup.AppDbContextRepo;
using DAL.DBSetup.UnitOfWorkRepo;
using DAL.IGeneralRepository.IPersonModel;
using DAL.IGeneralRepository.IPhoneNumberRepo;
using DAL.IGeneralRepository.IRelatedPersonsRepo;
using DAL.RepositoryImplementation;
using PhysicalPersonsApp.CustomActionFilter;
using PhysicalPersonsApp.CustomMiddleware;
using PhysicalPersonsApp.IGeneralRepository.ICityReferanceRepository;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<CheckRequestFilter>();
});
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<IPersonModel, PersonRepository>();
builder.Services.AddScoped<ICItyAddRepo, CityReferanceRepository>();
builder.Services.AddScoped<IPhoneNumberRepository, PhoneNumberRepository>();
builder.Services.AddScoped<IRelatedPersonsRepository, RelatedPersonsRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IServiceRepo, ServiceRepo>();
builder.Services.AddSingleton<IFileLogService, FileLogService>();
builder.Services.AddSingleton<IUploadImge, UploadImage>();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseMiddleware<LoggingMiddleware>();


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
