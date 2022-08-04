namespace ProjectManagment.Core.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public int DepartmentId { get; set; }
        public int MaxHours { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public override string? ToString()
        {
            return $"{ProjectId}\t {DepartmentId} \t {MaxHours} \t {StartDate}  \t {EndDate}   \t {ProjectName}";
        }

    }
}
