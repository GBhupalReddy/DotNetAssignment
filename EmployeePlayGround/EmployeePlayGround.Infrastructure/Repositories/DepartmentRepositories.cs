
using EmployeePlayGround.Core.Entities;
using EmployeePlayGround.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeePlayGround.Infrastructure.Repositories
{
    public class DepartmentRepositories : IDepartmentRepositories
    {

        private readonly EmployeeContext _employeeContext;
        public DepartmentRepositories(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        public async Task<Department> CreateAsync(Department department)
        {

            _employeeContext.Departments.Add(department);
            await _employeeContext.SaveChangesAsync();
            return department;

        }
        public async Task<IEnumerable<Department>> GetEmployeesAsync()
        {
            var departmentQuery = from department in _employeeContext.Departments
                                  select department;
            return await departmentQuery.ToListAsync();
        }

        public async Task<Department> GetEmployeeAsync(int departmentID)
        {
            return await _employeeContext.Departments.Where(e => e.Id == departmentID).FirstOrDefaultAsync();
        }
        public async Task<Department> UpdatedAsync(int employeeId, Department department)
        {
            var departmentToBeUpdated = await GetEmployeeAsync(employeeId);
            departmentToBeUpdated.Name = department.Name;

            _employeeContext.Departments.Update(departmentToBeUpdated);
            _employeeContext.SaveChanges();

            return departmentToBeUpdated;
        }
        public async Task DeletedAsync(int departmentId)
        {
            var deletedDepartment = await GetEmployeeAsync(departmentId);
            _employeeContext.Departments.Remove(deletedDepartment);
            await _employeeContext.SaveChangesAsync();

        }

    }
}
