using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeePlayGround.Core.Entities
{
    public class EntitiesBase
    {
        [Column(Order = 0)]
        public int Id { get; set; }
        [Column(Order = 1)]
        [Required]
        public string Name { get; set; }
    }
}
