using ProjectManagment.Core.Contracts;
using ProjectManagment.Core.Entities;
using ProjectManagment.Core.Model;
using static ProjectManagment.Infrastructure.Data.ProjrctManagmentDataInMemory;

namespace ProjectManagment.Infrastructure.Services
{
    public class ProjectManagmentService : IProjectManagementInterface
    {

        //Department Service
        public IEnumerable<Department> GetDepartmentDeta(int? deptId = null, string? deptName = null)
        {
           
                    var departmentData = from department in departments
                                         where (deptId == null || department.DepartmentId == deptId)
                                         && (deptName == null || department.DepartmentName?.ToLower() == deptName)
                                         select department;
                    return departmentData;
            
        }

        //Project Service
        public IEnumerable<Project> GetProjectsData(int? deptId = null, string? deptName = null)
        {
           
                    var projectData = from project in projects
                                      join department in departments
                                      on project.DepartmentId equals department.DepartmentId
                                      where (deptId == null || department.DepartmentId == deptId)
                                      && (deptName == null || department.DepartmentName?.ToLower() == deptName)
                                      select project;

                    return projectData;
               
        }

        //Employee Service
        public IEnumerable<Employee> GetEmployeeData(int? deptId = null, int? empNumber = null)
        {
           
                    var employeeData = from employee in employees
                                       where (deptId == null || employee.DepartmentId == deptId)
                                       && (empNumber == null || employee.EmployeeNumber == empNumber)
                                       select employee;
                    return employeeData;
                
            
        }

        // the number of employees working for each department.de 
        public IEnumerable<EmployeeCount> GetEmployeeCount()
        {

            var employeeCounts = from employee in employees
                                 group employee by employee.DepartmentId into empCount
                                 select new EmployeeCount() { DepartmentId = empCount.Key, TotalEmployee = empCount.Count() };

            return employeeCounts;

        }

        //the total salary paid for each department.

        public IEnumerable<DepartmentSalary> GetDepartmentSalary()
        {
            var departmentSalaries = from employee in employees
                                     group employee by employee.DepartmentId into EmpCount
                                     select new DepartmentSalary() { DepartmentId = EmpCount.Key, DepartmentTotalSalary = (decimal)EmpCount.Sum(sal => sal.Salary) };
            return departmentSalaries;
        }

        // return the  result(DepartmentName, Project Name, Assignment Name, Employee Name)
        public IEnumerable<ProjectResourseDetails> GetDeatils()
        {
            var combineDetails = (from department in departments
                                  join employee in employees
                                  on department.DepartmentId equals employee.DepartmentId
                                  join project in projects
                                  on employee.DepartmentId equals project.DepartmentId
                                  join assignment in assignments
                                  on employee.EmployeeNumber equals assignment.EmployeeNumber
                                  select new
                                  {
                                      departmentName = department.DepartmentName,
                                      employeeName = employee.EmployeeName,
                                      projectName = project.ProjectName,
                                      assignmentName = assignment.AssignmentName
                                  }).Distinct();

            var combineData = from data in combineDetails
                              select new ProjectResourseDetails() { DepartmentName = data.departmentName, EmployeeName = data.employeeName,
                                  ProjectName = data.projectName, AssignmentName = data.assignmentName };

            return combineData;

        }

        //  Department wise using DepartmentId And DepartmentName Text
        public IEnumerable<ProjectResourseDetails> GetCombineData(int? deptId = null, string? deptName = null)
        {
          
                var departmentWiseData = from combine in GetDeatils()
                                         join department in departments
                                         on combine.DepartmentName equals department.DepartmentName
                                         where (deptId == null || department.DepartmentId == deptId)
                                         && (deptName == null || combine.DepartmentName.ToLower().Contains(deptName))
                                         select combine;
                return departmentWiseData;
            
        }

        //  search the result by text
        public IEnumerable<ProjectResourseDetails> GetSearchData(string Text)
        {
            var searchData = from combineData in GetDeatils()
                             where combineData.DepartmentName.ToLower().Contains(Text) || combineData.EmployeeName.ToLower().Contains(Text)
                             || combineData.ProjectName.ToLower().Contains(Text) || combineData.AssignmentName.ToLower().Contains(Text)
                             select combineData;
            return searchData;
        }

        //Display data Using Generic Method
        
        public void DisplayData<t>(IEnumerable<t> collections)
        {
                foreach (var data in collections)
                {
                    Console.WriteLine(data?.ToString());
                }
        }
    
    }
}
