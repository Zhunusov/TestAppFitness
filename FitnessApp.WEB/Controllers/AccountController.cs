using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using FitnessApp.BLL.DTO;
using FitnessApp.BLL.Infrastructure;
using FitnessApp.BLL.Interfaces;
using FitnessApp.WEB.Models;
using FitnessApp.WEB.Filters;


namespace FitnessApp.WEB.Controllers
{
    [Culture]
    [Authorize(Roles = "Administrator, Manager, Customer, Coach")]
    public class AccountController : ApiController
    {        
        private IAdministratorService AdministratorService
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<IAdministratorService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return Request.GetOwinContext().Authentication;
            }
        }

        private ICustomerService CustomerService
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<ICustomerService>();
            }
        }

        private ICoachService CoachService
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<ICoachService>();
            }
        }

        private IManagerService ManagerService
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<IManagerService>();
            }
        }

        [HttpGet]
        public IHttpActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Login([FromBody]LoginModel model)
        {            
            if (ModelState.IsValid)
            {
                await SetInitialDataAsync();
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await AdministratorService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("Account.login", "Неверный логин или пароль.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return Ok();
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<string> GetUserRole()
        {
            return await AdministratorService.GetRole(User.Identity.Name);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Register([FromBody]RegisterModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                switch (model.Role)
                {
                    case "Customer":
                        return await CustomerRegistration(model);

                    case "Coach":
                        break;

                    case "Manager":
                        break;

                    default:
                        ModelState.AddModelError("Account.register", "Ошибка регистрации. Отсутствует регистрируемая роль.");
                        return BadRequest(ModelState);
                }

            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<UserDTO> GetUserInfo()
        {
            string role = await AdministratorService.GetRole(User.Identity.Name);
            if (role != null)
            {
                switch (role)
                {
                    case "Customer":
                        CustomerDTO customerDTO = new CustomerDTO();
                        OperationDetails opCustomer = CustomerService.GetPersonalInfo(User.Identity.Name, out customerDTO);
                        if (opCustomer.Succedeed)
                        {
                            return customerDTO;
                        }
                        break;

                    case "Administrator":
                        AdministratorDTO administratorDTO = new AdministratorDTO();
                        OperationDetails opAdministrator = AdministratorService.GetPersonalInfo(User.Identity.Name, out administratorDTO);
                        if (opAdministrator.Succedeed)
                        {
                            return administratorDTO;
                        }
                        break;

                    default: break;
                }
            }
            return null;
        }

        private async Task SetInitialDataAsync()
        {
            await AdministratorService.SetInitialData(new AdministratorDTO
            {
                Email = "administrator@test.com",
                UserName = "administrator@test.com",
                Password = "123456789",
                FirstName = "Артур",
                LastName = "Жунусов",
                Patronymic = "Кажиканович",
                Role = "Administrator",
            }, new List<string> { "Administrator", "Manager", "Customer", "Coach" });
        }

        private async Task<IHttpActionResult> CustomerRegistration(RegisterModel model)
        {
            CustomerDTO сustomerDTO = new CustomerDTO
            {
                Email = model.Email,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Patronymic = model.Patronymic,                
                Role = model.Role
            };

            OperationDetails operationDetails = await CustomerService.Create(сustomerDTO);

            if (operationDetails.Succedeed)
            {
                ClaimsIdentity claim = await AdministratorService.Authenticate(сustomerDTO);
                AuthenticationManager.SignOut();
                AuthenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = true
                }, claim);
                return Ok();
            }
            else
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);

            return BadRequest(ModelState);
        }

    }
}
