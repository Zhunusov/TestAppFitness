using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.BLL.DTO
{
    public class LoadDTO
    {        
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Series { get; set; }
        public string Iteration { get; set; }
        public string Icon { get; set; }
    }
}
