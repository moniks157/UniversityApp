using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityApp.BussinessLogic.DomainModels;
using UniversityApp.DTOs;

namespace UniversityApp.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StudentDomainModel, StudentDto>();
            CreateMap<StudentDto, StudentDomainModel>();

            CreateMap<GradeDomainModel, GradeDto>();
            CreateMap<GradeDto, GradeDomainModel>();
        }
    }
}
