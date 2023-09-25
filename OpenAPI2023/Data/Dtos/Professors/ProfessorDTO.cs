using System.ComponentModel.DataAnnotations;

namespace OpenAPI2023.Data.Dtos.Professors
{
    public class ProfessorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Field { get; set; }
        public int[] Students { get; set; }
        public int[] Subjects { get; set; }
    }
}
