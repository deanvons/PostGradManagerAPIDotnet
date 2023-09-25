using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OpenAPI2023.Data.Entities
{
    [Table(nameof(Subject))]
    public class Subject
    {
        public int Id { get; set; }
        [StringLength(10)]
        public string Code { get; set; } = null!;
        [StringLength(100)]
        public string Title { get; set; } = null!;
        public int? ProfessorId { get; set; }
        // Navigation 
        public ICollection<Student> Students { get; set; } // M-M
        public Professor Professor { get; set; } // M-1
    }
}
