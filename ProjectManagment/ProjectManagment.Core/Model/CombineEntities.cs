namespace ProjectManagment.Core.Model
{
    public class CombineEntities
    {
        public string? DepartmentName { get; set; }
        public string? EmployeeName { get; set; }
        public string? ProjectName { get; set; }
        public string? AssignmentName { get; set; }

        public override string? ToString()
        {
            return $"{DepartmentName} \t {ProjectName}  \t {AssignmentName} \t {EmployeeName}";
        }
    }
}
