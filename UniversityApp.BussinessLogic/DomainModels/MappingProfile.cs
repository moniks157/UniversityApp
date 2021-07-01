using AutoMapper;
using UniversityApp.DataAccess.Entities;

namespace UniversityApp.BussinessLogic.DomainModels
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StudentDomainModel, Student>();
            CreateMap<Student, StudentDomainModel>();

            CreateMap<GradeDomainModel, Grade>();
            CreateMap<Grade, GradeDomainModel>();
        }
    }
}
