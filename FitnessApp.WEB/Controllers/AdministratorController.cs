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
    [Authorize(Roles = "Administrator")]
    public class AdministratorController : ApiController
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
        public async Task<ICollection<CustomerDTO>> GetAllCustomers()
        {
            var customersDTO = await CustomerService.GetAll();
            return customersDTO;
        }

        [HttpPost]
        public async Task<IHttpActionResult> RegisterCustomer([FromBody]RegisterCustomerModel model)
        {
            if (ModelState.IsValid)
            {
                CustomerDTO сustomerDTO = new CustomerDTO
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Password = model.Password,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Patronymic = model.Patronymic,
                    Role = model.Role,
                    Address = model.Address,
                    DateOfBirth = model.DateOfBirth,
                    Growth = model.Growth,
                    Weight = model.Weight,
                    Phone = model.Phone,
                    Sex = model.Gender,
                };

                OperationDetails operationDetails = await CustomerService.CreateByAdministrator(сustomerDTO);

                if (operationDetails.Succedeed)
                    return Ok();

                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);

            }
            return BadRequest(ModelState);
        }
    }
}
