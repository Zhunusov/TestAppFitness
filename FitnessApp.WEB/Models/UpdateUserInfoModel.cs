using System;
using System.ComponentModel.DataAnnotations;

namespace FitnessApp.WEB.Models
{
    public class UpdateUserInfoModel
    {
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Role { get; set; }
        public int? Growth { get; set; }
        public int? Weight { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Sex { get; set; }
    }
}