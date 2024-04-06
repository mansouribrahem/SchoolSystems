using Microsoft.EntityFrameworkCore;
using SchoolSystems.DAL.Context;
using SchoolSystems.DAL.Models;
using SchoolSystems.APIs.DTOs;

namespace SchoolSystems.APIs.DAL.StudentRepo
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context) {
            _context= context;
        }
        public int Create(CreateStudentDto studentDto)
        {
            Student student = new Student();
            
            student.Name = studentDto.Name;
            student.TeacherId = studentDto.TeacherId;
            student.Grade = studentDto.Grade;

            _context.Students.Add(student);
            _context.SaveChanges();

            return student.Id;
        }

        public bool Delete(int id)
        {
           Student targetStd= _context.Students.FirstOrDefault(s=>s.Id==id);
            if (targetStd is null)
                return false;

            _context.Students.Remove(targetStd);
            _context.SaveChanges();
            return true;
        }

        public List<GetStudentDto> GetAll()
        {
            List<GetStudentDto>StudentsDto= new List<GetStudentDto>();
           List<Student> students= _context.Students.Include(s=>s.Teacher).ToList();
            foreach (var student in students)
            {
                GetStudentDto studentDto = new GetStudentDto();
                studentDto.Id = student.Id;
                studentDto.Name = student.Name;
                studentDto.Grade = student.Grade;
                studentDto.TeacherName = student.Teacher?.Name??"NA";
                StudentsDto.Add(studentDto);
            }
            return StudentsDto;
        }

        public GetStudentDto GetById(int Id)
        {
            Student targetStudent = _context.Students.Include(s => s.Teacher).FirstOrDefault(s=>s.Id==Id);
            if (targetStudent is null)
            {
                return null;
            }
            GetStudentDto getStudentDto= new GetStudentDto();

            getStudentDto.Id = targetStudent.Id;
            getStudentDto.Name= targetStudent.Name;
            getStudentDto.Grade = targetStudent.Grade;
            getStudentDto.TeacherName = targetStudent.Teacher?.Name ?? "NA";
            return getStudentDto;
        }

        public GetStudentDto GetByName(string Name)
        {
           Student targetStudent=_context.Students.Include(s=>s.Teacher).FirstOrDefault(s=>s.Name==Name);
            if (targetStudent == null)
            {
                return null;
            }
            GetStudentDto targetStudentDto = new GetStudentDto();
            targetStudentDto.Id = targetStudent.Id;
            targetStudentDto.Name = targetStudent.Name;
            targetStudentDto.Grade = targetStudent.Grade;
            targetStudentDto.TeacherName = targetStudent.Teacher?.Name??"NA";
            return targetStudentDto;
        }

        public Student Update(int id,UpdateStudentDto updatedStudent)
        {
            Student targetStudent = _context.Students.FirstOrDefault(s=>s.Id==id);
            if (targetStudent == null)
                return null;

            targetStudent.Name= updatedStudent.Name;
            targetStudent.TeacherId= updatedStudent.TeacherId;
            targetStudent.Grade = updatedStudent.Grade;
            _context.Update(targetStudent);
            _context.SaveChanges();
            return targetStudent;
        }
    }
}
