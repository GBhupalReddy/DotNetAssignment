// See https://aka.ms/new-console-template for more information

using EmployeePlayGround.Core.Entities;
using EmployeePlayGround.Infrastructure.Data;
using EmployeePlayGround.Infrastructure.Repositories;

Console.WriteLine("Hello, World!");





using (var employeeContext = new EmployeeContext())
{
    //IDepartmentRepositories departmentRepositories = new DepartmentRepositories(employeeContext);
    //var hr = await departmentRepositories.CreateAsync(new Department() { Name = "HR" });
    //var it = await departmentRepositories.CreateAsync(new Department() { Name = "IT" });
    //var accounts = await departmentRepositories.CreateAsync(new Department() { Name = "Accounts" });

    //IEmployeeRepositories employeeRepositories = new EmployeeRepositories(employeeContext);
    //var bhupal = await employeeRepositories.CreateAsync(new Employee
    //{
    //    Name = "Bhupal",
    //    Salary = 12000m,
    //    DepartmentId = 1,
    //});
    //var mallika = await employeeRepositories.CreateAsync(new Employee
    //{
    //    Name = "mallika",
    //    Salary = 10500m,
    //    DepartmentId = 2
    //});
    //Console.WriteLine($"Create Employee : {bhupal.Id} {bhupal.Name}, {mallika.Id} {mallika.Name}");
    //var employees = await employeeRepositories.GetEmployeesAsync();

    //Console.WriteLine($"Total Employee Records : {employees.Count()} ");

    //var updatedBhupalData = new Employee
    //{
    //    Name = "Bhupal Reddy",
    //    Salary = 12000m,
    //    DepartmentId = 1
    //};
    //var updatedEmployee = await employeeRepositories.UpdatedAsync(bhupal.Id, updatedBhupalData);
    //Console.WriteLine($"Updated Employee : {updatedEmployee.Id} {updatedEmployee.Name}  {updatedEmployee.Salary}");

    //await employeeRepositories.DeletedAsync(mallika.Id);

    //var deletedRecord = await employeeRepositories.GetEmployeeAsync(mallika?.Id ?? 0);
    //Console.WriteLine($"was record deleted successfully? {deletedRecord == null: true ? false}");
}