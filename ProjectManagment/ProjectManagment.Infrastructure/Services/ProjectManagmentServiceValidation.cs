namespace ProjectManagment.Infrastructure.Services
{
    public class ProjectManagmentServiceValidation
    {
        ProjectManagmentService projectManagementService = new ProjectManagmentService();

        // The department details 
        public void DepartmentData()
        {

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

                    Console.WriteLine("Enter  department id ");
                    int? deptId = Convert.ToInt32(Console.ReadLine());
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
                    CheckData(getDeparmentData);
                    Console.WriteLine();
                    break;

                case 2:

                    // The department details for using Department Name

                    Console.WriteLine(" Enter Department Name (Marketing,Finance,Accounting) ");
                    Console.WriteLine();
                    string? deptName = Console.ReadLine()?.ToLower();
                    if (String.IsNullOrEmpty(deptName))
                    {
                        throw new ArgumentNullException();
                    }
                    Console.WriteLine("The department details for the Department Name ");
                    var getDeparment = projectManagementService.GetDepartmentDeta(deptName: deptName);
                    CheckData(getDeparment);
                    Console.WriteLine();
                    break;

                case 3:

                    // all Departments  

                    Console.WriteLine(" all departments  ");
                    Console.WriteLine();
                    var deparmentyData = projectManagementService.GetDepartmentDeta();
                    CheckData(deparmentyData);
                    Console.WriteLine();
                    break;

                default: throw new ArgumentOutOfRangeException("Please Enter on 1 to 3 numbers only");

            }
        }

        // Employee

        public void EmployeeData()
        {
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

                    Console.WriteLine("Enter  department id ");
                    int? deptId = Convert.ToInt32(Console.ReadLine());
                    if (deptId <= 0)
                    {
                        throw new InvalidDataException("please enter valid input");
                    }
                    if (deptId.HasValue)
                    {
                        throw new ArgumentNullException();
                    }
                    Console.WriteLine($" the list of employees there for the department Id {deptId} ");
                    Console.WriteLine();
                    var getEmployeeData = projectManagementService.GetEmployeeData(deptId: deptId);
                    CheckData(getEmployeeData);
                    Console.WriteLine();
                    break;

                case 2:

                    // The employee details for using employee number

                    Console.WriteLine("Enter which  employees number");
                    int? empNumber = Convert.ToInt32(Console.ReadLine());
                    if (empNumber <= 0)
                    {
                        throw new InvalidDataException("please enter valid input");
                    }
                    if (empNumber == null)
                    {
                        throw new ArgumentNullException();
                    }
                    Console.WriteLine($" the employees details for the Employee number {empNumber} ");
                    Console.WriteLine();
                    var employeeData = projectManagementService.GetEmployeeData(empNumber: empNumber);
                    CheckData(employeeData);
                    Console.WriteLine();
                    break;

                case 3:

                    // All  employee details  

                    Console.WriteLine(" the all employees details  ");
                    Console.WriteLine();
                    var allEmployeeData = projectManagementService.GetEmployeeData();
                    CheckData(allEmployeeData);
                    Console.WriteLine();
                    break;

                default: throw new ArgumentOutOfRangeException("Please Enter on 1 to 3 numbers only");

            }
        }

        // Project

        public void ProjectData()
        {
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

                    Console.WriteLine("Enter department id");
                    int? deptId = Convert.ToInt32(Console.ReadLine());
                    if (deptId <= 0)
                    {
                        throw new InvalidDataException("please enter valid input");
                    }
                    if (deptId == null)
                    {
                        throw new ArgumentNullException();
                    }
                    Console.WriteLine(" the list of projects there for the department Id. ");
                    Console.WriteLine();
                    var getProjectData = projectManagementService.GetProjectsData(deptId);
                    CheckData(getProjectData);
                    Console.WriteLine();
                    break;

                case 2:

                    //The list of projects there for the Department Name  

                    Console.WriteLine(" Enter Department Name (Marketing,Finance,Accounting) ");
                    Console.WriteLine();
                    string? deptName = Console.ReadLine()?.ToLower();
                    if (String.IsNullOrEmpty(deptName))
                    {
                        throw new ArgumentNullException();
                    }
                    Console.WriteLine(" the list of projects there for the Department Name  ");
                    var ProjectData = projectManagementService.GetProjectsData(deptName: deptName);
                    CheckData(ProjectData);
                    Console.WriteLine();
                    break;

                case 3:

                    //All projects for each department 

                    Console.WriteLine(" all projects for each department ");
                    Console.WriteLine();
                    var getProject = projectManagementService.GetProjectsData();
                    CheckData(getProject);
                    Console.WriteLine();
                    break;

                default: throw new ArgumentOutOfRangeException("Please Enter on 1 to 3 numbers only");

            }
        }

        // the number of employees working for each department 

        public void EmployeeCount()
        {

            Console.WriteLine("the number of employees working for each department");
            Console.WriteLine();
            var employeCount = projectManagementService.GetEmployeeCount();
            CheckData(employeCount);
            Console.WriteLine();

        }
        // the total salary paid for each department.

        public void DepartmentSalary()
        {

            Console.WriteLine();
            Console.WriteLine("The total salary paid for each department");
            Console.WriteLine();
            var departmentSalary = projectManagementService.GetDepartmentSalary();
            CheckData(departmentSalary);
            Console.WriteLine();

        }

        // ProjectResourseDetails(DepartmentName, Project Name, Assignment Name, Employee Name) 
        public void ProjectResourseDetails()
        {
            Console.WriteLine(" PRESS 1 : The  ProjectResourse details using department ");
            Console.WriteLine(" PRESS 2 : The ProjectResourse details using department Name  ");
            Console.WriteLine(" PRESS 3 : In ProjectResourse detailsPlease do you want search text : ");
            Console.WriteLine(" PRESS 4 : The All ProjectResourse details ");
            int? projectResoursesearch = Convert.ToInt32(Console.ReadLine());
            if (projectResoursesearch == null)
            {
                throw new ArgumentNullException();
            }
            switch (projectResoursesearch)
            {
                case 1:

                   // ProjectResourseDetails using department id

                    Console.WriteLine("Enter Department Id  which Department  ProjectResourseDetails you want");
                    int? deptId = Convert.ToInt32(Console.ReadLine());
                    if (deptId < 0)
                    {
                        throw new InvalidDataException("please enter valid input");
                    }
                    if (deptId == null)
                    {
                        throw new ArgumentNullException();
                    }
                    Console.WriteLine($"Department wise using DepartmentId is {deptId}");
                    Console.WriteLine();
                    var combineDataId = projectManagementService.GetCombineData(deptId: deptId);
                    CheckData(combineDataId);
                    Console.WriteLine();
                    break;

                case 2:

                    // ProjectResourseDetails using department name

                    Console.WriteLine("Enter Department name  which Department  ProjectResourseDetails you want");
                    string? departmentName = Console.ReadLine()?.ToLower();
                    if (String.IsNullOrEmpty(departmentName))
                    {
                        throw new ArgumentNullException();
                    }

                    Console.WriteLine($"Department wise using DepartmentName Text is {departmentName}");
                    Console.WriteLine();
                    var combineDataName = projectManagementService.GetCombineData(deptName: departmentName);
                    CheckData(combineDataName);
                    Console.WriteLine();
                    break;

                case 3:

                    // ProjectResourseDetails using text

                    Console.WriteLine("Enter searching  Text");
                    string? searchText = Console.ReadLine()?.ToLower();
                    if (String.IsNullOrEmpty(searchText))
                    {
                        throw new ArgumentNullException();

                    }
                    Console.WriteLine($"search the result by {searchText} text");
                    Console.WriteLine();
                    var searchData = projectManagementService.GetSearchData(searchText);
                    CheckData(searchData);
                    break;

                case 4:

                    // ALL ProjectResourseDetails 

                    var projectResoursedata = projectManagementService.GetDeatils();
                    Console.WriteLine();
                    CheckData(projectResoursedata);
                    break;

                default: throw new ArgumentOutOfRangeException("Please Enter on 1 to 4 numbers only");

            }
        }


        // Check the  searching data found or not


        public void CheckData<t>(IEnumerable<t> CollecatedData)
        {

                if (CollecatedData.Any())
                {
                    projectManagementService.DisplayData(CollecatedData);
                }
                else
                {

                    Console.WriteLine("data not found");
                }

            }
    }
}
