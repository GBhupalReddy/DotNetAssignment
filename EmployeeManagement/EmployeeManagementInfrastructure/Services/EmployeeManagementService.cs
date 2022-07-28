using EmployeeManagement._Core.Entities;
using EmployeeManagementInfrastructure.Data;
using System.Collections;
using System.Collections.Generic;

namespace EmployeeManagement.Infrastructure.Services
{
    public class EmployeeManagementService
    {
        //Department Service

        EmployeeManagementDataInMemory employeeManagementDataInMemory = new EmployeeManagementDataInMemory();

        public IEnumerable<Department> GetDepartmentData()
        {
            return (from department in employeeManagementDataInMemory.GetDepartments() select department);
        }
        public IEnumerable<Department> GetDepartmentData(int deptId)
        {
            return (from department in employeeManagementDataInMemory.GetDepartments() where department.DepartmentId == deptId select department);
        }
        public IEnumerable<Department> GetDepartmentData(string deptName)
        {
            return from department in employeeManagementDataInMemory.GetDepartments() where department.DepartmentName == deptName select department;
        }

        public void Displaydepartment(IEnumerable<Department> dept)
        {
            foreach (var v in dept)
            {
                Console.WriteLine($"\t{v.DepartmentId} \t {v.DepartmentName} \t {v.PhoneNumber}");
            }
        }

        //Project Service
        public IEnumerable<Project> GetProjectsData(int deptId)
        {
            return (from project in employeeManagementDataInMemory.GetProjects() where project.DepartmentId == deptId select project);

        }
        public IEnumerable<Project> GetProjectsData()
        {
            return (from project in employeeManagementDataInMemory.GetProjects() select project);

        }
        public IEnumerable<Project> GetProjectsData(string deptName)
        {
            return (from project in employeeManagementDataInMemory.GetProjects() join department in employeeManagementDataInMemory.GetDepartments() on project.DepartmentId equals department.DepartmentId where department.DepartmentName == deptName select project);

        }
        public void DisplayProjectData(IEnumerable<Project> project)
        {
            foreach (var projectItem in project)
            {
                Console.WriteLine($"{projectItem.ProjectId}  \t {projectItem.DepartmentId} \t {projectItem.MaxHours} \t {projectItem.StartDate} \t {projectItem.EndDate} \t {projectItem.ProjectName}");
            }
        }


        //Employee Service
        public IEnumerable<Employee> GetEmployeeData()
        {
            return from employee in employeeManagementDataInMemory.GetEmployee() select employee;
        }
        public IEnumerable<Employee> GetEmployeeData(int Number)
        {
            if (Number < 5)
            {
                return from employee in employeeManagementDataInMemory.GetEmployee() where employee.DepartmentId == Number select employee;
            }
            else
            {
                return from employee in employeeManagementDataInMemory.GetEmployee() where employee.EmployeeNumber == Number select employee;
            }
        }

        // the number of employees working for each department.de 
        public IEnumerable GetEmployeeCount()
        {
            
            var result = (from employee in employeeManagementDataInMemory.GetEmployee() group employee by employee.DepartmentId into EmpCount select new { DapartmentId = EmpCount.Key, EmployeeCount = EmpCount.Count() }).ToList();
            
            return result;

        }

        //the total salary paid for each department.

        public IEnumerable GetDepartmentSalary()
        {
            var result = (from employee in employeeManagementDataInMemory.GetEmployee() group employee by employee.DepartmentId into EmpCount select new { DapartmentId = EmpCount.Key, Deptsalary = EmpCount.Sum(sal => sal.Salary) }).ToList();
             return result;
            
                            
        }



        public void DisplayEmployee(IEnumerable<Employee> employees)
        {
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.EmployeeNumber}\t {employee.EmployeeName} \t {employee.FirstName} \t {employee.LastName} \t {employee.DepartmentId} \t {employee.Phone} \t  {employee.Email} \t {employee.Salary}");
            }

        }
    }
}

