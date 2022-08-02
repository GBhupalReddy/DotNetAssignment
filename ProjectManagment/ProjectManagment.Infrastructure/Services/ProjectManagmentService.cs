using ProjectManagment.Core.Contracts;
using ProjectManagment.Core.Entities;
using ProjectManagment.Core.Model;
using System.Collections;
using static ProjectManagment.Infrastructure.Data.ProjrctManagmentDataInMemory;

namespace ProjectManagment.Infrastructure.Services
{
    public class ProjectManagmentService : IProjectManagementInterface
    {
        //Department Service
        public IEnumerable<Department> GetDepartmentDeta(int? deptId = null, string? deptName = null)
        {
            if (deptId.HasValue || deptName != null)
            {
                var departmentData = from department in departments
                                     where (deptId == null || department.DepartmentId == deptId)
                                     && (deptName == null || department.DepartmentName == deptName)
                                     select department;
                return departmentData;
            }

            return departments;
        }

        //Project Service
        public IEnumerable<Project> GetProjectsData(int? deptId = null, string? deptName = null)
        {
            if (deptId.HasValue || deptName != null)
            {
                var projectData = from project in projects
                                  join department in departments
                                  on project.DepartmentId equals department.DepartmentId
                                  where (deptId == null || department.DepartmentId == deptId)
                                  && (deptName == null || department.DepartmentName == deptName)
                                  select project;

                return projectData;
            }

            return projects;
        }

        //Employee Service
        public IEnumerable<Employee> GetEmployeeData(int? deptId = null, int? empNumber = null)
        {
            if (deptId.HasValue || empNumber.HasValue)
            {
                var employeeData = from employee in employees
                                   where (deptId == null || employee.DepartmentId == deptId)
                                   && (empNumber == null || employee.EmployeeNumber == empNumber)
                                   select employee;
                return employeeData;
            }
            return employees;
        }

        // the number of employees working for each department.de 
        public IEnumerable<EmployeeCount> GetEmployeeCount()
        {

            var employeeCounts = from employee in employees
                                 group employee by employee.DepartmentId into EmpCount
                                 select new EmployeeCount() { DepartmentId = EmpCount.Key, TotalEmployee = EmpCount.Count() };

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
                                     && (deptName == null || combine.DepartmentName.Contains(deptName))
                                     select combine;
            return departmentWiseData;
        }

        //  search the result by text
        public IEnumerable<ProjectResourseDetails> GetSearchData(string Text)
        {
            var searchData = from combineData in GetDeatils()
                             where combineData.DepartmentName.Contains(Text) || combineData.EmployeeName.Contains(Text)
                             || combineData.ProjectName.Contains(Text) || combineData.AssignmentName.Contains(Text)
                             select combineData;
            return searchData;
        }

        //Check the  searching data found or not

        public void CheckData<t>(IEnumerable<t> CollecatedData)
        {
           

            if (CollecatedData.Any())
            {
                DisplayData(CollecatedData);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("  Data Not  Found ");
            }
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
