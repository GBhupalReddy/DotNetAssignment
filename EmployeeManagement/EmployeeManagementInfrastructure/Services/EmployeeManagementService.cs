using EmployeeManagement._Core.Contracts;
using EmployeeManagement._Core.Entities;
using EmployeeManagement._Core.Model;
using System.Collections;
using System.Collections.Generic;
using static EmployeeManagementInfrastructure.Data.EmployeeManagementDataInMemory;


namespace EmployeeManagement.Infrastructure.Services
{
    public class EmployeeManagementService : IEmployeeManagementInterface
    {

        //Department Service
        public IEnumerable<Department> GetDepartmentDeta(int? deptId = null, string? deptName = null)
        {
            if (deptId != null || deptName != null)
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
            if (deptId != null || deptName != null)
            {
                var projectData= from project in projects
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
            if (deptId != null || empNumber != null)
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
            
            var result = from employee in employees
                          group employee by employee.DepartmentId into EmpCount 
                          select new EmployeeCount() { DepartmentId = EmpCount.Key, TotalEmployee = EmpCount.Count() };
            
            return result;

        }

        //the total salary paid for each department.

        public IEnumerable<DepartmentSalary> GetDepartmentSalary()
        {
            var result = from employee in employees
                          group employee by employee.DepartmentId into EmpCount
                          select new DepartmentSalary (){ DepartmentId = EmpCount.Key, DepartmentTotalSalary = (decimal)EmpCount.Sum(sal => sal.Salary) };
             return result;
            
                            
        }
        public IEnumerable<CombineEntities> Getdeatils()
        {
            var result = (from department in departments
                         join employee in employees
                         on department.DepartmentId equals employee.DepartmentId
                         join project in projects
                         on employee.DepartmentId equals project.DepartmentId
                         join assignment in assignments
                         on employee.EmployeeNumber equals assignment.EmployeeNumber
                         //where (deptId==null || employee.DepartmentId == deptId) && (deptname== null || department.DepartmentName.Contains(deptname))
                         select new CombineEntities()
                         { DepartmentName = department.DepartmentName, EmployeeName = employee.EmployeeName, ProjectName = project.ProjectName, AssignmentName = assignment.AssignmentName }).Distinct();


           return result;
            
        }
     
        public void DisplayData<t>(IEnumerable<t> collections)
        {
            foreach(var data in collections)
            {
                Console.WriteLine(data?.ToString());
            }
        }
       
    }
}

