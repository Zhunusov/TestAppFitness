using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.BLL.DTO
{
    public class CustomerDTO : UserDTO
    {
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public int Weight { get; set; }
        public int Growth { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }

        public string Coach { get; set; }
        public string Manager { get; set; }
        public ICollection<TrainingDTO> TrainingsDTO { get; set; }
        public CustomerDTO()
        {
            TrainingsDTO = new List<TrainingDTO>();
        }
    }
}
