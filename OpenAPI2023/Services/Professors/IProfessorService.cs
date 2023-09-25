using OpenAPI2023.Data.Entities;
using OpenAPI2023.Data.Exceptions;

namespace OpenAPI2023.Services.Professors
{
    public interface IProfessorService : ICrudService<Professor, int>
    {
        /// <summary>
        /// Updates the students for a professor.
        /// This is a complete replacement for the related students and needs to include all students.
        /// Business rule: A professor can supervise at most 5 students at a time.
        /// </summary>
        /// <param name="professorId"></param>
        /// <param name="studentIds"></param>
        /// <returns></returns>
        /// <exception cref="EntryPointNotFoundException"></exception>
        /// <exception cref="EntityValidationException"></exception>
        Task UpdateStudentsAsync(int professorId, int[] studentIds);

        /// <summary>
        /// Updates the subjects for a professor.
        /// This is a complete replacement for the related subjects and needs to include all subjects.
        /// Business rule: A professor can teach at most 3 subjects at a time.
        /// </summary>
        /// <param name="professorId"></param>
        /// <param name="subjectIds"></param>
        /// <returns></returns>
        /// <exception cref="EntryPointNotFoundException"></exception>
        /// <exception cref="EntityValidationException"></exception>
        Task UpdateSubjectsAsync(int professorId, int[] subjectIds);

        /// <summary>
        /// Gets the subjects for a professor. 
        /// If there are no subjects, no exception is thrown, an empty set is returned.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <returns></returns>
        Task<ICollection<Subject>> GetSubjectsAsync(int id);

        /// <summary>
        /// Gets the students for a professor. 
        /// If there are no students, no exception is thrown, an empty set is returned.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <returns></returns>
        Task<ICollection<Student>> GetStudentsAsync(int id);
    }
}
