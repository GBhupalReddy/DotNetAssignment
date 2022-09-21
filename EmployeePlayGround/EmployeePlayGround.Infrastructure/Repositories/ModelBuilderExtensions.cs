using EmployeePlayGround.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeePlayGround.Infrastructure.Repositories
{
    public static class ModelBuilderExtensions
    {
        public static void  Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
               
            new Employee
            {
                Id = 6,
                Name = "Kameswari",
                Salary = 10000m,
                DepartmentId = 2
            },
                new Employee
                {
                    Id = 7,
                    Name = "Naneet",
                    Salary = 20000m,
                    DepartmentId = 2
                });
        }
    }
}
