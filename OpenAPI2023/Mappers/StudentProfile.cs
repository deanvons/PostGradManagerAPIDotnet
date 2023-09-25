using AutoMapper;
using OpenAPI2023.Data.Dtos.Students;
using OpenAPI2023.Data.Entities;

namespace OpenAPI2023.Mappers
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentDTO>()
                .ForMember(sdto => sdto.Professor, opt => opt
                    .MapFrom(s => s.ProfessorId))
                .ForMember(sdto => sdto.Subjects, opt => opt
                    .MapFrom(sd => sd.Subjects.Select(sj => sj.Id)));
        }
    }
}
