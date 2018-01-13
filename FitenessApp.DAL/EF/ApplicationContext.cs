using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using FitenessApp.DAL.Entities;

namespace FitenessApp.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(string conectionString) 
            : base(conectionString) { }

        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<CustomLoad> CustomLoads { get; set; }
        public DbSet<Load> Loads { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<TrainingTemplate> TrainingTemplates { get; set; }
    }    
}
