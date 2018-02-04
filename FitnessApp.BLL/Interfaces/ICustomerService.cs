using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessApp.BLL.DTO;
using FitnessApp.BLL.Infrastructure;

namespace FitnessApp.BLL.Interfaces
{
    public interface ICustomerService : IUserService<CustomerDTO>
    {
        Task<OperationDetails> CreateByAdministrator(CustomerDTO itemDto);
    }
}
