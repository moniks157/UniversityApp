using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityApp.BussinessLogic.DomainModels;
using UniversityApp.BussinessLogic.Services.Interfaces;
using UniversityApp.Controllers;
using UniversityApp.DTOs;
using UniversityApp.MappingProfiles;
using UniversityApp.Validators;

namespace UniversityApp.Tests.Controllers
{
    [TestFixture]
    public class StudentControllerTests
    {
        private IStudentsService studentsServiceMock;
        private IValidator<StudentDto> studentValidator;
        private IValidator<GradeDto> gradeValidator;
        private IValidator<StudentSearchParametersDto> studentSearchParametersValidator;
        private IMapper mapper;

        [SetUp]
        public void SetUp()
        {
            studentsServiceMock = Substitute.For<IStudentsService>();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            mapper = mapperConfig.CreateMapper();
            studentValidator = new StudentValidator();
            gradeValidator = new GradeValidator(studentsServiceMock);
            studentSearchParametersValidator = new StudentSearchParametersValidator();
        }

        [Test]
        public async Task GetStudent_Should_ReturnNotFound_When_StudentDoesntExists([Random(1)] int id)
        {
            //Arrange
            studentsServiceMock.GetStudent(Arg.Any<int>()).ReturnsNull();
            var contorller = new StudentsController(studentsServiceMock, mapper, studentValidator, gradeValidator, studentSearchParametersValidator);

            //Act
            var result = await contorller.GetStudent(id);

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task GetStudent_Should_ReturnStudent_When_StudentExists([Random(1)] int id)
        {
            //Arrange
            var expectedStudent = new StudentDomainModel
            {
                FirstName = "Adam",
                LastName = "Ptak",
                Age = 24,
                IsAdult = true,
                Gender = "M"
            };

            studentsServiceMock.GetStudent(Arg.Any<int>()).Returns(expectedStudent);
            var contorller = new StudentsController(studentsServiceMock, mapper, studentValidator, gradeValidator,studentSearchParametersValidator);

            //Act
            var result = await contorller.GetStudent(id) as OkObjectResult;

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult).Value.Should().BeEquivalentTo(expectedStudent, options => options.ComparingByValue<StudentDto>().ExcludingMissingMembers());
        }

        [Test]
        public async Task Get_Should_ReturnExpectedListOfStudents_When_StudentsExist()
        {
            //Arrange
            var expectedStudents = new List<StudentDomainModel>()
            {   new StudentDomainModel
                {
                    FirstName = "Adam",
                    LastName = "Ptak",
                    Age = 24,
                    IsAdult = true,
                    Gender = "M"
                },
                new StudentDomainModel
                {
                    FirstName = "Lola",
                    LastName = "Tak",
                    Age = 23,
                    IsAdult = true,
                    Gender = "K"
                }
            };

            studentsServiceMock.GetStudents().Returns(expectedStudents);
            var controller = new StudentsController(studentsServiceMock, mapper, studentValidator, gradeValidator, studentSearchParametersValidator);
            
            //Act
            var result = await controller.Get();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult).Value.Should().BeOfType<List<StudentDto>>();
            (result as OkObjectResult).Value.Should().BeEquivalentTo(expectedStudents, options => options.ComparingByValue<StudentDto>().ExcludingMissingMembers());
        }

        [Test]
        public async Task Search_Should_ReturnBadRequest_When_SearchParametersAreNotValid()
        {
            //Arrange
            var searchParams = new StudentSearchParametersDto()
            {
                FirstName = "Ala",
                PageNumber = -1,
                PageSize = 10
            };
            var controller = new StudentsController(studentsServiceMock, mapper, studentValidator, gradeValidator, studentSearchParametersValidator);

            //Act
            var result = await controller.Search(searchParams);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task Search_Should_ReturnOk_When_SearchParametersAreValid()
        {
            //Arrange
            var searchParams = new StudentSearchParametersDto()
            {
                FirstName = "Ala",
                PageNumber = 1,
                PageSize = 10
            };

            var expectedResult = new List<StudentDomainModel>(){
                new StudentDomainModel()
                {
                    FirstName = "Alaksandra",
                    LastName = "Xx",
                    Age = 20,
                    IsAdult = true,
                    Gender = "K"
                },
                new StudentDomainModel()
                {
                    FirstName = "Alax",
                    LastName = "Xx",
                    Age = 20,
                    IsAdult = true,
                    Gender = "M"
                }
            };
            studentsServiceMock.SearchStudents(Arg.Any<StudentSearchParametersDomainModel>()).Returns((expectedResult, expectedResult.Count));
            var controller = new StudentsController(studentsServiceMock, mapper, studentValidator, gradeValidator, studentSearchParametersValidator);

            //Act
            var result = await controller.Search(searchParams);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult).Value.Should().BeEquivalentTo((expectedResult, expectedResult.Count), options => options.ComparingByValue<StudentDto>().ExcludingMissingMembers());
        }

