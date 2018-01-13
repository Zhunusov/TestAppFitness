using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitenessApp.DAL.Entities
{
    public class Coach
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }


        public virtual ApplicationUser ApplicationUser { get; set; }       
        [ForeignKey("Manager")]
        public string ManagerId { get; set; }
        public Manager Manager { get; set; }
        [ForeignKey("Image")]
        public Guid ImageId { get; set; }
        public Image Image { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public ICollection<Training> Trainings { get; set; }

        public Coach()
        {
            Customers = new List<Customer>();
            Trainings = new List<Training>();
        }
    }
}
