using FitnessApp.BLL.Interfaces;
using FitnessApp.DAL.Repositories;

namespace FitnessApp.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IAdministratorService CreateAdministratorService(string connection)
        {
            return new AdministratorService(new IdentityUnitOfWork(connection));
        }

        public ICoachService CreateCoachService(string connection)
        {
            return new CoachService(new IdentityUnitOfWork(connection));
        }
        public ICustomerService CreateCustomerService(string connection)
        {
            return new CustomerService(new IdentityUnitOfWork(connection));
        }
        public IManagerService CreateManagerService(string connection)
        {
            return new ManagerService(new IdentityUnitOfWork(connection));
        }
    }
}
