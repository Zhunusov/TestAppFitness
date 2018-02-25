using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.BLL.DTO
{
    public class TrainingTemplateDTO
    {
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
        public ICollection<CustomLoadDTO> CustomLoadsDTO { get; set; }

        public TrainingTemplateDTO()
        {
            CustomLoadsDTO = new List<CustomLoadDTO>();
        }
    }
}
