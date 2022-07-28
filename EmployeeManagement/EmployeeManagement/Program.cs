using EmployeeManagement._Core.Entities;
using EmployeeManagement.Infrastructure.Services;


internal class Program
{
   
    static void Main(string[] args)
    {
        // Service  object Creation

        EmployeeManagementService employeeManagementService = new EmployeeManagementService();

        // Department 
        // Pass Department Id(optional): return department details for the department Id. 

        Console.WriteLine("The department details for the department Id." ) ;
        Console.WriteLine();
        var getDeparmentData = employeeManagementService.GetDepartmentData(2);

        employeeManagementService.Displaydepartment(getDeparmentData);
        Console.WriteLine();

        //Pass Department Name(optional): return department details for the Department Name 

        Console.WriteLine("The department details for the Department Name ");
        Console.WriteLine();
        var getDeparment = employeeManagementService.GetDepartmentData("Marketing");
        employeeManagementService.Displaydepartment(getDeparment);
        Console.WriteLine();

        //Without input parameters: return all departments  

        Console.WriteLine(" all departments  ");
        Console.WriteLine();
        var deparmentyData = employeeManagementService.GetDepartmentData();
        employeeManagementService.Displaydepartment(deparmentyData);
        Console.WriteLine();


        //Project

        //Pass Department Id(optional): return the list of projects there for the department Id. 

        Console.WriteLine(" the list of projects there for the department Id. ");
        Console.WriteLine();
        var getProjectData = employeeManagementService.GetProjectsData(3);
        employeeManagementService.DisplayProjectData(getProjectData);
        Console.WriteLine();

        //Pass Department Name(optional): return the list of projects there for the Department Name  

        Console.WriteLine(" the list of projects there for the Department Name  ");
        Console.WriteLine();
        var ProjectData = employeeManagementService.GetProjectsData("Marketing");
        employeeManagementService.DisplayProjectData(ProjectData);
        Console.WriteLine();

        //Without input parameters: return all projects for each department 

        Console.WriteLine(" all projects for each department ");
        Console.WriteLine();
        var getProject = employeeManagementService.GetProjectsData();
        employeeManagementService.DisplayProjectData(getProject);
        Console.WriteLine();


        //Employee

        //Pass Department Id(optional): returns the list of employees there for the department Id. 

        Console.WriteLine(" the list of employees there for the department Id. ");
        Console.WriteLine();
        var getEmployeeData = employeeManagementService.GetEmployeeData(3);
        employeeManagementService.DisplayEmployee(getEmployeeData);
        Console.WriteLine();

        // Pass Employee Id(optional): returns the employees details for the Employee Id 

        Console.WriteLine(" the employees details for the Employee Id ");
        Console.WriteLine();
        var employeeData = employeeManagementService.GetEmployeeData(126);
        employeeManagementService.DisplayEmployee(employeeData);
        Console.WriteLine();

        // the number of employees working for each department 
        Console.WriteLine("the number of employees working for each department");
        Console.WriteLine();
        //employeeManagementService.GetDepartmentCount();
        var employeCount = employeeManagementService.GetEmployeeCount();
        foreach (var department in employeCount)
        {
            Console.WriteLine($"{department.ToString()}");
        }
        Console.WriteLine();

        //the total salary paid for each department.
        Console.WriteLine();
        Console.WriteLine("The total salary paid for each department");
        Console.WriteLine();
        var departmentSalary=employeeManagementService.GetDepartmentSalary();
        foreach(var salary in departmentSalary)
        {
            Console.WriteLine($"{salary.ToString()}");
        }



    }


}
