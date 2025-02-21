using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared_Layer.Models;
using Shared_Layer.Models.AdditionalModels;

namespace DAL.DBSetup.AppDbContextRepo;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectToDataBase.GetConnectionString());
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonModel>(model =>
        {
            model.Property(x => x.City).IsRequired(false);
            model.Property(x => x.Gender).IsRequired(false);
            model.Property(x => x.ImagePath).IsRequired(false);
            model.Property(x => x.PhoneNumberId).IsRequired(false);
            model.Property(x => x.RelatedPersonsId).IsRequired(false);
        });
    }
    public DbSet<PersonModel> PersonModel { get; set; }
    public DbSet<CityReferance> CityReferance { get; set; }
    public DbSet<PhoneNumber> PhoneNumber { get; set; }
    public DbSet<RelatedPersons> RelatedPersons { get; set; }
}
