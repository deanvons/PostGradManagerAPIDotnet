using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OpenAPI2023.Data.Entities
{
    [Table(nameof(Professor))]
    public class Professor
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;
        public string? Field { get; set; }
        // Navigation
        public ICollection<Student> Students { get; set; } // 1-M
        public ICollection<Subject> Subjects { get; set; } // 1-M
    }
}
