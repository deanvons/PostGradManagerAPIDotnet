using AutoMapper;
using OpenAPI2023.Data.Dtos.Professors;
using OpenAPI2023.Data.Entities;

namespace OpenAPI2023.Mappers
{
    public class ProfessorProfile : Profile
    {
        public ProfessorProfile() 
        {
            CreateMap<Professor, ProfessorPostDTO>().ReverseMap();

            // ReverseMap is pointless here as we never do it, and shouldnt do it. 
            // As it would involve turning ids into entities.
            CreateMap<Professor,ProfessorDTO>()
                .ForMember(pdto => pdto.Students, options => options
                    .MapFrom(p => p.Students.Select(s => s.Id).ToArray()))
                .ForMember(pdto => pdto.Subjects, options => options
                    .MapFrom(p => p.Subjects.Select(s => s.Id).ToArray()));

            CreateMap<Professor, ProfessorPutDTO>().ReverseMap();
        }
    }
}
