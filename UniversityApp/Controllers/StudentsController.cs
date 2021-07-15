using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityApp.BussinessLogic.DomainModels;
using UniversityApp.BussinessLogic.Services.Interfaces;
using UniversityApp.DTOs;
using UniversityApp.Pagination;

namespace UniversityApp.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsService _studentsService;
        private readonly IMapper _mapper;

        public StudentsController(IStudentsService studentsService, IMapper mapper)
        {
            _studentsService = studentsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var students = await _studentsService.GetStudents();

            var result = _mapper.Map<List<StudentDto>>(students);

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] StudentSearchModel student, int pageNumber = 1, int pageSize = 10)
        {
            var data = await _studentsService.SearchStudents(student, pageNumber, pageSize);

            var students = _mapper.Map<List<StudentDto>>(data.Students);

            return Ok(new PagedModel<StudentDto>(students, pageNumber, pageSize, data.TotalRecordCount));

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
            var gradeToAdd = _mapper.Map<GradeDomainModel>(grade);

            var gradeId = await _studentsService.AddStudentGrade(id, gradeToAdd);

            if(gradeId == null)
            {
                return BadRequest();
            }

            return Created($"~api/students/{id}/grades/{gradeId}", gradeId);
        }

        [HttpPut("{id}/grades/{gradeId}")]
        public async Task<IActionResult> PutGrade(int id, int gradeId, GradeDto grade)
        {
            var gradeToUpdate = _mapper.Map<GradeDomainModel>(grade);

            var result = await _studentsService.UpdateStudentGarde(id, gradeId, gradeToUpdate);

            if(!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
