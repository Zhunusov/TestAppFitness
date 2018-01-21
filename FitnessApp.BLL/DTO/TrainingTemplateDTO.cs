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
        public string Type { get; set; }
        public ICollection<CustomLoadDTO> CustomLoadsDTO { get; set; }

        public TrainingTemplateDTO()
        {
            CustomLoadsDTO = new List<CustomLoadDTO>();
        }
    }
}
