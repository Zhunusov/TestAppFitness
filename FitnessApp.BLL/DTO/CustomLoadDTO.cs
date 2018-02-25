using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.BLL.DTO
{
    public class CustomLoadDTO
    {
        public Guid Id { get; set; }
        public int? CustomSeries { get; set; }
        public string CustomIteration { get; set; }
        public LoadDTO LoadDTO { get; set; }
    }
}
