using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeePlayGround.Core.Entities
{
    [Table("Department",Schema ="Emp")]
    public class Department : EntitiesBase
    {
        public List<Employee> employees { get; set; }

    }
}
