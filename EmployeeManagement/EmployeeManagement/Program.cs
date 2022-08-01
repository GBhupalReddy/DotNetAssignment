using EmployeeManagement._Core.Entities;
using EmployeeManagement.Infrastructure.Services;


public class Program
{
   
    static void Main(string[] args)
    {
        // Service  object Creation

        EmployeeManagementService employeeManagementService = new EmployeeManagementService();

        // Department 
        // Pass Department Id(optional): return department details for the department Id. 

        Console.WriteLine("The department details for the department Id." ) ;
        Console.WriteLine();
        var getDeparmentData = employeeManagementService.GetDepartmentDeta(2) ;

        employeeManagementService.DisplayData(getDeparmentData);
        Console.WriteLine();

        //Pass Department Name(optional): return department details for the Department Name 

        Console.WriteLine("The department details for the Department Name ");
        Console.WriteLine();
        var getDeparment = employeeManagementService.GetDepartmentDeta(deptName:"Marketing");
        employeeManagementService.DisplayData(getDeparment);
        Console.WriteLine();

        //Without input parameters: return all departments  

        Console.WriteLine(" all departments  ");
        Console.WriteLine();
        var deparmentyData = employeeManagementService.GetDepartmentDeta();
        employeeManagementService.DisplayData(deparmentyData);
        Console.WriteLine();


        //Project

        //Pass Department Id(optional): return the list of projects there for the department Id. 

        Console.WriteLine(" the list of projects there for the department Id. ");
        Console.WriteLine();
        var getProjectData = employeeManagementService.GetProjectsData(3);
        employeeManagementService.DisplayData(getProjectData);
        Console.WriteLine();

        //Pass Department Name(optional): return the list of projects there for the Department Name  

        Console.WriteLine(" the list of projects there for the Department Name  ");
        Console.WriteLine();
        var ProjectData = employeeManagementService.GetProjectsData(deptName: "Marketing");
        employeeManagementService.DisplayData(ProjectData);
        Console.WriteLine();

        //Without input parameters: return all projects for each department 

        Console.WriteLine(" all projects for each department ");
        Console.WriteLine();
        var getProject = employeeManagementService.GetProjectsData();
        employeeManagementService.DisplayData(getProject);
        Console.WriteLine();


        //Employee

        //Pass Department Id(optional): returns the list of employees there for the department Id. 

        Console.WriteLine(" the list of employees there for the department Id. ");
        Console.WriteLine();
        var getEmployeeData = employeeManagementService.GetEmployeeData(deptId : 3);
        employeeManagementService.DisplayData(getEmployeeData);
        Console.WriteLine();

        // Pass Employee Id(optional): returns the employees details for the Employee Id 

        Console.WriteLine(" the employees details for the Employee Id ");
        Console.WriteLine();
        var employeeData = employeeManagementService.GetEmployeeData(empNumber :126);
        employeeManagementService.DisplayData(employeeData);
        Console.WriteLine();

        // the number of employees working for each department 
        Console.WriteLine("the number of employees working for each department");
        Console.WriteLine();
        //employeeManagementService.GetDepartmentCount();
        var employeCount = employeeManagementService.GetEmployeeCount();
        employeeManagementService.DisplayData(employeCount);
        Console.WriteLine();

        //the total salary paid for each department.
        Console.WriteLine();
        Console.WriteLine("The total salary paid for each department");
        Console.WriteLine();
        var departmentSalary=employeeManagementService.GetDepartmentSalary();
        employeeManagementService.DisplayData(departmentSalary);
        Console.WriteLine();
       var combinedata= employeeManagementService.Getdeatils();
       
        Console.WriteLine();
        employeeManagementService.DisplayData(combinedata);





    }


}
