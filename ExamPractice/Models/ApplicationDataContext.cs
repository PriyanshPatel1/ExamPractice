using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPractice.Models
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {

        }
        public DbSet<Employee>employee{get;set;}

        public DbSet<temp> Temps { get; set; }
    }
}
