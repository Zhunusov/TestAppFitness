using FitenessApp.DAL.Entities;
using FitenessApp.DAL.Identity;
using System;
using System.Threading.Tasks;

namespace FitenessApp.DAL.Interfaces
{
    interface IUnitOfWork
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
