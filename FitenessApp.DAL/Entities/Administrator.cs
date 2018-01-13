using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitenessApp.DAL.Entities
{
    public class Administrator
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }   

        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("Image")]
        public Guid ImageId { get; set; }
        public Image Image { get; set; }
    }
}
