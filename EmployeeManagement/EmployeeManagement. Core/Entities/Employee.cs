namespace EmployeeManagement._Core.Entities
{
    public class Employee
    {
        public int EmployeeNumber { get; set; }
        public string? EmployeeName { get; set; }
        public String? FirstName { get; set; }
        public string? LastName { get; set; }
        public int DepartmentId { get; set; }
        public double Phone { get; set; }
        public string? Email { get; set; }
        public double Salary { get; set; }

        public override string? ToString()
        {
            return $"{EmployeeNumber}\t { EmployeeName} \t { FirstName} \t {LastName} \t { DepartmentId} \t {Phone} \t  { Email} \t { Salary}";
        }



    }
}
