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
    }
}
