
using ProjectManagment.Infrastructure.Services;
using System.Collections;

public class Program
{

    static void Main(string[] args)
    {
        // Service  object Creation

        ProjectManagmentService projectManagementService = new ProjectManagmentService();

        // Department 

        // The department details for the department Id. 

        Console.WriteLine("The department details for the department Id.");
        Console.WriteLine();
        var getDeparmentData = projectManagementService.GetDepartmentDeta(6);

        projectManagementService.CheckData(getDeparmentData);
        Console.WriteLine();

        //The department details for the Department Name

        Console.WriteLine("The department details for the Department Name ");
        Console.WriteLine();
        var getDeparment = projectManagementService.GetDepartmentDeta(deptName: "Marketing");
        projectManagementService.CheckData(getDeparment);
        Console.WriteLine();

        // all Departments  

        Console.WriteLine(" all departments  ");
        Console.WriteLine();
        var deparmentyData = projectManagementService.GetDepartmentDeta();
        projectManagementService.CheckData(deparmentyData);
        Console.WriteLine();


        //Project

        //the list of projects there for the department Id. 

        Console.WriteLine(" the list of projects there for the department Id. ");
        Console.WriteLine();
        var getProjectData = projectManagementService.GetProjectsData(3);
        projectManagementService.CheckData(getProjectData);
        Console.WriteLine();

        //the list of projects there for the Department Name  

        Console.WriteLine(" the list of projects there for the Department Name  ");
        Console.WriteLine();
        var ProjectData = projectManagementService.GetProjectsData(deptName: "Marketing");
        projectManagementService.CheckData(ProjectData);
        Console.WriteLine();

        //all projects for each department 

        Console.WriteLine(" all projects for each department ");
        Console.WriteLine();
        var getProject = projectManagementService.GetProjectsData();
        projectManagementService.CheckData(getProject);
        Console.WriteLine();


        //Employee

        //the list of employees there for the department Id

        Console.WriteLine(" the list of employees there for the department Id. ");
        Console.WriteLine();
        var getEmployeeData = projectManagementService.GetEmployeeData(deptId: 3);
        projectManagementService.CheckData(getEmployeeData);
        Console.WriteLine();

        // the employees details for the Employee Id 

        Console.WriteLine(" the employees details for the Employee Id ");
        Console.WriteLine();
        var employeeData = projectManagementService.GetEmployeeData(121);
        projectManagementService.CheckData(employeeData);
        Console.WriteLine();

        // the number of employees working for each department 

        Console.WriteLine("the number of employees working for each department");
        Console.WriteLine();
        var employeCount = projectManagementService.GetEmployeeCount();
        projectManagementService.CheckData(employeCount);
        Console.WriteLine();

        //the total salary paid for each department.

        Console.WriteLine();
        Console.WriteLine("The total salary paid for each department");
        Console.WriteLine();
        var departmentSalary = projectManagementService.GetDepartmentSalary();
        projectManagementService.CheckData(departmentSalary);
        Console.WriteLine();

        // return the  result(DepartmentName, Project Name, Assignment Name, Employee Name) 

        var combinedata = projectManagementService.GetDeatils();
        Console.WriteLine();

        //  Department wise using DepartmentId

        Console.WriteLine("Enter Department Id  which Department data you want");
        try
        {
            int deptId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Department wise using DepartmentId is {deptId}");
            Console.WriteLine();
            var combineDataId = projectManagementService.GetCombineData(deptId: deptId);
            projectManagementService.CheckData(combineDataId);
            Console.WriteLine();

        }
        catch(FormatException ex)
        {
            Console.WriteLine(ex.Message);
        }
       
        // Department wise using DepartmentName Text

        Console.WriteLine("Enter The text which Department data you want");
        string? deptName = Console.ReadLine();
        Console.WriteLine($"Department wise using DepartmentName Text is {deptName}");
        Console.WriteLine();
        var combinedataName = projectManagementService.GetCombineData(deptName: deptName);
        projectManagementService.CheckData(combinedataName);
        Console.WriteLine();

        // search the result by text

        Console.WriteLine("Enter searching  Text");
        string? searcText = Console.ReadLine();
        Console.WriteLine($"search the result by {searcText} text");
        Console.WriteLine();
        var searchData = projectManagementService.GetSearchData(searcText);
        projectManagementService.CheckData(searchData);

       
    }


}
