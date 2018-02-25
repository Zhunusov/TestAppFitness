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
        public string Name { get; set; }
        public string Aim { get; set; }
        public string GenderCategory { get; set; }
        public string Complexity { get; set; }
        public int? WeekDuration { get; set; }
        public int? DaysPerWeek { get; set; }
        public double? TrainingDuration { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public ICollection<CustomLoad> CustomLoads { get; set; }
        public TrainingTemplate()
        {
            CustomLoads = new List<CustomLoad>();
        }
    }
}
