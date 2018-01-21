using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.DAL.Entities
{
    public class Load
    {
        [Key]
        public string Title { get; set; }       
        public string Description { get; set; }
    }
}
