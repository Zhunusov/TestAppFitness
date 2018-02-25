using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessApp.BLL.Interfaces;
using FitnessApp.BLL.DTO;
using FitnessApp.BLL.Infrastructure;
using FitnessApp.BLL.Helpers;
using FitnessApp.DAL.Interfaces;
using FitnessApp.DAL.Entities;
using Microsoft.AspNet.Identity;

namespace FitnessApp.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        IUnitOfWork Database { get; set; }

        public CustomerService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(CustomerDTO customerDTO)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(customerDTO.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = customerDTO.Email, UserName = customerDTO.Email };
                var result = await Database.UserManager.CreateAsync(user, customerDTO.Password);

                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                await Database.UserManager.AddToRoleAsync(user.Id, customerDTO.Role);

                Customer customer = new Customer
                {
                    Id = user.Id,
                    FirstName = customerDTO.FirstName,
                    LastName = customerDTO.LastName,
                    Patronymic = customerDTO.Patronymic,                    
                    DateOfBirth = customerDTO.DateOfBirth,
                    Sex = customerDTO.Sex,
                    Address = customerDTO.Address,
                    Growth = customerDTO.Growth,
                    Weight = customerDTO.Weight,
                    Phone = customerDTO.Phone
                };

                Database.CustomerRepository.Create(customer);
                await Database.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<OperationDetails> CreateByAdministrator(CustomerDTO customerDTO)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(customerDTO.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = customerDTO.Email, UserName = customerDTO.Email };
                var result = await Database.UserManager.CreateAsync(user, customerDTO.Password);

                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                await Database.UserManager.AddToRoleAsync(user.Id, customerDTO.Role);

                Customer customer = new Customer
                {
                    Id = user.Id,
                    FirstName = customerDTO.FirstName,
                    LastName = customerDTO.LastName,
                    Patronymic = customerDTO.Patronymic,
                    DateOfBirth = customerDTO.DateOfBirth,
                    Sex = customerDTO.Sex,
                    Address = customerDTO.Address,
                    Growth = customerDTO.Growth,
                    Weight = customerDTO.Weight,
                    Phone = customerDTO.Phone
                };
                Database.CustomerRepository.Create(customer);
                await Database.SaveAsync();

                customer = Database.CustomerRepository.Get(customerDTO.UserName);
                if(user != null)
                {
                    Database.CustomerRepository.Update(customer);
                    customer.Phone = customerDTO.Phone;
                    await Database.SaveAsync();
                }

                return new OperationDetails(true, "Регистрация успешно осуществлена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }
        public async Task<OperationDetails> Update(CustomerDTO customerDTO)
        {
            Customer user = Database.CustomerRepository.Get(customerDTO.UserName);
            if (user != null)
            {
                Database.CustomerRepository.Update(user);
                user.FirstName = customerDTO.FirstName;
                user.LastName = customerDTO.LastName;
                user.Patronymic = customerDTO.Patronymic;
                user.Address = customerDTO.Address;
                user.DateOfBirth = customerDTO.DateOfBirth;
                user.Sex = customerDTO.Sex;
                user.Growth = customerDTO.Growth;
                user.Weight = customerDTO.Weight;
                user.Phone = customerDTO.Phone;

                await Database.SaveAsync();
                return new OperationDetails(true, "Данные пользователя обновлены", "");
            }
            else
            {
                return new OperationDetails(false, "Пользовательское имя не найдено", "Username");
            }
        }
        public OperationDetails GetPersonalInfo(string username, out CustomerDTO customerDTO)
        {
            customerDTO = new CustomerDTO();
            Customer user = Database.CustomerRepository.Get(username);
            if (user != null)
            {
                string roleID = user.ApplicationUser
                    .Roles
                    .Where(r => r.UserId == user.Id).Select(r => r.RoleId)
                    .FirstOrDefault();

                ApplicationRole role = Database.RoleManager.FindById(roleID);
                if (role != null)
                {
                    customerDTO.Id = user.Id;
                    customerDTO.Email = user.ApplicationUser.Email;
                    customerDTO.FirstName = user.FirstName;
                    customerDTO.LastName = user.LastName;
                    customerDTO.Patronymic = user.Patronymic;
                    customerDTO.Address = user.Address;
                    customerDTO.DateOfBirth = user.DateOfBirth;
                    customerDTO.DateOfBirthString = user.DateOfBirth != null ? ((DateTime)user.DateOfBirth).ToShortDateString() : null;
                    customerDTO.Sex = user.Sex;
                    customerDTO.Phone = user.Phone;
                    customerDTO.Growth = user.Growth;
                    customerDTO.Weight = user.Weight;
                    customerDTO.UserName = user.ApplicationUser.UserName;
                    customerDTO.Role = role.Name;

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
        public async Task<ICollection<CustomerDTO>> GetAll()
        {
            List<CustomerDTO> customersDTO = new List<CustomerDTO>();
            var customers = Database.CustomerRepository.GetAll();
            foreach (var customer in customers)
            {
                CustomerDTO customerDTO = new CustomerDTO
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Patronymic = customer.Patronymic,
                    Address = customer.Address,
                    DateOfBirth = customer.DateOfBirth,
                    DateOfBirthString = customer.DateOfBirth != null ? ((DateTime)customer.DateOfBirth).ToShortDateString() : null,
                    Age = Age.GetAgeOfUser(customer.DateOfBirth),
                    Sex = customer.Sex,
                    Phone = customer.Phone,
                    Email = customer.ApplicationUser.Email,
                    Growth = customer.Growth,
                    Weight = customer.Weight,
                    UserName = customer.ApplicationUser.UserName,
                    Role = await GetRole(customer.ApplicationUser.UserName)
                };
                customersDTO.Add(customerDTO);
            }
            return customersDTO;
        }

        public async Task<OperationDetails> RemoveUser(string username)
        {
            ApplicationUser user = await Database.UserManager.FindByNameAsync(username);
            if (user != null)
            {
                if (Database.CustomerRepository.Delete(username))
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

        public ICollection<TrainingTemplateDTO> GetAllTrainingTemplates()
        {
            List<TrainingTemplateDTO> trainingTemplatesDTO = new List<TrainingTemplateDTO>();
            var trainingTemplates = Database.TrainingTemplateRepository.GetAll();
            foreach(var trainingTemplate in trainingTemplates)
            {
                TrainingTemplateDTO trainingTemplateDTO = new TrainingTemplateDTO();
                var customLoads = Database.CustomLoadRepository.Find(_ => _.TrainingTemplateId == trainingTemplate.Id);
                foreach (var customLoad in customLoads)
                {
                    var load = Database.LoadRepository.Get(customLoad.LoadName);
                    LoadDTO loadDTO = new LoadDTO()
                    {
                         Title =load.Title,
                         Description = load.Description,
                         Name = load.Name,
                         Iteration = load.Iteration,
                         Series = load.Series,
                         Icon = load.Icon
                    };

                    CustomLoadDTO customLoadDTO = new CustomLoadDTO()
                    {
                        Id = customLoad.Id,
                        CustomIteration = customLoad.CustomIteration,
                        CustomSeries = customLoad.CustomSeries,
                        LoadDTO = loadDTO
                    };

                    trainingTemplateDTO.CustomLoadsDTO.Add(customLoadDTO);
                }

                trainingTemplateDTO.Aim = trainingTemplate.Aim;
                trainingTemplateDTO.Complexity = trainingTemplate.Complexity;
                trainingTemplateDTO.DaysPerWeek = trainingTemplate.DaysPerWeek;
                trainingTemplateDTO.Description = trainingTemplate.Description;
                trainingTemplateDTO.GenderCategory = trainingTemplate.GenderCategory;
                trainingTemplateDTO.Icon = trainingTemplate.Icon;
                trainingTemplateDTO.Id = trainingTemplate.Id;
                trainingTemplateDTO.Name = trainingTemplate.Name;
                trainingTemplateDTO.TrainingDuration = trainingTemplate.TrainingDuration;
                trainingTemplateDTO.WeekDuration = trainingTemplate.WeekDuration;  

                trainingTemplatesDTO.Add(trainingTemplateDTO);
            }

            return trainingTemplatesDTO;
        }

        public Task<List<TrainingTemplateDTO>> GetFilteredTrainingTemplates(string aimParameter, string levelParameter, string forWhomParameter)
        {
            List<TrainingTemplateDTO> trainingTemplatesDTO = new List<TrainingTemplateDTO>();

            return Task.Run(() =>
            {
                var trainingTemplates = Database.TrainingTemplateRepository
                    .Find(_ => 
                    (string.IsNullOrEmpty(aimParameter) || (aimParameter != null ? _.Aim == aimParameter : false)) &&
                    (string.IsNullOrEmpty(levelParameter) || (levelParameter != null ? _.Complexity == levelParameter : false)) &&
                    (string.IsNullOrEmpty(forWhomParameter) || 
                        (forWhomParameter == "Женщины" ? _.GenderCategory == forWhomParameter || _.GenderCategory == "Для всех" : false) ||
                        (forWhomParameter == "Мужчины" ? _.GenderCategory == forWhomParameter || _.GenderCategory == "Для всех" : false) ||
                        (forWhomParameter != null ? _.GenderCategory == forWhomParameter : false)));

                foreach (var trainingTemplate in trainingTemplates)
                {
                    TrainingTemplateDTO trainingTemplateDTO = new TrainingTemplateDTO();
                    var customLoads = Database.CustomLoadRepository.Find(_ => _.TrainingTemplateId == trainingTemplate.Id);
                    foreach (var customLoad in customLoads)
                    {
                        var load = Database.LoadRepository.Get(customLoad.LoadName);
                        LoadDTO loadDTO = new LoadDTO()
                        {
                            Title = load.Title,
                            Description = load.Description,
                            Name = load.Name,
                            Iteration = load.Iteration,
                            Series = load.Series,
                            Icon = load.Icon
                        };

                        CustomLoadDTO customLoadDTO = new CustomLoadDTO()
                        {
                            Id = customLoad.Id,
                            CustomIteration = customLoad.CustomIteration,
                            CustomSeries = customLoad.CustomSeries,
                            LoadDTO = loadDTO
                        };

                        trainingTemplateDTO.CustomLoadsDTO.Add(customLoadDTO);
                    }

                    trainingTemplateDTO.Aim = trainingTemplate.Aim;
                    trainingTemplateDTO.Complexity = trainingTemplate.Complexity;
                    trainingTemplateDTO.DaysPerWeek = trainingTemplate.DaysPerWeek;
                    trainingTemplateDTO.Description = trainingTemplate.Description;
                    trainingTemplateDTO.GenderCategory = trainingTemplate.GenderCategory;
                    trainingTemplateDTO.Icon = trainingTemplate.Icon;
                    trainingTemplateDTO.Id = trainingTemplate.Id;
                    trainingTemplateDTO.Name = trainingTemplate.Name;
                    trainingTemplateDTO.TrainingDuration = trainingTemplate.TrainingDuration;
                    trainingTemplateDTO.WeekDuration = trainingTemplate.WeekDuration;

                    trainingTemplatesDTO.Add(trainingTemplateDTO);
                }

                return trainingTemplatesDTO;
            });
         
        }

        public Task<TrainingTemplateDTO> GetTrainingTemplateById(Guid? Id)
        {
            return Task.Run(() =>
            {
                if (Id == null)
                    return null;

                var trainingTemplate = Database.TrainingTemplateRepository.Get((Guid)Id);
                if(trainingTemplate != null)
                {
                    TrainingTemplateDTO trainingTemplateDTO = new TrainingTemplateDTO();
                    var customLoads = Database.CustomLoadRepository.Find(_ => _.TrainingTemplateId == trainingTemplate.Id);
                    foreach (var customLoad in customLoads)
                    {
                        var load = Database.LoadRepository.Get(customLoad.LoadName);
                        LoadDTO loadDTO = new LoadDTO()
                        {
                            Title = load.Title,
                            Description = load.Description,
                            Name = load.Name,
                            Iteration = load.Iteration,
                            Series = load.Series,
                            Icon = load.Icon
                        };

                        CustomLoadDTO customLoadDTO = new CustomLoadDTO()
                        {
                            Id = customLoad.Id,
                            CustomIteration = customLoad.CustomIteration,
                            CustomSeries = customLoad.CustomSeries,
                            LoadDTO = loadDTO
                        };

                        trainingTemplateDTO.CustomLoadsDTO.Add(customLoadDTO);
                    }

                    trainingTemplateDTO.Aim = trainingTemplate.Aim;
                    trainingTemplateDTO.Complexity = trainingTemplate.Complexity;
                    trainingTemplateDTO.DaysPerWeek = trainingTemplate.DaysPerWeek;
                    trainingTemplateDTO.Description = trainingTemplate.Description;
                    trainingTemplateDTO.GenderCategory = trainingTemplate.GenderCategory;
                    trainingTemplateDTO.Icon = trainingTemplate.Icon;
                    trainingTemplateDTO.Id = trainingTemplate.Id;
                    trainingTemplateDTO.Name = trainingTemplate.Name;
                    trainingTemplateDTO.TrainingDuration = trainingTemplate.TrainingDuration;
                    trainingTemplateDTO.WeekDuration = trainingTemplate.WeekDuration;

                    return trainingTemplateDTO;
                }
                return null;
            });
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
