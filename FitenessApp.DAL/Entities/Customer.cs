using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.DAL.Entities
{
    public class Customer
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public int? Weight { get; set; }
        public int? Growth { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("Coach")]
        public string CoachId { get; set; }
        public Coach Coach { get; set; }
        [ForeignKey("Manager")]
        public string ManagerId { get; set; }
        public Manager Manager { get; set; }
        [ForeignKey("Image")]
        public Guid? ImageId { get; set; }
        public Image Image { get; set; }

        public ICollection<Training> Trainings { get; set; }
        public Customer()
        {
            Trainings = new List<Training>();
        }

    }
}
