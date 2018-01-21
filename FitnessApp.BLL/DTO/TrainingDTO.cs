using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.BLL.DTO
{
    public class TrainingDTO
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime? DateOfTraining { get; set; }
        public DateTime? TimeOfTraining { get; set; }
        public string Status { get; set; }

        public string Customer { get; set; }
        public string Coach { get; set; }
        public string Manager { get; set; }
        public ICollection<CustomLoadDTO> CustomLoadsDTO { get; set; }

        public TrainingDTO()
        {
            CustomLoadsDTO = new List<CustomLoadDTO>();
        }

    }
}
