using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitenessApp.DAL.Entities
{
    public class Manager
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }


        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("Image")]
        public Guid ImageId { get; set; }
        public Image Image { get; set; }
        public ICollection<Coach> Coaches { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public ICollection<Training> Trainings { get; set; }
        public Manager()
        {
            Coaches = new List<Coach>();
            Customers = new List<Customer>();
            Trainings = new List<Training>();
        }
    }
}
