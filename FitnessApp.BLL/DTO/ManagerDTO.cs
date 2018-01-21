using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.BLL.DTO
{
    public class ManagerDTO : UserDTO
    {
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public ICollection<string> Coaches { get; set; }
        public ICollection<string> Customers { get; set; }
        public ICollection<TrainingDTO> TrainingsDTO { get; set; }

        public ManagerDTO()
        {
            Coaches = new List<string>();
            Customers = new List<string>();
            TrainingsDTO = new List<TrainingDTO>();
        }
    }
}
