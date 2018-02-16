using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.BLL.DTO
{
    public class CoachDTO : UserDTO
    {
        public DateTime? DateOfBirth { get; set; }
        public string DateOfBirthString { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }

        public string Manager { get; set; }
        public ICollection<string> Customers { get; set; }
        public ICollection<TrainingDTO> TrainingsDTO { get; set; }

        public CoachDTO()
        {
            Customers = new List<string>();
            TrainingsDTO = new List<TrainingDTO>();
        }
    }
}
