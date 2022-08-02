using ProjectManagment.Core.Entities;
using ProjectManagment.Core.Model;

namespace ProjectManagment.Core.Contracts
{
    public interface IProjectManagementInterface
    {
        public IEnumerable<Department> GetDepartmentDeta(int? deptId = null, string? deptName = null);
        public IEnumerable<Project> GetProjectsData(int? deptId = null, string? deptName = null);
        public IEnumerable<Employee> GetEmployeeData(int? deptId = null, int? empNumber = null);
        public IEnumerable<EmployeeCount> GetEmployeeCount();
        public IEnumerable<DepartmentSalary> GetDepartmentSalary();
        public IEnumerable<ProjectResourseDetails> GetDeatils();
        public IEnumerable<ProjectResourseDetails> GetCombineData(int? deptId = null, string? deptName = null);
        public IEnumerable<ProjectResourseDetails> GetSearchData(string Text);
        public void CheckData<t>(IEnumerable<t> collections);
        public void DisplayData<t>(IEnumerable<t> collections);
    }
}
