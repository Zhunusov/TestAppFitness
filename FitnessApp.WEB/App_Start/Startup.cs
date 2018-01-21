using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using FitnessApp.BLL.Services;
using Microsoft.AspNet.Identity;
using FitnessApp.BLL.Interfaces;
using FitnessApp.BLL.DTO;

[assembly: OwinStartup(typeof(FitnessApp.WEB.App_Start.Startup))]

namespace FitnessApp.WEB.App_Start
{
    public class Startup
    {
        IServiceCreator serviceCreator = new ServiceCreator();
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(CreateAdministratorService);            
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Index"),
                CookieName = ".fitness",
            });
        }

        private IAdministratorService CreateAdministratorService()
        {
            return serviceCreator.CreateAdministratorService("Fitness");
        }
       
    }
}