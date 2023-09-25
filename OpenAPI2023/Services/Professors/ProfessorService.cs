using Microsoft.EntityFrameworkCore;
using OpenAPI2023.Data;
using OpenAPI2023.Data.Entities;
using OpenAPI2023.Data.Exceptions;

namespace OpenAPI2023.Services.Professors
{
    public class ProfessorService : IProfessorService
    {
        // DI for DbContext
        private readonly PostgradDbContext _context;

        public ProfessorService(PostgradDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Professor>> GetAllAsync()
        {
            return await _context.Professors.Include(p => p.Subjects).ToListAsync();
        }

        public async Task<Professor> GetByIdAsync(int id)
        {
            var prof = await _context.Professors.Where(p => p.Id == id)
                .Include(p => p.Students)
                .Include(p => p.Subjects)
                .FirstAsync();

            if (prof is null)
                throw new EntityNotFoundException(nameof(prof), id);

            return prof;
        }

        public async Task<Professor> AddAsync(Professor obj)
        {
            await _context.Professors.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task DeleteByIdAsync(int id)
        {
            // Early exit if prof doesnt exist
            if (!await ProfessorExistsAsync(id))
                throw new EntityNotFoundException(nameof(Professor), id);

            // We do the null check in the controller
            var prof = await _context.Professors
                .Where(p => p.Id == id)
                .FirstAsync();

            // Want to remove related entities to not cause referential problems
            // https://learn.microsoft.com/en-us/ef/core/saving/cascade-delete#severing-a-relationship
            // Only works if relationship is nullable
            prof.Students.Clear();
            prof.Subjects.Clear();
            // Can safely remove professor
            _context.Professors.Remove(prof); // Doesnt seem to be a RemoveAsync method
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Student>> GetStudentsAsync(int id)
        {
            // Early exit if prof doesnt exist
            if (!await ProfessorExistsAsync(id))
                throw new EntityNotFoundException(nameof(Professor), id);

            // Its a smaller SQL statement if we go from students (no joining)
            return await _context.Students
                .Where(s => s.ProfessorId == id)
                .ToListAsync();
        }

        public async Task<ICollection<Subject>> GetSubjectsAsync(int id)
        {
            // Early exit if prof doesnt exist
            if (!await ProfessorExistsAsync(id))
                throw new EntityNotFoundException(nameof(Professor), id);

            // Its a smaller SQL statement if we go from subjects (no joining)
            return await _context.Subjects
                .Where(s => s.ProfessorId == id)
                .ToListAsync();
        }

        public async Task<Professor> UpdateAsync(Professor obj)
        {
            if (!await ProfessorExistsAsync(obj.Id))
                throw new EntityNotFoundException(nameof(Professor), obj.Id);

            // We want to force updating related entities in another endpoint
            obj.Students.Clear();
            obj.Students.Clear();
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();

            return obj;
        }

        public async Task UpdateStudentsAsync(int professorId, int[] studentIds)
        {
            // Check business logic rule
            if(studentIds.Length > 5)
                throw new EntityValidationException("A professor can only supervise 5 students.");

            // Check if professor exists
            if (!await ProfessorExistsAsync(professorId))
                throw new EntityNotFoundException(nameof(Professor), professorId);

            var professor = await _context.Professors.FindAsync(professorId);

            var students = new List<Student>();

            // Check if students exist, then add to the list
            foreach (int id in studentIds)
            {
                if(!await StudentExistsAsync(id))
                    throw new EntityNotFoundException(nameof(Student), id);

                students.Add(await _context.Students
                    .Where(s => s.Id == id)
                    .FirstAsync());
            }

            professor.Students = students;
            await _context.SaveChangesAsync();
        }
 
        public async Task UpdateSubjectsAsync(int professorId, int[] subjectIds)
        {
            // Check business logic rule
            if (subjectIds.Length > 3)
                throw new EntityValidationException("A professor can only teach 3 subjects.");

            // Check if professor exists
            if (!await ProfessorExistsAsync(professorId))
                throw new EntityNotFoundException(nameof(Professor), professorId);

            var professor = await _context.Professors.FindAsync(professorId);

            var subjects = new List<Subject>();

            // Check if students exist, then add to the list
            foreach (int id in subjectIds)
            {
                if (!await StudentExistsAsync(id))
                    throw new EntityNotFoundException(nameof(Student), id);

                subjects.Add(await _context.Subjects
                    .Where(s => s.Id == id)
                    .FirstAsync());
            }

            professor.Subjects = subjects;
            await _context.SaveChangesAsync();
        }

        // Hjelper meg methods
        private async Task<bool> ProfessorExistsAsync(int id)
        {
            return await _context.Professors.AnyAsync(p => p.Id == id);
        }

        private Task<bool> StudentExistsAsync(int studentId)
        {
            return _context.Students.AnyAsync(s => s.Id == studentId);
        }

        private Task<bool> SubjectExistsAsync(int subjectId)
        {
            return _context.Subjects.AnyAsync(s => s.Id == subjectId);
        }
    }
}
