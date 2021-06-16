using AutoMapper;
using System;
using UniversityAppBussinessLogic.DTO;
using UniversityDataAccess.Entities;

namespace UniversityAppBussinessLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<Student, StudentDTO>()
                );
        }
    }
}
