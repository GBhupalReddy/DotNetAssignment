using EmployeeManagement._Core.Entities;

namespace EmployeeManagement._Core.Contracts
{
    public interface EmployeeManagementInterface
    {
        public List<Department> GetDepartments();
        public List<Employee> GetEmployee();
        public List<Project> GetProjects();
        public List<Assignment> GetAssignments();
    }
}
