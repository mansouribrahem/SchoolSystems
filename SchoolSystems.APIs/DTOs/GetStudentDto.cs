namespace SchoolSystems.APIs.DTOs
{
    public class GetStudentDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Grade { get; set; }
        public string? TeacherName { get; set; }
    }
}