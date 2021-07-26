using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityApp.BussinessLogic.DomainModels;
using UniversityApp.BussinessLogic.Services.Interfaces;
using UniversityApp.DTOs;
using UniversityApp.Pagination;
using UniversityApp.Validators;

namespace UniversityApp.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsService _studentsService;
        private readonly IMapper _mapper;
        private readonly IValidator<StudentDto> _studentValidator;
        private readonly IValidator<GradeDto> _gradeValidator;
        private readonly IValidator<StudentSearchParametersDto> _studentSearchParametersValidator;

        public StudentsController(IStudentsService studentsService, IMapper mapper, IValidator<StudentDto> studentValidator, IValidator<GradeDto> gradeValidator, IValidator<StudentSearchParametersDto> studentSearchParametersValidator)
        {
            _studentsService = studentsService;
            _mapper = mapper;
            _studentValidator = studentValidator;
            _gradeValidator = gradeValidator;
            _studentSearchParametersValidator = studentSearchParametersValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var students = await _studentsService.GetStudents();

            var result = _mapper.Map<List<StudentDto>>(students);

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] StudentSearchParametersDto searchParameters)
        {
            var searchParamsValidation = _studentSearchParametersValidator.Validate(searchParameters);

            if(!searchParamsValidation.IsValid)
            {
                return BadRequest();
            }

            var searchData = _mapper.Map<StudentSearchParametersDomainModel>(searchParameters);
            var data = await _studentsService.SearchStudents(searchData);

            var students = _mapper.Map<List<StudentDto>>(data.Students);
            return Ok(new PagedModel<StudentDto>
            {
                PageNo = searchParameters.PageNumber,
                PageSize = searchParameters.PageSize,
                Data = students,
                TotalRecordCount = data.TotalRecordCount
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentsService.GetStudent(id);

            if(student == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<StudentDto>(student);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentDto student)
        {
            var studentValidation = _studentValidator.Validate(student);

            if(!studentValidation.IsValid)
            {
                return BadRequest(studentValidation.Errors);
            }

            var studentToAdd = _mapper.Map<StudentDomainModel>(student);

            var id = await _studentsService.AddStudent(studentToAdd);

            return Created($"~api/students/{id}",id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] StudentDto student)
        {
            var studentToUpdate = _mapper.Map<StudentDomainModel>(student);

            var result = await _studentsService.UpdateStudent(id, studentToUpdate);

            if(!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _studentsService.DeleteStudent(id);
            
            if(!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("{id}/grades")]
        public async Task<IActionResult> GetGrades(int id)
        {
            var grades = await _studentsService.GetStudentGrades(id);

            if(grades == null)
            {
                return BadRequest();
            }

            var result = _mapper.Map<List<GradeDto>>(grades);

            return Ok(result);
        }

        [HttpPost("{id}/grades")]
        public async Task<IActionResult> PostGrade(int id, GradeDto grade)
        {
            grade.StudentId = id;

            var gradeValidation = await _gradeValidator.ValidateAsync(grade);

            if(gradeValidation.IsValid)
            {
                return BadRequest(gradeValidation.Errors);
            }

            var gradeToAdd = _mapper.Map<GradeDomainModel>(grade);

            var gradeId = await _studentsService.AddStudentGrade(gradeToAdd);

            if(gradeId == null)
            {
                return BadRequest();
            }

            return Created($"~api/students/{id}/grades/{gradeId}", gradeId);
        }

        [HttpPut("{id}/grades/{gradeId}")]
        public async Task<IActionResult> PutGrade(int id, int gradeId, GradeDto grade)
        {
            grade.StudentId = id;

            var gradeToUpdate = _mapper.Map<GradeDomainModel>(grade);

            var result = await _studentsService.UpdateStudentGarde(gradeId, gradeToUpdate);

            if(!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
