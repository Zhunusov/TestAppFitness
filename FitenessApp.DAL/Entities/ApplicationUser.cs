using Microsoft.AspNet.Identity.EntityFramework;


namespace FitnessApp.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual Administrator Administrator { get; set; }
        public virtual Coach Coach { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Manager Manager { get; set; }
    }
}
