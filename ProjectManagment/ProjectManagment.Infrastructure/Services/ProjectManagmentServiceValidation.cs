
namespace ProjectManagment.Infrastructure.Services
{
    public class ProjectManagmentServiceValidation : IProjectManagmentServiceValidation
    {
        ProjectManagmentService projectManagementService = new ProjectManagmentService();

        const string compareString = "yes";
        string? value;
        bool validation;

        // Main menu
        public void Mainmenu()
        {
            do
            {
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
                            DepartmentData();
                            break;

                        case 2:
                            EmployeeData();
                            break;

                        case 3:
                            ProjectData();
                            break;

                        case 4:
                            EmployeeCount();
                            break;

                        case 5:
                            DepartmentSalary();
                            break;

                        case 6:
                            ProjectResourseDetails();
                            break;

                        default:
                            throw new ArgumentOutOfRangeException("Please Enter on 1 to 6 numbers only");
                    }
                }
                catch (ArgumentOutOfRangeException arex)
                {
                    Console.WriteLine("Please enter valid input");
                    Console.WriteLine(arex.Message);
                    Mainmenu();
                }
                catch (InvalidDataException invalidex)
                {
                    Console.WriteLine("Please enter valid input");
                    Console.WriteLine(invalidex.Message);
                    Mainmenu();
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("with out enter anything it is not possible to search data \n So please enter valid data");
                    Mainmenu();
                }
                catch (FormatException)
                {
                    Console.WriteLine("you are trying with string are null values  \n So please enter valid data");
                    Mainmenu();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Please enter valid input");
                    Console.WriteLine(ex.Message);
                    Mainmenu();
                }
                Console.WriteLine();
                Console.WriteLine("Do want again search data please enter yes ");
                value = Console.ReadLine()?.ToLower();
                Console.WriteLine();
            } while (compareString.Equals(value));
        }
           
        // The department details 
        public void DepartmentData()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine(" PRESS 1 : The department details using department Id  : ");
                Console.WriteLine(" PRESS 2 : The department details using department Name  ");
                Console.WriteLine(" PRESS 3 : The All department details  3 : ");

                int? departmentSearch = Convert.ToInt32(Console.ReadLine());
                if (departmentSearch == null)
                {
                    throw new ArgumentNullException();
                }
                switch (departmentSearch)
                {
                    case 1:

                        // The department details for using Department Id
                        do 
                        { 
                             Console.WriteLine("Enter  department id ");
                             int deptId = Convert.ToInt32(Console.ReadLine());
                             if (deptId <= 0)
                             {
                                 throw new InvalidDataException("please enter valid input");
                             }
                             if (deptId == null)
                             {
                                 throw new ArgumentNullException();
                             }
                             Console.WriteLine("The department details for the department Id.");
                             Console.WriteLine();
                             var getDeparmentData = projectManagementService.GetDepartmentDeta(deptId);
                             if (CheckData(getDeparmentData))
                             {
                                 projectManagementService.DisplayData(getDeparmentData);
                                 validation = false;
                             }
                             else
                             {
                                 Console.WriteLine("Data Not Found Please enter valid input ");
                                 validation = true;
                             }
                             Console.WriteLine();
                        } while (validation) ;
                       break;

                    case 2:
                        // The department details for using Department Name
                        do
                        {
                            Console.WriteLine(" Enter Department Name (Marketing,Finance,Accounting) ");
                            Console.WriteLine();
                            string deptName = Console.ReadLine()?.ToLower();
                            if (String.IsNullOrEmpty(deptName))
                            {
                                throw new ArgumentNullException();
                            }
                            Console.WriteLine("The department details for the Department Name ");
                            var getDeparment = projectManagementService.GetDepartmentDeta(deptName: deptName);
                            if (CheckData(getDeparment))
                            {
                                projectManagementService.DisplayData(getDeparment);
                                validation = false;
                            }
                            else
                            {
                                Console.WriteLine("Data Not Found Please enter valid input ");
                                validation = true;
                            }
                            Console.WriteLine();
                        } while (validation) ;
                        break;
                    case 3:
                        // all Departments  

                        Console.WriteLine(" all departments  ");
                        Console.WriteLine();
                        var deparmentyData = projectManagementService.GetDepartmentDeta();
                        projectManagementService.DisplayData(deparmentyData);
                        Console.WriteLine();
                        break;
                    default: throw new ArgumentOutOfRangeException("Please Enter on 1 to 3 numbers only");
                }
            }
            catch (ArgumentOutOfRangeException arex)
            {
                Console.WriteLine("Please enter valid input");
                Console.WriteLine(arex.Message);

                DepartmentData();
            }
            catch (InvalidDataException )
            {
                Console.WriteLine("Please enter valid input");
                DepartmentData();
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("with out enter anything it is not possible to search data  \n So please enter valid data");
                DepartmentData();

            }
            catch (FormatException)
            {
                Console.WriteLine("you are trying with string are null values \n So please enter valid data");
                DepartmentData();
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter valid input");
                DepartmentData();
            }
            do
            {
                Console.WriteLine("PRESS 1 : Department Menu");
                Console.WriteLine("PRESS 2 : Main menu");
                Console.WriteLine();
                int a = Convert.ToInt32(Console.ReadLine());
                if (a == 1)
                {
                    DepartmentData();
                }
                else if (a == 2)
                {
                    Mainmenu();
                }
                else
                {
                    Console.WriteLine("please Enter only 1 or 2");
                    Console.WriteLine();
                    validation = true;
                }
            }while(validation);
        }

        // Employee Data
        public void EmployeeData()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine(" PRESS 1 : The Employee details using department Id  : ");
                Console.WriteLine(" PRESS 2 : The Employee details using Employee number  ");
                Console.WriteLine(" PRESS 3 : The All Employee details : ");
                int? employeesearch = Convert.ToInt32(Console.ReadLine());
                if (employeesearch == null)
                {
                    throw new ArgumentNullException();
                }
                switch (employeesearch)
                {
                    case 1:
                        // The employee details for using department id
                        do
                        {
                              Console.WriteLine("Enter  department id ");
                              int deptId = Convert.ToInt32(Console.ReadLine());
                              if (deptId <= 0)
                              {
                                  throw new InvalidDataException();
                              }
                              if (deptId==null)
                              {
                                  throw new ArgumentNullException();
                              }
                              Console.WriteLine($" the list of employees there for the department Id {deptId} ");
                              Console.WriteLine();
                              var getEmployeeData = projectManagementService.GetEmployeeData(deptId: deptId);
                              if (CheckData(getEmployeeData))
                              {
                                  projectManagementService.DisplayData(getEmployeeData);
                                  validation = false;
                              }
                              else
                              {
                                  Console.WriteLine("Data Not Found Please enter valid input ");
                                  validation = true;
                              }
                              Console.WriteLine();
                        } while (validation) ;
                        break;
                    case 2:
                        // The employee details for using employee number
                        do
                        {
                             Console.WriteLine("Enter which  employees number");
                             int empNumber = Convert.ToInt32(Console.ReadLine());
                             if (empNumber <= 0)
                             {
                                 throw new InvalidDataException();
                             }
                             if (empNumber == null)
                             {
                                 throw new ArgumentNullException();
                             }
                             Console.WriteLine($" the employees details for the Employee number {empNumber} ");
                             Console.WriteLine();
                             var employeeData = projectManagementService.GetEmployeeData(empNumber: empNumber);
                             if (CheckData(employeeData))
                             {
                                 projectManagementService.DisplayData(employeeData);
                                 validation = false;
                             }
                             else
                             {
                                 Console.WriteLine("Data Not Found Please enter valid input ");
                                 validation = true;
                             }
                             Console.WriteLine();
                        } while (validation) ;
                        break;
                    case 3:
                        // All  employee details  

                        Console.WriteLine(" the all employees details  ");
                        Console.WriteLine();
                        var allEmployeeData = projectManagementService.GetEmployeeData();
                        projectManagementService.DisplayData(allEmployeeData);
                        Console.WriteLine();
                        break;
                    default: throw new ArgumentOutOfRangeException("Please Enter on 1 to 3 numbers only");
                }
            }
            catch (ArgumentOutOfRangeException arex)
            {
                Console.WriteLine("Please enter valid input");
                Console.WriteLine(arex.Message);
                EmployeeData();
            }
            catch (InvalidDataException)
            {
                Console.WriteLine("Please enter valid input");
                EmployeeData();
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("with out enter anything it is not possible to search data  \n So please enter valid data");
                EmployeeData();

            }
            catch (FormatException)
            {
                Console.WriteLine("you are trying with string are null values \n So please enter valid data");
                EmployeeData();
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter valid input");
                EmployeeData();
            }
            do
            {
                Console.WriteLine("PRESS 1 : Employee Menu");
                Console.WriteLine("PRESS 2 : Main menu");
                int a = Convert.ToInt32(Console.ReadLine());
                if (a == 1)
                {
                    EmployeeData();
                }
                else if (a == 2)
                {
                    Mainmenu();
                }
                else
                {
                    Console.WriteLine("please Enter only 1 or 2");
                    Console.WriteLine();
                    validation = true;
                }
            }while(validation);
        }

        // Project Data
        public void ProjectData()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine(" PRESS 1 : The Project details using department Id ");
                Console.WriteLine(" PRESS 2 : The Project details using department Name  ");
                Console.WriteLine(" PRESS 3 : The All Project details  3 : ");

                int? projectSearch = Convert.ToInt32(Console.ReadLine());
                if (projectSearch == null)
                {
                    throw new ArgumentNullException();
                }
                switch (projectSearch)
                {
                    case 1:
                        //the list of projects there for the department Id. 
                        do
                        {
                             Console.WriteLine("Enter department id");
                             int deptId = Convert.ToInt32(Console.ReadLine());
                             if (deptId <= 0)
                             {
                                 throw new InvalidDataException();
                             }
                             if (deptId == null)
                             {
                                 throw new ArgumentNullException();
                             }
                             Console.WriteLine(" the list of projects there for the department Id. ");
                             Console.WriteLine();
                             var getProjectData = projectManagementService.GetProjectsData(deptId);
                             if (CheckData(getProjectData))
                             {
                                 projectManagementService.DisplayData(getProjectData);
                                 validation = false;
                             }
                             else
                             {
                                 Console.WriteLine("Data Not Found Please enter valid input ");
                                 validation = true;
                             }
                             Console.WriteLine();
                        } while (validation) ;
                          break;
                    case 2:
                        //The list of projects there for the Department Name  
                        do
                        {
                               Console.WriteLine(" Enter Department Name (Marketing,Finance,Accounting) ");
                               Console.WriteLine();
                               string deptName = Console.ReadLine()?.ToLower();
                               if (String.IsNullOrEmpty(deptName))
                               {
                                   throw new ArgumentNullException();
                               }
                               Console.WriteLine(" the list of projects there for the Department Name  ");
                               var ProjectDataName = projectManagementService.GetProjectsData(deptName: deptName);
                               if (CheckData(ProjectDataName))
                               {
                                   projectManagementService.DisplayData(ProjectDataName);
                               }
                               else
                               {
                                   Console.WriteLine("Data Not Found Please enter valid input ");
                                   validation = true;
                               }
                               Console.WriteLine();
                         } while (validation) ;
                           break;
                    case 3:
                        //All projects for each department 

                        Console.WriteLine(" all projects for each department ");
                        Console.WriteLine();
                        var getProject = projectManagementService.GetProjectsData();
                        projectManagementService.DisplayData(getProject);
                        Console.WriteLine();
                        break;
                    default: throw new ArgumentOutOfRangeException("Please Enter on 1 to 3 numbers only");
                }
            }
            catch (ArgumentOutOfRangeException arex)
            {
                Console.WriteLine("Please enter valid input");
                Console.WriteLine(arex.Message);
                ProjectData();
            }
            catch (InvalidDataException)
            {
                Console.WriteLine("Please enter valid input");
                ProjectData();
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("with out enter anything it is not possible to search data  \n So please enter valid data");
                ProjectData();

            }
            catch (FormatException)
            {
                Console.WriteLine("you are trying with string are null values  \n So please enter valid data");
                ProjectData();
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter valid input");
                ProjectData();
            }
            do
            { 
                Console.WriteLine("PRESS 1 : Project Menu");
                Console.WriteLine("PRESS 2 : Main menu");
                int a = Convert.ToInt32(Console.ReadLine());
                if (a == 1)
                {
                    ProjectData();
                }
                else if (a == 2)
                {
                    Mainmenu();
                }
                else
                {
                    Console.WriteLine("please Enter only 1 or 2");
                    Console.WriteLine();
                    validation = true;
                }
            }while(validation);
        }

        // the number of employees working in each department 
        public void EmployeeCount()
        {
            Console.WriteLine("the number of employees working for each department");
            Console.WriteLine();
            var employeCount = projectManagementService.GetEmployeeCount();
            projectManagementService.DisplayData(employeCount);
            Console.WriteLine();
        }

        // the total salary paid for each department.
        public void DepartmentSalary()
        {
            Console.WriteLine();
            Console.WriteLine("The total salary paid for each department");
            Console.WriteLine();
            var departmentSalary = projectManagementService.GetDepartmentSalary();
            projectManagementService.DisplayData(departmentSalary);
            Console.WriteLine();
        }

        // ProjectResourseDetails(DepartmentName, ProjectName, AssignmentName, EmployeeName) 
        public void ProjectResourseDetails()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine(" PRESS 1 : The  ProjectResourse details using department Id ");
                Console.WriteLine(" PRESS 2 : The ProjectResourse details using department Name  ");
                Console.WriteLine(" PRESS 3 : In ProjectResourse detailsPlease do you want search text : ");
                Console.WriteLine(" PRESS 4 : The All ProjectResourse details ");
                int projectResoursesearch = Convert.ToInt32(Console.ReadLine());
                if (projectResoursesearch == null)
                {
                    throw new ArgumentNullException();
                }
                switch (projectResoursesearch)
                {
                    case 1:
                        // ProjectResourse Details using department id
                        do
                        {
                              Console.WriteLine("Enter Department Id  which Department  ProjectResourseDetails you want");
                              int? deptId = Convert.ToInt32(Console.ReadLine());
                              if (deptId < 0)
                              {
                                  throw new InvalidDataException();
                              }
                              if (deptId == null)
                              {
                                  Console.WriteLine("Data Not Found Please enter valid input ");
                                  EmployeeCount();
                              }
                              Console.WriteLine($"Department wise using DepartmentId is {deptId}");
                              Console.WriteLine();
                              var combineDataId = projectManagementService.GetCombineData(deptId: deptId);
                              if (CheckData(combineDataId))
                              {
                                  projectManagementService.DisplayData(combineDataId);
                                  validation = false;
                              }
                              else
                              {
                                  Console.WriteLine("Data Not Found Please enter valid input ");
                                  validation = true;
                              }
                              Console.WriteLine();
                        } while (validation) ;
                          break;

                    case 2:
                        // ProjectResourse Details using department name
                        do
                        { 
                              Console.WriteLine("Enter Department name  which Department  ProjectResourseDetails you want");
                              string departmentName = Console.ReadLine().ToLower();
                              if (String.IsNullOrEmpty(departmentName))
                              {
                                  throw new ArgumentNullException();
                              }

                              Console.WriteLine($"Department wise using DepartmentName Text is {departmentName}");
                              Console.WriteLine();
                              var combineDataName = projectManagementService.GetCombineData(deptName: departmentName);
                              if (CheckData(combineDataName))
                              {
                                  projectManagementService.DisplayData(combineDataName);
                                  validation = false;
                              }
                              else
                              {
                                  Console.WriteLine("Data Not Found Please enter valid input ");
                                  validation = true;
                              }
                         } while (validation) ;
                           break;
                    case 3:

                        // ProjectResourse Details using text
                        
                        do
                        {
                             Console.WriteLine("Enter searching  Text");
                             string searchText = Console.ReadLine()?.ToLower();
                             if (String.IsNullOrEmpty(searchText))
                             {
                                 throw new ArgumentNullException();
                             }
                             Console.WriteLine($"search the result by {searchText} text");
                             Console.WriteLine();
                             var searchData = projectManagementService.GetSearchData(searchText);
                             if(CheckData(searchData))
                             {
                                 projectManagementService.DisplayData(searchData);
                                     validation = false;
                                 }
                             else
                             {
                                 Console.WriteLine("Data Not Found Please enter valid input ");
                                validation = true;
                             }
                        } while (validation);
                          break;
                    case 4:

                        // ALL ProjectResourseDetails 
                        var projectResoursedata = projectManagementService.GetDeatils();
                        Console.WriteLine();
                        projectManagementService.DisplayData(projectResoursedata);
                        break;
                    default: throw new ArgumentOutOfRangeException("Please Enter on 1 to 4 numbers only");
                }
            }
            catch (ArgumentOutOfRangeException arex)
            {
                Console.WriteLine("Please enter valid input");
                Console.WriteLine(arex.Message);
                ProjectResourseDetails();
            }
            catch (InvalidDataException )
            {
                Console.WriteLine("Please enter valid input");
                ProjectResourseDetails();
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("with out enter anything it is not possible to search data  \n So please enter valid data");
                ProjectResourseDetails();
            }
            catch (FormatException)
            {
                Console.WriteLine("you are trying with string are null values  \n So please enter valid data");
                ProjectResourseDetails();
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter valid input");
                ProjectResourseDetails();
            }
            do
            { 
                Console.WriteLine("PRESS 1 : ProjectResourseDetailst Menu");
                Console.WriteLine("PRESS 2 : Main menu");
                int a = Convert.ToInt32(Console.ReadLine());
                if (a == 1)
                {
                    ProjectResourseDetails();
                }
                else if (a == 2)
                {
                    Mainmenu();
                }
                else
                {
                    Console.WriteLine("please Enter only 1 or 2");
                    Console.WriteLine();
                    validation = true;
                }
            }while(validation);
        }

        // Check the  searching data found or not
        public bool CheckData<t>(IEnumerable<t> CollecatedData)
        {
            if (CollecatedData.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
