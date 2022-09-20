using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeePlayGround.Core.Entities
{
    [Table("Employee", Schema = "Emp")]
    public class Employee : EntitiesBase
    {
        
        [Column(Order =2,TypeName = "decimal(10, 2)")]
        public decimal Salary { get; set; }

        [Column(Order =3)]
        [Required]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }
    }
}
