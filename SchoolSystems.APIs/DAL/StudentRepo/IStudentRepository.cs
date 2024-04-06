using SchoolSystems.APIs.DTOs;
using SchoolSystems.DAL.Models;

namespace SchoolSystems.APIs.DAL.StudentRepo
{
    public interface IStudentRepository
    {
        public int Create(CreateStudentDto studentDto);
        public List<GetStudentDto> GetAll();
        public GetStudentDto GetByName(string Name);
        public GetStudentDto GetById(int Id);
        public Student Update(int id, UpdateStudentDto updatedStudent);
        public bool Delete(int id);
    }
}
