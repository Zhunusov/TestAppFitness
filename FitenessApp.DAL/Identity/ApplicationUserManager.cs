using FitnessApp.DAL.Entities;
using Microsoft.AspNet.Identity;

namespace FitnessApp.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {

        }
    }
}
