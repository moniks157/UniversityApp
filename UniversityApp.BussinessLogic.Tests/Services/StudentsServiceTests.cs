using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityApp.BussinessLogic.DomainModels;
using UniversityApp.BussinessLogic.MappingProfiles;
using UniversityApp.BussinessLogic.Services;
using UniversityApp.DataAccess.Entities;
using UniversityApp.DataAccess.Repositories.Interfaces;

namespace UniversityApp.BussinessLogic.Tests.Services
{
    [TestFixture]
    class StudentsServiceTests
    {
        private IStudentsRepository studentsRepositoryMock;
        private IGradesRepository gradesRepositoryMock;
        private IMapper mapper;

        [SetUp]
        public void SetUp()
        {
            studentsRepositoryMock = Substitute.For<IStudentsRepository>();
            gradesRepositoryMock = Substitute.For<IGradesRepository>();
            var mapperMockConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            mapper = mapperMockConfig.CreateMapper();
        }

        [Test]
        public async Task GetStudent_Should_ReturnsNull_When_StudentDoesntExist([Random(1)] int id)
        {
            //Arrange
            studentsRepositoryMock
                .GetStudent(Arg.Any<int>())
                .ReturnsNull();
            
            var studentsService = new StudentsService(studentsRepositoryMock, gradesRepositoryMock, mapper);

            //Act
            var result = await studentsService.GetStudent(id);

            //Assert
            result.Should().BeNull();
        }

        [Test]
        public async Task GetStudent_Should_ReturnsExpectedStudent_WhenStudentExists([Random(1)] int id)
        {
            //Arrange
            var expectedStudent = new Student
            {
                FirstName = "Adam",
                LastName = "Ptak",
                Age = 24,
                IsAdult = true,
                Gender = "M"
            };

            studentsRepositoryMock.GetStudent(Arg.Any<int>()).Returns(expectedStudent);

            var studentsService = new StudentsService(studentsRepositoryMock, gradesRepositoryMock, mapper);

            //Act
            var result = await studentsService.GetStudent(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedStudent, options => options.ComparingByValue<StudentDomainModel>().ExcludingMissingMembers());
        }

        [Test]
        public async Task GetStudents_Should_ReturnsExpectedStudents_When_Called()
        {
            //Arrange
            var expectedStudents = new List<Student>
            {
                new Student
                {
                    Id = 22,
                    FirstName = "Adam",
                    LastName = "Ptak",
                    Age = 24,
                    IsAdult = true,
                    Gender = "M",
                    Grades = new List<Grade>()
                },
                new Student
                {
                    Id = 14,
                    FirstName = "Lola",
                    LastName = "Tak",
                    Age = 23,
                    IsAdult = true,
                    Gender = "K",
                    Grades = new List<Grade>()
                }
            };

            studentsRepositoryMock.GetStudents().Returns(expectedStudents);

            var studentsService = new StudentsService(studentsRepositoryMock, gradesRepositoryMock, mapper);

            //Act
            var result = await studentsService.GetStudents();

            //Assert
            result.Should().BeEquivalentTo(expectedStudents, options => options.ComparingByValue<StudentDomainModel>().ExcludingMissingMembers());
        }

        [Test]
        public async Task AddStudent_Should_ReturnId_WhenStudentCreated()
        {
            //Arrange
            var expectedID = 1;
            var studentToAdd = new StudentDomainModel();

            studentsRepositoryMock.AddStudent(Arg.Any<Student>()).Returns(expectedID);
            var studentService = new StudentsService(studentsRepositoryMock, gradesRepositoryMock, mapper);

            //Act
            var result = await studentService.AddStudent(studentToAdd);

            //Assert
            result.Should().Be(expectedID);
        }

        [TestCase(1)]
        [TestCase(30)]
        [TestCase(200)]
        public async Task UpdateStudent_Should_ReturnFalse_When_StudentDoesntExist(int id)
        {
            //Arrange
            var studentToUpdate = new StudentDomainModel();
            studentsRepositoryMock.DoesStudentExist(Arg.Any<int>()).Returns(false);
            var studentService = new StudentsService(studentsRepositoryMock, gradesRepositoryMock, mapper);

            //Act
            var result = await studentService.UpdateStudent(id, studentToUpdate);

            //Assert
            result.Should().BeFalse();
        }

        [TestCase(1)]
        [TestCase(30)]
        [TestCase(200)]
        public async Task UpdateStudent_Should_ReturnTrue_When_StudentExists(int id)
        {
            //Arrange
            var studentToUpdate = new StudentDomainModel();
            studentsRepositoryMock.DoesStudentExist(Arg.Any<int>()).Returns(true);
            studentsRepositoryMock.UpdateStudent(Arg.Any<Student>()).Returns(true);
            var studentService = new StudentsService(studentsRepositoryMock, gradesRepositoryMock, mapper);

            //Act
            var result = await studentService.UpdateStudent(id, studentToUpdate);

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task AddStudentGrade_Should_ReturnNull_When_StudentDoesntExist()
        {
            //Arrange
            var gradeToAdd = new GradeDomainModel()
            {
                Value = 3,
                Description = "some description",
                StudentId = 1
            };
            studentsRepositoryMock.DoesStudentExist(Arg.Any<int>()).Returns(false);
            var studentService = new StudentsService(studentsRepositoryMock, gradesRepositoryMock, mapper);

            //Act
            var result = await studentService.AddStudentGrade(gradeToAdd);

            //Assert
            result.Should().BeNull();
        }

        [Test]
        public async Task AddStudentGrade_Should_ReturnGradeId_When_StudentExists()
        {
            //Arrange
            var gradeToAdd = new GradeDomainModel()
            {
                Value = 3,
                Description = "some description",
                StudentId = 1
            };
            studentsRepositoryMock.DoesStudentExist(Arg.Any<int>()).Returns(true);
            gradesRepositoryMock.AddGrade(Arg.Any<Grade>()).Returns(gradeToAdd.StudentId);
            var studentService = new StudentsService(studentsRepositoryMock, gradesRepositoryMock, mapper);

            //Act
            var result = await studentService.AddStudentGrade(gradeToAdd);

            //Assert
            result.Should().Be(gradeToAdd.StudentId);
        }
    }
}
