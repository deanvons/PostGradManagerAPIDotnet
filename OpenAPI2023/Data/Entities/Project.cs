using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OpenAPI2023.Data.Entities
{
    [Table(nameof(Project))]
    public class Project
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Title { get; set; } = null!;
        public int StudentId { get; set; }
        // Navigation
        public Student Student { get; set; } // 1-1
    }
}
