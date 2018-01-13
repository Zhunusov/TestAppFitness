using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitenessApp.DAL.Entities
{
    public class Image
    {
        [Key]
        public Guid Id { get; set; }
        public string OriginImage { get; set; }
        public string Avatar { get; set; }
        public string SmallAvatar { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
