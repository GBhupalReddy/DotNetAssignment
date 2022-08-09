namespace ProjectManagment.Infrastructure.Services
{
    public interface IProjectManagmentServiceValidation
    {
        void CheckData<t>(IEnumerable<t> CollecatedData);
        void DepartmentData();
        void DepartmentSalary();
        void EmployeeCount();
        void EmployeeData();
        void Mainmenu();
        void ProjectData();
        void ProjectResourseDetails();
    }
}