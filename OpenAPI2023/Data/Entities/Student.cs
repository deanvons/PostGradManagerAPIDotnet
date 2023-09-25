using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenAPI2023.Data.Entities
{
    [Table(nameof(Student))]
    public class Student
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;
        public DateTime Dob { get; set; }
        public string? Email { get; set; }
        public int? ProfessorId { get; set; }

        // Navigation
        public Project Project { get; set; } // 1-1
        public Professor Professor { get; set; } // M-1
        public ICollection<Subject> Subjects { get; set; } // M-M
    }
}
