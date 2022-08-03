
using ProjectManagment.Infrastructure.Services;
using Serilog;
using Serilog.Formatting.Json;
using System.Collections;

public class Program
{

    static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
                     .MinimumLevel.Debug()
                    
                     .WriteTo.File(new JsonFormatter(), "logs/ProjectManagmentlogging.txt", rollingInterval: RollingInterval.Day)
                     .CreateLogger();

        // Service  object Creation

        ProjectManagmentService projectManagementService = new ProjectManagmentService();


        // Department 

        // The department details for the department Id. 

        Console.WriteLine("The department details for the department Id.");
        Console.WriteLine();
        var getDeparmentData = projectManagementService.GetDepartmentDeta(-2);
        CheckData(getDeparmentData);
        Console.WriteLine();

        //The department details for the Department Name

        Console.WriteLine("The department details for the Department Name ");
        Console.WriteLine();
        var getDeparment = projectManagementService.GetDepartmentDeta(deptName: "Marketing");
        CheckData(getDeparment);
        Console.WriteLine();

        // all Departments  

        Console.WriteLine(" all departments  ");
        Console.WriteLine();
        var deparmentyData = projectManagementService.GetDepartmentDeta();
        CheckData(deparmentyData);
        Console.WriteLine();


        //Project

        //the list of projects there for the department Id. 

        Console.WriteLine(" the list of projects there for the department Id. ");
        Console.WriteLine();
        var getProjectData = projectManagementService.GetProjectsData(3);
        CheckData(getProjectData);
        Console.WriteLine();

        //the list of projects there for the Department Name  

        Console.WriteLine(" the list of projects there for the Department Name  ");
        Console.WriteLine();
        var ProjectData = projectManagementService.GetProjectsData(deptName: "Marketing");
        CheckData(ProjectData);
        Console.WriteLine();

        //all projects for each department 

        Console.WriteLine(" all projects for each department ");
        Console.WriteLine();
        var getProject = projectManagementService.GetProjectsData();
        CheckData(getProject);
        Console.WriteLine();


        //Employee

        //the list of employees there for the department Id

        Console.WriteLine(" the list of employees there for the department Id. ");
        Console.WriteLine();
        var getEmployeeData = projectManagementService.GetEmployeeData(deptId: 3);
       CheckData(getEmployeeData);
        Console.WriteLine();

        // the employees details for the Employee Id 

        Console.WriteLine(" the employees details for the Employee Id ");
        Console.WriteLine();
        var employeeData = projectManagementService.GetEmployeeData(empNumber:112);
      CheckData(employeeData);
        Console.WriteLine();

       
        // the number of employees working for each department 

        Console.WriteLine("the number of employees working for each department");
        Console.WriteLine();
        var employeCount = projectManagementService.GetEmployeeCount();
         CheckData(employeCount);
        Console.WriteLine();

        //the total salary paid for each department.

        Console.WriteLine();
        Console.WriteLine("The total salary paid for each department");
        Console.WriteLine();
        var departmentSalary = projectManagementService.GetDepartmentSalary();
       CheckData(departmentSalary);
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
            CheckData(combineDataId);
            Console.WriteLine();

        }
        catch(FormatException)
        {
            Console.WriteLine("please enter only integer values");
            Log.Error("you are trying with string are null values ");
        }
        catch(Exception aex)
        {
            Console.WriteLine(aex.Message);
            Log.Error("you are trying with string are null values ");
        }

        //  Department wise using Department

        Console.WriteLine("Enter The text which Department data you want");
        try {
            string? departmentName = Console.ReadLine();
            if (String.IsNullOrEmpty(departmentName))
            {
                throw new InvalidDataException("with out enter  any string it is not possible to search department data ");
            }
            
                Console.WriteLine($"Department wise using DepartmentName Text is {departmentName}");
                Console.WriteLine();
                var combineDataName = projectManagementService.GetCombineData(deptName: departmentName);
               CheckData(combineDataName);
                Console.WriteLine();
           
        }
        catch(InvalidDataException idex)
        {
            Console.WriteLine(idex.Message);
            Console.WriteLine();
            Log.Error("with out enter  any string it is not possible to search department data ");
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            Log.Error("with out enter  any string it is not possible to search department data ");
        }


        // search the result by text

        try
        {
            Console.WriteLine("Enter searching  Text");
            string? searchText = Console.ReadLine();
            if (String.IsNullOrEmpty(searchText))
            {
                throw new Exception("with out enter  any string it is not possible to search data ");

            }
                Console.WriteLine($"search the result by {searchText} text");
                Console.WriteLine();
                var searchData = projectManagementService.GetSearchData(searchText);
                CheckData(searchData);
          

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine();
            Log.Error("with out enter  any string it is not possible to search data ");
        }
        finally
        {
            Log.CloseAndFlush();
        }

        // Checking Method for return Result is empty or not

        void CheckData<t>(IEnumerable<t> CollecatedData)
        {
            

                try
                {
                    if (CollecatedData is null)
                    {
                        throw new ArgumentNullException(nameof(CollecatedData));
                    }
                    if (CollecatedData.Any())
                    {
                        projectManagementService.DisplayData(CollecatedData);
                    }
                    else
                    {

                        Console.WriteLine("data not found");
                    }

                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("  please enter valid Input");

                }


           

        }


    }


}
