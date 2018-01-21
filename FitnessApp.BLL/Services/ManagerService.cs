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

namespace FitnessApp.BLL.Services
{
    public class ManagerService : IManagerService
    {
        IUnitOfWork Database { get; set; }

        public ManagerService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task<OperationDetails> Create(ManagerDTO managerDTO)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(managerDTO.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = managerDTO.Email, UserName = managerDTO.Email };
                var result = await Database.UserManager.CreateAsync(user, managerDTO.Password);

                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                await Database.UserManager.AddToRoleAsync(user.Id, managerDTO.Role);

                Manager manager = new Manager
                {
                    Id = user.Id,
                    FirstName = managerDTO.FirstName,
                    LastName = managerDTO.LastName,
                    Patronymic = managerDTO.Patronymic,
                    DateOfBirth = managerDTO.DateOfBirth,                    
                };

                Database.ManagerRepository.Create(manager);
                await Database.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }
        public async Task<OperationDetails> Update(ManagerDTO managerDTO)
        {
            Manager user = Database.ManagerRepository.Get(managerDTO.UserName);
            if (user != null)
            {
                Database.ManagerRepository.Update(user);
                user.FirstName = managerDTO.FirstName;
                user.LastName = managerDTO.LastName;
                user.Patronymic = managerDTO.Patronymic;
                user.Address = managerDTO.Address;
                user.DateOfBirth = managerDTO.DateOfBirth;               
                user.ApplicationUser.PhoneNumber = managerDTO.Phone;

                await Database.SaveAsync();
                return new OperationDetails(true, "Данные пользователя обновлены", "");
            }
            else
            {
                return new OperationDetails(false, "Пользовательское имя не найдено", "Username");
            }
        }
        public OperationDetails GetPersonalInfo(string username, out ManagerDTO managerDTO)
        {
            managerDTO = new ManagerDTO();
            Manager user = Database.ManagerRepository.Get(username);
            if (user != null)
            {
                string roleID = user.ApplicationUser
                    .Roles
                    .Where(r => r.UserId == user.Id).Select(r => r.RoleId)
                    .FirstOrDefault();

                ApplicationRole role = Database.RoleManager.FindById(roleID);
                if (role != null)
                {
                    managerDTO.Id = user.Id;
                    managerDTO.Email = user.ApplicationUser.Email;
                    managerDTO.FirstName = user.FirstName;
                    managerDTO.LastName = user.LastName;
                    managerDTO.Patronymic = user.Patronymic;
                    managerDTO.Address = user.Address;
                    managerDTO.DateOfBirth = user.DateOfBirth;
                    managerDTO.Phone = user.ApplicationUser.PhoneNumber;
                    managerDTO.UserName = user.ApplicationUser.UserName;
                    managerDTO.Role = role.Name;

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
        public async Task<ICollection<ManagerDTO>> GetAll()
        {
            List<ManagerDTO> managersDTO = new List<ManagerDTO>();
            var managers = Database.ManagerRepository.GetAll();
            foreach (var manager in managers)
            {
                ManagerDTO managerDTO = new ManagerDTO
                {
                    Id = manager.Id,
                    FirstName = manager.FirstName,
                    LastName = manager.LastName,
                    Patronymic = manager.Patronymic,
                    Address = manager.Address,
                    DateOfBirth = manager.DateOfBirth,                    
                    Phone = manager.ApplicationUser.PhoneNumber,
                    Email = manager.ApplicationUser.Email,
                    UserName = manager.ApplicationUser.UserName,
                    Role = await GetRole(manager.ApplicationUser.UserName)
                };
                managersDTO.Add(managerDTO);
            }
            return managersDTO;
        }
        public async Task<OperationDetails> RemoveUser(string username)
        {
            ApplicationUser user = await Database.UserManager.FindByNameAsync(username);
            if (user != null)
            {
                if (Database.ManagerRepository.Delete(username))
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
