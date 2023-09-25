using System.ComponentModel.DataAnnotations;

namespace OpenAPI2023.Data.Dtos.Subjects
{
    public class SubjectDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Title { get; set; } = null!;
        public int? Professor { get; set; }
        public int[] Students { get; set; }
    }
}
