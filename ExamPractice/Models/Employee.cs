using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPractice.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Email")]
        public string Email { get; set; }

        
        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Enter ConfirmPassword")]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
       
        public DateTime RegistrationDate { get; set; }
    }
}
