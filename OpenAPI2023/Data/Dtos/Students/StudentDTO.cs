using System.ComponentModel.DataAnnotations;

namespace OpenAPI2023.Data.Dtos.Students
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Email { get; set; }
        public int? Professor { get; set; }
        public int[] Subjects { get; set; }
    }
}
