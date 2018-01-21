using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.DAL.Entities
{
    public class CustomLoad
    { 
        [Key]       
        public Guid Id { get; set; }
        public int Amount { get; set; }

        [ForeignKey("Load")]
        public string LoadTitle { get; set; }
        public Load Load { get; set; }
        [ForeignKey("Training")]
        public Guid TrainingId { get; set; }
        public Training Training { get; set; }
    }
}
