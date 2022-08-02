namespace ProjectManagment.Core.Entities
{
    public class Assignment
    {
        public int ProjectId { get; set; }
        public int EmployeeNumber { get; set; }

        public int HoursWorked { get; set; }

        public string? AssignmentName { get; set; }
    }
}
