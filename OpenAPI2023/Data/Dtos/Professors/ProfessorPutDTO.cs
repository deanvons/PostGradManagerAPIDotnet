using System.ComponentModel.DataAnnotations;

namespace OpenAPI2023.Data.Dtos.Professors
{
    public class ProfessorPutDTO
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public string Field { get; set; }
    }
}
