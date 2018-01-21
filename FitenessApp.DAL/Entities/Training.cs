using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.DAL.Entities
{
    public  class Training
    { 
        [Key]       
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime? DateOfTraining { get; set; }
        public DateTime? TimeOfTraining { get; set; }
        public string Status { get; set; }

        [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        [ForeignKey("Coach")]
        public string CoachId { get; set; }
        public Coach Coach { get; set; }
        [ForeignKey("Manager")]
        public string ManagerId { get; set; }
        public Manager Manager { get; set; }
        public ICollection<CustomLoad> CustomLoads { get; set; }
        public Training()
        {
            CustomLoads = new List<CustomLoad>();
        }
    }
}
