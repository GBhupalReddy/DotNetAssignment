using EmployeePlayGround.Core.Entities;
using EmployeePlayGround.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeePlayGround.Infrastructure.Data
{
    public  class EmployeeContext : DbContext
    {
       public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected  override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server= (localDb)\MSSQLLocalDB; DataBase=EmployeeGroundDB;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employee",schema:"Emp");
            modelBuilder.Entity<Department>().ToTable("Department", schema: "Emp");
            modelBuilder.Entity<Employee>().Property(e => e.Name).HasColumnType("Varchar(50)");
            modelBuilder.Entity<Employee>().Property(e => e.Salary).HasColumnType("decimal(8,2)");
            modelBuilder.Entity<Employee>().Property(e => e.Id).IsRequired();

            modelBuilder.Seed();
                

        }
    
    }
}