        [Test]
        public async Task Post_Should_ReturnCreated_When_CorrectStudentData()
        {
            //Arrange
            var createStudentData = new StudentDto
            {
                FirstName = "Ala",
                LastName = "Doga",
                Age = 17,
                IsAdult = false,
                Gender = "K"
            };
            var expectedId = 10;

            studentsServiceMock.AddStudent(Arg.Any<StudentDomainModel>()).Returns(expectedId);
            var controller = new StudentsController(studentsServiceMock, mapper, studentValidator, gradeValidator,studentSearchParametersValidator);

            //Act
            var result = await controller.Post(createStudentData);

            //Assert
            result.Should().BeOfType<CreatedResult>();
            (result as CreatedResult).Value.Should().Be(expectedId);
        }

        [Test]
        public async Task Post_Should_ReturnBadRequest_When_NotCorrectStudentData()
        {
            //Arrange
            var createStudentData = new StudentDto
            {
                FirstName = "Ala123",
                LastName = "Doga",
                Age = 17,
                IsAdult = false,
                Gender = "K"
            };

            var controller = new StudentsController(studentsServiceMock, mapper, studentValidator, gradeValidator, studentSearchParametersValidator);
            
            //Act
            var result = await controller.Post(createStudentData);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task Put_Should_ReturnBadRequest_When_StudentDoesntExist([Random(1)]int id)
        {
            //Arrange
            var editStudentData = new StudentDto();

            studentsServiceMock.UpdateStudent(Arg.Any<int>(), Arg.Any<StudentDomainModel>()).Returns(false);
            var controller = new StudentsController(studentsServiceMock, mapper, studentValidator, gradeValidator,studentSearchParametersValidator);

            //Act
            var result = await controller.Put(id, editStudentData);

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Test]
        public async Task Put_Should_ReturnOk_When_StudentExists([Random(1)] int id)
        {
            //Arrange
            var editStudentData = new StudentDto();

            studentsServiceMock.UpdateStudent(Arg.Any<int>(), Arg.Any<StudentDomainModel>()).Returns(true);
            var controller = new StudentsController(studentsServiceMock, mapper, studentValidator, gradeValidator,studentSearchParametersValidator);

            //Act
            var result = await controller.Put(id, editStudentData);

            //Assert
            result.Should().BeOfType<OkResult>();
        }

        [Test]
        public async Task PostGrade_Should_ReturnBadRequest_When_StudentDoesntExist([Random(1)] int id)
        {
            //Arrange
            var gradeToAdd = new GradeDto()
            {
                Value = 3,
                Description = "some description"
            };
            studentsServiceMock.DoesStudentExists(Arg.Any<int>()).Returns(false);
            var controller = new StudentsController(studentsServiceMock, mapper, studentValidator, gradeValidator,studentSearchParametersValidator);

            //Act
            var result = await controller.PostGrade(id, gradeToAdd);

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Test]
        public async Task PostGrade_Should_ReturnBadRequest_When_NotCorrectGradeData([Random(1)] int id)
        {
            //Arrange
            var gradeToAdd = new GradeDto()
            {
                Value = 1,
                Description = "some description"
            };

            var controller = new StudentsController(studentsServiceMock, mapper, studentValidator, gradeValidator,studentSearchParametersValidator);

            //Act
            var result = await controller .PostGrade(id, gradeToAdd);

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Test]
        public async Task PostGrade_Should_ReturnCreatedGradeAdress_When_CorrectGradeData([Random(1)] int id)
        {
            //Arrange
            var gradeToAdd = new GradeDto()
            {
                Value = 3,
                Description = "some description"
            };
            var expectedId = 10;
            studentsServiceMock.AddStudentGrade(Arg.Any<GradeDomainModel>()).Returns(expectedId);
            
            var controller = new StudentsController(studentsServiceMock, mapper, studentValidator, gradeValidator ,studentSearchParametersValidator);

            //Act
            var result = await controller.PostGrade(id, gradeToAdd);

            //Assert
            result.Should().BeOfType<CreatedResult>();
            (result as CreatedResult).Location.Should().Be($"~api/students/{id}/grades/{expectedId}");
        }
    }
}
