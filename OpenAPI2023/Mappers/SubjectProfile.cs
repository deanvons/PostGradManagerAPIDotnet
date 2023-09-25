using AutoMapper;
using OpenAPI2023.Data.Dtos.Subjects;
using OpenAPI2023.Data.Entities;

namespace OpenAPI2023.Mappers
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile() 
        { 
            CreateMap<Subject, SubjectDTO>()
                .ForMember(sdto => sdto.Professor, opt => opt
                    .MapFrom(s => s.ProfessorId))
                .ForMember(sdto => sdto.Students, opt => opt
                    .MapFrom(sj => sj.Students.Select(sd => sd.Id)));
        }
    }
}
