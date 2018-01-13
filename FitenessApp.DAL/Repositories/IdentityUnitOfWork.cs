using FitenessApp.DAL.EF;
using FitenessApp.DAL.Entities;
using FitenessApp.DAL.Interfaces;
using FitenessApp.DAL.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace FitenessApp.DAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private AdministratorRepository administratorRepository;
        private CoachRepository coachRepository;
        private CustomerRepository customerRepository;
        private ManagerRepository managerRepository;
        private CustomLoadRepository customLoadRepository;
        private ImageRepository imageRepository;
        private LoadRepository loadRepository;
        private TrainingRepository trainingRepository;
        private TrainingTemplateRepository trainingTemplateRepository;

        public IdentityUnitOfWork(string connectionString)
        {
            db = new ApplicationContext(connectionString);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                if (userManager == null)
                    userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));

                return userManager;
            }
        }
        public ApplicationRoleManager RoleManager
        {
            get
            {
                if (roleManager == null)
                    roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));

                return roleManager;
            }
        }

        public IUserManager<Administrator> AdministratorRepository
        {
            get
            {
                if (administratorRepository == null)
                    administratorRepository = new AdministratorRepository(db);

                return administratorRepository;
            }
        }
        public IUserManager<Coach> CoachRepository
        {
            get
            {
                if (coachRepository == null)
                    coachRepository = new CoachRepository(db);

                return coachRepository;
            }
        }
        public IUserManager<Customer> CustomerRepository
        {
            get
            {
                if (customerRepository == null)
                    customerRepository = new CustomerRepository(db);

                return customerRepository;
            }
        }
        public IUserManager<Manager> ManagerRepository
        {
            get
            {
                if (managerRepository == null)
                    managerRepository = new ManagerRepository(db);

                return managerRepository;
            }
        }

        public ICustomLoadRepository CustomLoadRepository
        {
            get
            {
                if (customLoadRepository == null)
                    customLoadRepository = new CustomLoadRepository(db);

                return customLoadRepository;
            }
        }
        public IImageRepository ImageRepository
        {
            get
            {
                if (imageRepository == null)
                    imageRepository = new ImageRepository(db);

                return imageRepository;
            }
        }
        public ILoadRepository LoadRepository
        {
            get
            {
                if (loadRepository == null)
                    loadRepository = new LoadRepository(db);

                return loadRepository;
            }
        }
        public ITrainingRepository TrainingRepository
        {
            get
            {
                if (trainingRepository == null)
                    trainingRepository = new TrainingRepository(db);

                return trainingRepository;
            }
        }
        public ITrainingTemplateRepository TrainingTemplateRepository
        {
            get
            {
                if (trainingTemplateRepository == null)
                    trainingTemplateRepository = new TrainingTemplateRepository(db);

                return trainingTemplateRepository;
            }
        }
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
