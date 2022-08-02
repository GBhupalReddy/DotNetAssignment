﻿
using ProjectManagment.Infrastructure.Services;

public class Program
{

    static void Main(string[] args)
    {
        // Service  object Creation

        ProjectManagmentService projectManagementService = new ProjectManagmentService();

        // Department 

        // Pass Department Id(optional): return department details for the department Id. 

        Console.WriteLine("The department details for the department Id.");
        Console.WriteLine();
        var getDeparmentData = projectManagementService.GetDepartmentDeta(2);

        projectManagementService.DisplayData(getDeparmentData);
        Console.WriteLine();

        //Pass Department Name(optional): return department details for the Department Name 

        Console.WriteLine("The department details for the Department Name ");
        Console.WriteLine();
        var getDeparment = projectManagementService.GetDepartmentDeta(deptName: "Marketing");
        projectManagementService.DisplayData(getDeparment);
        Console.WriteLine();

        //Without input parameters: return all departments  

        Console.WriteLine(" all departments  ");
        Console.WriteLine();
        var deparmentyData = projectManagementService.GetDepartmentDeta();
        projectManagementService.DisplayData(deparmentyData);
        Console.WriteLine();


        //Project

        //Pass Department Id(optional): return the list of projects there for the department Id. 

        Console.WriteLine(" the list of projects there for the department Id. ");
        Console.WriteLine();
        var getProjectData = projectManagementService.GetProjectsData(3);
        projectManagementService.DisplayData(getProjectData);
        Console.WriteLine();

        //Pass Department Name(optional): return the list of projects there for the Department Name  

        Console.WriteLine(" the list of projects there for the Department Name  ");
        Console.WriteLine();
        var ProjectData = projectManagementService.GetProjectsData(deptName: "Marketing");
        projectManagementService.DisplayData(ProjectData);
        Console.WriteLine();

        //Without input parameters: return all projects for each department 

        Console.WriteLine(" all projects for each department ");
        Console.WriteLine();
        var getProject = projectManagementService.GetProjectsData();
        projectManagementService.DisplayData(getProject);
        Console.WriteLine();


        //Employee

        //Pass Department Id(optional): returns the list of employees there for the department Id. 

        Console.WriteLine(" the list of employees there for the department Id. ");
        Console.WriteLine();
        var getEmployeeData = projectManagementService.GetEmployeeData(deptId: 3);
        projectManagementService.DisplayData(getEmployeeData);
        Console.WriteLine();

        // Pass Employee Id(optional): returns the employees details for the Employee Id 

        Console.WriteLine(" the employees details for the Employee Id ");
        Console.WriteLine();
        var employeeData = projectManagementService.GetEmployeeData(empNumber: 126);
        projectManagementService.DisplayData(employeeData);
        Console.WriteLine();

        // the number of employees working for each department 

        Console.WriteLine("the number of employees working for each department");
        Console.WriteLine();

        //employeeManagementService.GetDepartmentCount();

        var employeCount = projectManagementService.GetEmployeeCount();
        projectManagementService.DisplayData(employeCount);
        Console.WriteLine();

        //the total salary paid for each department.
        Console.WriteLine();
        Console.WriteLine("The total salary paid for each department");
        Console.WriteLine();
        var departmentSalary = projectManagementService.GetDepartmentSalary();
        projectManagementService.DisplayData(departmentSalary);
        Console.WriteLine();

        // return the  result(DepartmentName, Project Name, Assignment Name, Employee Name) 

        var combinedata = projectManagementService.GetDeatils();
        Console.WriteLine();

        //  Department wise using DepartmentId

        Console.WriteLine("Enter Department Id  which Department data you want");
        int deptId = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine($"Department wise using DepartmentId is {deptId}");
        var combinedataId = projectManagementService.GetData(deptId: deptId);

        projectManagementService.CheckData(combinedataId);
        Console.WriteLine();

        // Department wise using DepartmentName Text

        Console.WriteLine("Enter The text which Department data you want");
        string? deptName = Console.ReadLine();
        Console.WriteLine($"Department wise using DepartmentName Text is {deptName}");
        var combinedataName = projectManagementService.GetData(deptName: deptName);
        projectManagementService.CheckData(combinedataName);
        Console.WriteLine();

        // search the result by text

        Console.WriteLine("Enter Text");
        string searcText = Console.ReadLine();
        Console.WriteLine($"search the result by {searcText} text");
        var searchData = projectManagementService.GetSearchData(searcText);
        projectManagementService.CheckData(searchData);




    }


}