using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.DAL.Entities
{
    public class TrainingTemplate
    {
        [Key]
        public Guid Id { get; set; }
        public string Type { get; set; }
        public ICollection<CustomLoad> CustomLoads { get; set; }
        public TrainingTemplate()
        {
            CustomLoads = new List<CustomLoad>();
        }
    }
}
