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


namespace FitnessApp.WEB.Controllers
{
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
    }
}
