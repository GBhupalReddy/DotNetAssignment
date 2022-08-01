namespace EmployeeManagement._Core.Model
{
    public class EmployeeCount
    {
        public int DepartmentId { get; set; }
        public int TotalEmployee { get; set; }
        public override string ToString()
        {
            return $"{DepartmentId} {TotalEmployee}";
        }
    }
}
