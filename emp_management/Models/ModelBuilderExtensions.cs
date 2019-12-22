using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace emp_management.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder )
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "TT",
                    Department = Dept.IT,
                    Email = "tt@gmail.com"
                },

                new Employee
                {
                    Id = 2,
                    Name = "EE",
                    Department = Dept.HR,
                    Email = "EE@gmail.com"
                }
                );
        }
    }
}
