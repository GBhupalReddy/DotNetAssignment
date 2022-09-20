using EmployeePlayGround.Core.Entities;

namespace EmployeePlayGround.Infrastructure.Repositories
{
    public interface IEmployeeRepositories
    {
        Task<Employee> CreateAsync(Employee employee);
        Task DeletedAsync(int employeeId);
        Task<Employee> GetEmployeeAsync(int employeeId);
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<Employee> UpdatedAsync(int employeeId, Employee employee);
    }
}