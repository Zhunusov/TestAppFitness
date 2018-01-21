using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessApp.BLL.Interfaces;
using FitnessApp.BLL.DTO;
using FitnessApp.BLL.Infrastructure;
using FitnessApp.DAL.Interfaces;
using FitnessApp.DAL.Entities;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace FitnessApp.BLL.Services
{
    public class AdministratorService : IAdministratorService
    {
        IUnitOfWork Database { get; set; }

        public AdministratorService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(AdministratorDTO adminDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(adminDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = adminDto.Email, UserName = adminDto.Email };
                var result = await Database.UserManager.CreateAsync(user, adminDto.Password);

                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                await Database.UserManager.AddToRoleAsync(user.Id, adminDto.Role);

                Administrator admin = new Administrator
                {
                    Id = user.Id,
                    FirstName = adminDto.FirstName,
                    LastName = adminDto.LastName,
                    Patronymic = adminDto.Patronymic                     
                };

                Database.AdministratorRepository.Create(admin);
                await Database.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<OperationDetails> Update(AdministratorDTO adminDto)
        {
            Administrator user = Database.AdministratorRepository.Get(adminDto.UserName);
            if (user != null)
            {
                Database.AdministratorRepository.Update(user);
                user.FirstName = adminDto.FirstName;
                user.LastName = adminDto.LastName;
                user.Patronymic = adminDto.Patronymic;

                await Database.SaveAsync();
                return new OperationDetails(true, "Данные пользователя обновлены", "");
            }
            else
            {
                return new OperationDetails(false, "Пользовательское имя не найдено", "Username");
            }
        }

        public OperationDetails GetPersonalInfo(string username, out AdministratorDTO adminDto)
        {
            adminDto = new AdministratorDTO();
            Administrator user = Database.AdministratorRepository.Get(username);
            if (user != null)
            {
                string roleID = user.ApplicationUser
                    .Roles
                    .Where(r => r.UserId == user.Id).Select(r => r.RoleId)
                    .FirstOrDefault();

                ApplicationRole role = Database.RoleManager.FindById(roleID);
                if (role != null)
                {
                    adminDto.Id = user.Id;
                    adminDto.Email = user.ApplicationUser.Email;
                    adminDto.FirstName = user.FirstName;
                    adminDto.LastName = user.LastName;
                    adminDto.Patronymic = user.Patronymic;
                    adminDto.UserName = user.ApplicationUser.UserName;
                    adminDto.Role = role.Name;                                    

                    return new OperationDetails(true, "Поиск пользователя произведен успешно", "");
                }
                else
                {
                    return new OperationDetails(false, "Пользовательская роль не найдена", "Role");
                }
            }
            else
            {
                return new OperationDetails(false, "Пользовательское имя не найдено", "Username");
            }
        }

        public async Task<string> GetRole(string username)
        {
            ApplicationUser user = await Database.UserManager.FindByNameAsync(username);
            if (user != null)
            {
                string roleID = user.Roles
                    .Where(r => r.UserId == user.Id)
                    .Select(r => r.RoleId)
                    .FirstOrDefault();

                ApplicationRole role = await Database.RoleManager.FindByIdAsync(roleID);
                if (role != null)
                    return role.Name;

            }
            return null;
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);

            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public async Task SetInitialData(AdministratorDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public async Task<ICollection<AdministratorDTO>> GetAll()
        {
            List<AdministratorDTO> adminsDto = new List<AdministratorDTO>();
            var admins = Database.AdministratorRepository.GetAll();
            foreach (var admin in admins)
            {
                AdministratorDTO admDto = new AdministratorDTO
                {
                    Id = admin.Id,
                    FirstName = admin.FirstName,
                    LastName = admin.LastName,
                    Patronymic = admin.Patronymic,
                    Email = admin.ApplicationUser.Email,
                    UserName = admin.ApplicationUser.UserName,
                    Role = await GetRole(admin.ApplicationUser.UserName)
                };
                adminsDto.Add(admDto);
            }
            return adminsDto;
        }

        public async Task<OperationDetails> RemoveUser(string username)
        {
            ApplicationUser user = await Database.UserManager.FindByNameAsync(username);
            if (user != null)
            {
                if (Database.AdministratorRepository.Delete(username))
                {
                    var result = Database.UserManager.Delete(user);

                    if (result.Errors.Count() > 0)
                        return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                    await Database.SaveAsync();
                    return new OperationDetails(true, "", "");
                }
            }
            return new OperationDetails(false, "Пользователь не найден", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
