using System.ComponentModel.DataAnnotations;

namespace SchoolSystems.APIs.DTOs
{
    public class CreateStudentDto
    {
      
        [Required]
        public string Name { get; set; }=string.Empty;
        [Required]
        [MaxLength(5)]
        public string Grade { get; set; }= string.Empty;
        public int TeacherId { get; set; }

    }
}