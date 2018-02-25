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
using FitnessApp.WEB.Helpers;
using System;

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
            foreach (var _ in coachesDTO)
            {
                _.Avatar = ImageHelper.GetAvatarPath(_.Avatar);
                _.SmallAvatar = ImageHelper.GetAvatarPath(_.SmallAvatar);
            }
            return coachesDTO;
        }

        [HttpPost]
        public async Task<ICollection<CoachDTO>> SearchCoaches([FromBody] SearchCoachModel model)
        {
            if (ModelState.IsValid)
            {
                var coachesDTO = await CoachService.GetFilteredCoaches(model.AgeParameter, model.GenderParameter);
                foreach (var _ in coachesDTO)
                {
                    _.Avatar = ImageHelper.GetAvatarPath(_.Avatar);
                    _.SmallAvatar = ImageHelper.GetAvatarPath(_.SmallAvatar);
                }
                return coachesDTO;
            }
            
            return null;
        }

        [HttpGet]
        public ICollection<TrainingTemplateDTO> GetAllTrainingTemplates()
        {
            var trainingTemplatesDTO = CustomerService.GetAllTrainingTemplates();
            foreach(var _ in trainingTemplatesDTO)
            {
                _.Icon = ImageHelper.GetTrainingTemplateIconPath(_.Icon);
            }
            return trainingTemplatesDTO;
        }

        [HttpPost]
        public async Task<ICollection<TrainingTemplateDTO>> SearchTrainingTemplates([FromBody] SearchTrainingTemplatesModel model)
        {
            if (ModelState.IsValid)
            {
                var trainingTemplatesDTO = await CustomerService.GetFilteredTrainingTemplates(model.AimParameter, model.LevelParameter, model.ForWhomParameter);
                foreach (var _ in trainingTemplatesDTO)
                {
                    _.Icon = ImageHelper.GetTrainingTemplateIconPath(_.Icon);
                }
                return trainingTemplatesDTO;
            }

            return null;
        }

        [HttpPost]
        public async Task<TrainingTemplateDTO> GetTrainingTemplate([FromBody]string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var trainingTemplateDTO = await CustomerService.GetTrainingTemplateById(Guid.Parse(Id));
                if (trainingTemplateDTO != null)
                {
                    trainingTemplateDTO.Icon = ImageHelper.GetTrainingTemplateIconPath(trainingTemplateDTO.Icon);
                    foreach (var _ in trainingTemplateDTO.CustomLoadsDTO)
                    {
                        _.LoadDTO.Icon = ImageHelper.GetTrainingTemplateIconPath(_.LoadDTO.Icon);
                    }
                    return trainingTemplateDTO;
                }
            }            

            return null;            
        }

    }
}
