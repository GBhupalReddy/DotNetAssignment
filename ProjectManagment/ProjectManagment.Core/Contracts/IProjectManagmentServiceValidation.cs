namespace ProjectManagment.Infrastructure.Services
{
    public interface IProjectManagmentServiceValidation
    {
        public bool CheckData<t>(IEnumerable<t> CollecatedData);
        public void DepartmentData();
        public void DepartmentSalary();
        public void EmployeeCount();
        public void EmployeeData();
        public void Mainmenu();
        public void ProjectData();
        public void ProjectResourseDetails();
    }
}