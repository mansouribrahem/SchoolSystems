using System.ComponentModel.DataAnnotations;

namespace SchoolSystems.APIs.DTOs
{
    public class UpdateStudentDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Grade { get; set; }
        public int? TeacherId { get; set; }
    }
}
