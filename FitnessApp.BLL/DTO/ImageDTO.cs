using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.BLL.DTO
{
    public class ImageDTO
    {
        public Guid Id { get; set; }
        public string OriginImage { get; set; }
        public string Avatar { get; set; }
        public string SmallAvatar { get; set; }
    }
}
