using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FitnessApp.BLL.Interfaces;
using FitnessApp.WEB.Filters;

namespace FitnessApp.WEB.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        private IAdministratorService AdministratorService
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<IAdministratorService>();
            }
        }
        public async Task<ActionResult> Index()
        {
            if (Request.IsAuthenticated)
            {
                string role = await AdministratorService.GetRole(User.Identity.Name);
                if (role != null)
                {
                    switch (role)
                    {
                        case "Manager":
                            return RedirectToAction("ManagerArea");

                        case "Customer":
                            return RedirectToAction("CustomerArea");

                        case "Coach":
                            return RedirectToAction("CoachArea");

                        case "Administrator":
                            return RedirectToAction("AdministratorArea");

                        default: break;
                    }
                }
            }
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult AdministratorArea()
        {
            return View();
        }

        [Authorize(Roles = "Manager")]
        public ActionResult ManagerArea()
        {
            return View();
        }

        [Authorize(Roles = "Customer")]
        public ActionResult CustomerArea()
        {
            return View();
        }

        [Authorize(Roles = "Coach")]
        public ActionResult CoachArea()
        {
            return View();
        }
    }
}
