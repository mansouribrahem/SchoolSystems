using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystems.APIs.DAL.StudentRepo;
using SchoolSystems.APIs.DTOs;
using SchoolSystems.DAL.Context;
using SchoolSystems.DAL.Models;

namespace SchoolSystems.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        
        private readonly IStudentRepository _stdRepo;

        public StudentController( IStudentRepository stdRepo)
        {
            
            _stdRepo = stdRepo;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            List<GetStudentDto> studentsDto = _stdRepo.GetAll();
            if (studentsDto == null)
            {
                return BadRequest("No students yet!");
            }
            return Ok(studentsDto);
        }
        [HttpGet("Name/{Name}")]
        public IActionResult GetByName(string Name)
        {
            GetStudentDto student = _stdRepo.GetByName(Name);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpGet("Id/{Id}")]
        public IActionResult GetById(int Id)
        {

            GetStudentDto student = _stdRepo.GetById(Id);
            if (student == null)
                return NotFound();


            return Ok(student);

        }

        [HttpPost]
        public IActionResult Create(CreateStudentDto studentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int stdId = _stdRepo.Create(studentDto);
            return Ok($"Student {stdId} Created Successfully!");
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id,UpdateStudentDto studentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Student std=_stdRepo.Update(id, studentDto);
            if (std is null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            bool isDeleted = _stdRepo.Delete(id);
            if (isDeleted)
                return NoContent();
            return NotFound();
        }
    }
}
