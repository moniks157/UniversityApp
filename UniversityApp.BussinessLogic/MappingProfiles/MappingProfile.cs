using AutoMapper;
using UniversityApp.BussinessLogic.DomainModels;
using UniversityApp.DataAccess.Entities;
using UniversityApp.DataAccess.Models;

namespace UniversityApp.BussinessLogic.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StudentDomainModel, Student>();
            CreateMap<Student, StudentDomainModel>();

            CreateMap<GradeDomainModel, Grade>();
            CreateMap<Grade, GradeDomainModel>();

            CreateMap<StudentSearchParametersDomainModel, StudentSearchParameters>();
        }
    }
}
