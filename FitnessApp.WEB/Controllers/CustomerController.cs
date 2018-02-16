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
    [Authorize(Roles = "Administrator, Customer")]
    public class CustomerController : ApiController
    {
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

        [HttpGet]
        public async Task<ICollection<CoachDTO>> GetAllCoaches()
        {
            var coachesDTO = await CoachService.GetAll();
            return coachesDTO;
        }

        [HttpPost]
        public async Task<ICollection<CoachDTO>> SearchCoaches([FromBody] SearchCoachModel model)
        {
            if (ModelState.IsValid)
            {
                var coachesDTO = await CoachService.GetFilteredCoaches(model.AgeParameter, model.GenderParameter);
                return coachesDTO;
            }
            
            return null;
        }

    }
}
