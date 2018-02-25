using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.DAL.Entities
{
    public class Image
    {
        [Key]
        public Guid Id { get; set; }
        public string OriginImage { get; set; }
        public string Avatar { get; set; }
        public string SmallAvatar { get; set; }
    }
}
