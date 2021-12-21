using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPractice.Models
{
    public class temp
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Designation { get; set; }

        public string Department { get; set; }


        public DateTime JoiningDate { get; set; } 
    }
}
