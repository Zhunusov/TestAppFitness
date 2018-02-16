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
using FitnessApp.BLL.Helpers;

namespace FitnessApp.BLL.Services
{
    public class CoachService : ICoachService
    {
        IUnitOfWork Database { get; set; }

        public CoachService (IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(CoachDTO coachDTO)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(coachDTO.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = coachDTO.Email, UserName = coachDTO.Email };
                var result = await Database.UserManager.CreateAsync(user, coachDTO.Password);

                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                await Database.UserManager.AddToRoleAsync(user.Id, coachDTO.Role);

                Coach coach = new Coach
                {
                    Id = user.Id,
                    FirstName = coachDTO.FirstName,
                    LastName = coachDTO.LastName,
                    Patronymic = coachDTO.Patronymic,                   
                    DateOfBirth = coachDTO.DateOfBirth,
                    Sex = coachDTO.Sex,
                    Address = coachDTO.Address,
                    Phone = coachDTO.Phone                                                         
                };

                Database.CoachRepository.Create(coach);
                await Database.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<OperationDetails> Update(CoachDTO coachDTO)
        {
            Coach user = Database.CoachRepository.Get(coachDTO.UserName);
            if (user != null)
            {
                Database.CoachRepository.Update(user);
                user.FirstName = coachDTO.FirstName;
                user.LastName = coachDTO.LastName;
                user.Patronymic = coachDTO.Patronymic;
                user.Address = coachDTO.Address;
                user.DateOfBirth = coachDTO.DateOfBirth;
                user.Sex = coachDTO.Sex;
                user.Phone = coachDTO.Phone;

                await Database.SaveAsync();
                return new OperationDetails(true, "Данные пользователя обновлены", "");
            }
            else
            {
                return new OperationDetails(false, "Пользовательское имя не найдено", "Username");
            }
        }

        public OperationDetails GetPersonalInfo(string username, out CoachDTO coachDTO)
        {
            coachDTO = new CoachDTO();
            Coach user = Database.CoachRepository.Get(username);
            if (user != null)
            {
                string roleID = user.ApplicationUser
                    .Roles
                    .Where(r => r.UserId == user.Id).Select(r => r.RoleId)
                    .FirstOrDefault();

                ApplicationRole role = Database.RoleManager.FindById(roleID);
                if (role != null)
                {
                    coachDTO.Id = user.Id;
                    coachDTO.Email = user.ApplicationUser.Email;
                    coachDTO.FirstName = user.FirstName;
                    coachDTO.LastName = user.LastName;
                    coachDTO.Patronymic = user.Patronymic;
                    coachDTO.Address = user.Address;
                    coachDTO.DateOfBirth = user.DateOfBirth;
                    coachDTO.Sex = user.Sex;
                    coachDTO.Phone = user.Phone;
                    coachDTO.UserName = user.ApplicationUser.UserName;
                    coachDTO.Role = role.Name;                    

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

        public async Task<ICollection<CoachDTO>> GetAll()
        {
            List<CoachDTO> coachesDTO = new List<CoachDTO>();
            var coaches = Database.CoachRepository.GetAll();
            foreach (var coach in coaches)
            {
                CoachDTO coachDTO = new CoachDTO
                {
                    Id = coach.Id,
                    FirstName = coach.FirstName,
                    LastName = coach.LastName,
                    Patronymic = coach.Patronymic,
                    Address = coach.Address,
                    DateOfBirth = coach.DateOfBirth,
                    DateOfBirthString = coach.DateOfBirth != null ? ((DateTime)coach.DateOfBirth).ToShortDateString() : null,
                    Age = Age.GetAgeOfUser(coach.DateOfBirth),
                    Sex = coach.Sex,
                    Phone = coach.Phone,
                    Email = coach.ApplicationUser.Email,
                    UserName = coach.ApplicationUser.UserName,
                    Role = await GetRole(coach.ApplicationUser.UserName)
                };

                var manager = Database.ManagerRepository.Find(_ => _.Id == coach.ManagerId).FirstOrDefault();
                if (manager != null)
                    coachDTO.Manager = $"{manager.LastName} {manager.FirstName} {manager.Patronymic} ({manager.ApplicationUser.UserName})";

                coachesDTO.Add(coachDTO);
            }
            return coachesDTO;
        }

        public async Task<OperationDetails> RemoveUser(string username)
        {
            ApplicationUser user = await Database.UserManager.FindByNameAsync(username);
            if (user != null)
            {
                if (Database.CoachRepository.Delete(username))
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

        public async Task<ICollection<CoachDTO>> GetFilteredCoaches(int? ageParameter, int? genderParameter)
        {
            List<CoachDTO> coachesDTO = new List<CoachDTO>();
           
            if (genderParameter != null && ageParameter != null)
            {
                var genderParameters = dictionaryGenderParameters[(int)genderParameter];
                var ageParameters = dictionaryAgeParameters[(int)ageParameter];

                var coaches = Database.CoachRepository.Find(_=>_.Sex == genderParameters[0] || _.Sex == genderParameters[1]);
                foreach (var coach in coaches)
                {
                    CoachDTO coachDTO = new CoachDTO
                    {
                        Id = coach.Id,
                        FirstName = coach.FirstName,
                        LastName = coach.LastName,
                        Patronymic = coach.Patronymic,
                        Address = coach.Address,
                        DateOfBirth = coach.DateOfBirth,
                        DateOfBirthString = coach.DateOfBirth != null ? ((DateTime)coach.DateOfBirth).ToShortDateString() : null,
                        Age = Age.GetAgeOfUser(coach.DateOfBirth),
                        Sex = coach.Sex,
                        Phone = coach.Phone,
                        Email = coach.ApplicationUser.Email,
                        UserName = coach.ApplicationUser.UserName,
                        Role = await GetRole(coach.ApplicationUser.UserName)
                    };

                    var manager = Database.ManagerRepository.Find(_ => _.Id == coach.ManagerId).FirstOrDefault();
                    if (manager != null)
                        coachDTO.Manager = $"{manager.LastName} {manager.FirstName} {manager.Patronymic} ({manager.ApplicationUser.UserName})";

                    if(coachDTO.Age >= ageParameters[0] && coachDTO.Age <= ageParameters[1])
                        coachesDTO.Add(coachDTO);
                }
                return coachesDTO;
            }
            return null;
        }

        private Dictionary<int, string[]> dictionaryGenderParameters = new Dictionary<int, string[]>
        {
            {0, new string []{"мужчина", "женщина"}},
            {1, new string []{"мужчина", "мужчина"}},
            {2, new string []{"женщина", "женщина"}}
        };

        private Dictionary<int, int[]> dictionaryAgeParameters = new Dictionary<int, int[]>
        {
            {0, new int []{0, 100}},
            {1, new int []{20, 25}},
            {2, new int []{25, 30}},
            {3, new int []{30, 35}},
            {4, new int []{35, 100}},
        };

        public void Dispose()
        {
            Database.Dispose();
        }

    }
}
