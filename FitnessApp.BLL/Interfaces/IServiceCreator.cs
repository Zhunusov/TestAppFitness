using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.BLL.Interfaces
{
    public interface IServiceCreator
    {
        IAdministratorService CreateAdministratorService(string connection);
        ICoachService CreateCoachService(string connection);
        ICustomerService CreateCustomerService(string connection);
        IManagerService CreateManagerService(string connection);
    }
}
