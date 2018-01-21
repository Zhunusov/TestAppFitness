using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessApp.BLL.DTO;
using System.Security.Claims;

namespace FitnessApp.BLL.Interfaces
{
    public interface IAdministratorService : IUserService<AdministratorDTO>
    {
        Task<ClaimsIdentity> Authenticate(UserDTO itemDto);
        Task SetInitialData(AdministratorDTO administratorDTO, List<string> roles);
    }
}
