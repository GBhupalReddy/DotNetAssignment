
using EmployeePlayGround.Core.Entities;
using EmployeePlayGround.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace EmployeePlayGround.Infrastructure.Repositories
{
    public class EmployeeRepositories : IEmployeeRepositories
    {
        private readonly EmployeeContext _employeeContext;
        public EmployeeRepositories(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {

            _employeeContext.Employees.Add(employee);
            await _employeeContext.SaveChangesAsync();
            return employee;

        }
        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            var employeeQuery = from employee in _employeeContext.Employees
                                select employee;
            return await employeeQuery.ToListAsync();
        }

        public async Task<Employee> GetEmployeeAsync(int employeeId)
        {
            return await _employeeContext.Employees.Where(e => e.Id == employeeId).FirstOrDefaultAsync();
        }
        public async Task<Employee> UpdatedAsync(int employeeId, Employee employee)
        {
            var employeeToBeUpdated = await GetEmployeeAsync(employeeId);
            employeeToBeUpdated.Name = employee.Name;
            employeeToBeUpdated.Salary = employee.Salary;
            employeeToBeUpdated.DepartmentId = employee.DepartmentId;

            _employeeContext.Employees.Update(employeeToBeUpdated);
            _employeeContext.SaveChanges();

            return employeeToBeUpdated;
        }
        public async Task DeletedAsync(int employeeId)
        {
            var deletedEmployee = await GetEmployeeAsync(employeeId);
            _employeeContext.Employees.Remove(deletedEmployee);
            await _employeeContext.SaveChangesAsync();

        }

    }
}
