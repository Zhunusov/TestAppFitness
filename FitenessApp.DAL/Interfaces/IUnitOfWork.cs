using FitnessApp.DAL.Entities;
using FitnessApp.DAL.Identity;
using System;
using System.Threading.Tasks;

namespace FitnessApp.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IUserManager<Administrator> AdministratorRepository { get; }
        IUserManager<Coach> CoachRepository { get; }
        IUserManager<Customer> CustomerRepository { get; }
        IUserManager<Manager> ManagerRepository { get; }
        ICustomLoadRepository CustomLoadRepository { get; }
        IImageRepository ImageRepository { get; }
        ILoadRepository LoadRepository { get; }
        ITrainingRepository TrainingRepository { get; }
        ITrainingTemplateRepository TrainingTemplateRepository { get; }
        Task SaveAsync();
    }
}
