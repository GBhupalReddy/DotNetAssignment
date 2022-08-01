using EmployeeManagement._Core.Entities;
using EmployeeManagement._Core.Model;
using System.Collections;

namespace EmployeeManagement._Core.Contracts
{
    public interface IEmployeeManagementInterface
    {
        public IEnumerable<Department> GetDepartmentDeta(int? deptId = null, string? deptName = null);
        public IEnumerable<Project> GetProjectsData(int? deptId = null, string? deptName = null);
        public IEnumerable<Employee> GetEmployeeData(int? deptId = null, int? empNumber = null);
        public IEnumerable<EmployeeCount> GetEmployeeCount();
        public IEnumerable<DepartmentSalary> GetDepartmentSalary();
        public IEnumerable<CombineEntities> Getdeatils();
        public void DisplayData<t>(IEnumerable<t> collections);

    }
}
