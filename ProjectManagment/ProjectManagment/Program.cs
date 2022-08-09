
using ProjectManagment.Infrastructure.Services;
using Serilog;
using Serilog.Formatting.Json;


Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(new JsonFormatter(), "logs/ProjectManagmentlogging.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
 
  Log.Information("This is ProjectManagment logging on console and file \n" +
      " ");

const string compareString = "yes";
string? value;

ProjectManagmentServiceValidation projectManagmentServiceValidation = new ProjectManagmentServiceValidation();
do
{
    Console.WriteLine("\t\t\t WELCOME TO PROJECT MANAGEMENT ");
    Console.WriteLine();
    Console.WriteLine(" PRESS 1 : Do you want department data  ");
    Console.WriteLine(" PRESS 2 : Do you want Employee data  ");
    Console.WriteLine(" PRESS 3 : Do you want Project  data  ");
    Console.WriteLine(" PRESS 4 : Do you want how many employees in each department  ");
    Console.WriteLine(" PRESS 5 : Do you want total salary paid for each department  ");
    Console.WriteLine(" PRESS 6 : Do you want ProjectResourseDetails  ");
    Console.WriteLine();

    try
    {
        int? deatils = Convert.ToInt32(Console.ReadLine());
        
        switch (deatils)
        {
           case 1:
               projectManagmentServiceValidation.DepartmentData();
               break;

           case 2:
               projectManagmentServiceValidation.EmployeeData();
               break;

           case 3:
               projectManagmentServiceValidation.ProjectData();
               break;

           case 4:
               projectManagmentServiceValidation. EmployeeCount();
               break;

           case 5:
               projectManagmentServiceValidation.DepartmentSalary();
               break;

           case 6:
               projectManagmentServiceValidation.ProjectResourseDetails();
               break;

           default:
           throw new ArgumentOutOfRangeException("Please Enter on 1 to 6 numbers only");

        }
    }
   
    catch(ArgumentOutOfRangeException arex)
    {
        Log.Error(arex.Message);
    }
    catch(InvalidDataException invalidex)
    {
        Log.Error(invalidex.Message);
    }
    catch(ArgumentNullException)
    {
        Log.Error("whit out enter anything it is not possible to search data ");
    }
    catch (FormatException)
    {
        Log.Error("you are trying with string are null values it is not possible");
    }
    catch(Exception ex)
    {
        Log.Error(ex.Message);
    }

    Console.WriteLine();
    Console.WriteLine("Do want again search data please enter yes ");
    value = Console.ReadLine()?.ToLower();
    Console.WriteLine();

} while (compareString.Equals(value));

Log.CloseAndFlush();    

