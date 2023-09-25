using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OpenAPI2023.Data.Dtos.Professors;
using OpenAPI2023.Data.Dtos.Students;
using OpenAPI2023.Data.Dtos.Subjects;
using OpenAPI2023.Data.Entities;
using OpenAPI2023.Data.Exceptions;
using OpenAPI2023.Services.Professors;
using System.Net.Mime;

namespace OpenAPI2023.Controllers
{
    [Route("api/v1/professors")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ProfessorsController : ControllerBase
    {
        // Want to separate out our concerns
        // This lets us keep data access, business logic, mapping, and user interaction separate
        private readonly IProfessorService _profService;
        private readonly IMapper _mapper;

        public ProfessorsController(IProfessorService profService, IMapper mapper)
        {
            _profService = profService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all the professors.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfessorDTO>>> GetProfessors()
        {
            // Automapper will reuse the single mapping for collecitons. We dont need to define another mapping
            return Ok(_mapper
                .Map<IEnumerable<ProfessorDTO>>(
                    await _profService.GetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfessorDTO>> GetProfessor(int id)
        {
            try
            {          
                return Ok(_mapper
                    .Map<ProfessorDTO>(
                        await _profService.GetByIdAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfessor(int id, ProfessorPutDTO professor)
        {
            if (id != professor.Id)
            {
                return BadRequest();
            }

            try
            {
                await _profService.UpdateAsync(_mapper.Map<Professor>(professor));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ProfessorDTO>> PostProfessor(ProfessorPostDTO professor)
        {     
            var newProf = await _profService.AddAsync(_mapper.Map<Professor>(professor));

            return CreatedAtAction("GetProfessor", 
                new { id = newProf.Id }, 
                _mapper.Map<ProfessorDTO>(newProf));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfessor(int id)
        {
            try
            {
                await _profService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}/students")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents(int id)
        {
            try
            {
                return Ok(_mapper
                    .Map<IEnumerable<StudentDTO>>(
                        await _profService.GetStudentsAsync(id)));
            } 
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}/subjects")]
        public async Task<ActionResult<IEnumerable<SubjectDTO>>> GetSubjects(int id)
        {
            try
            {
                return Ok(_mapper
                    .Map<IEnumerable<SubjectDTO>>(
                        await _profService.GetSubjectsAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}/students")]
        public async Task<IActionResult> UpdateStudents(int id, [FromBody] int[] students)
        {
            try
            {
                await _profService.UpdateStudentsAsync(id, students);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (EntityValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/subjects")]
        public async Task<IActionResult> UpdateSubjects(int id, [FromBody] int[] subjects)
        {
            try
            {
                await _profService.UpdateSubjectsAsync(id, subjects);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (EntityValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
