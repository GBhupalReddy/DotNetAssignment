namespace ProjectManagment.Core.Model
{
    public class DepartmentSalary
    {
        public int DepartmentId { get; set; }
        public decimal DepartmentTotalSalary { get; set; }

        public override string ToString()
        {
            return $"{DepartmentId} {DepartmentTotalSalary}";
        }
    }
}
