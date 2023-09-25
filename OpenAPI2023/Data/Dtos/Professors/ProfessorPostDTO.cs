using System.ComponentModel.DataAnnotations;

namespace OpenAPI2023.Data.Dtos.Professors
{
    // We can replicate validation on the DTOs that consume what the client provides (post/put)
    // We dont need it on the DTOs used to display db data, as its already validated.
    public class ProfessorPostDTO
    {
        [StringLength(50)]
        public string Name { get; set; }
        public string Field { get; set; }
    }
}
