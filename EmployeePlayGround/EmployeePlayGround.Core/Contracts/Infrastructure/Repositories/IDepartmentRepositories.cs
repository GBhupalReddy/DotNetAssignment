using EmployeePlayGround.Core.Entities;

namespace EmployeePlayGround.Infrastructure.Repositories
{
    public interface IDepartmentRepositories
    {
        Task<Department> CreateAsync(Department department);
        Task DeletedAsync(int departmentId);
        Task<Department> GetEmployeeAsync(int departmentID);
        Task<IEnumerable<Department>> GetEmployeesAsync();
        Task<Department> UpdatedAsync(int employeeId, Department department);
    }
}