using System.ComponentModel.DataAnnotations;

namespace SchoolSystems.APIs.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
        [MaxLength(50)]
        public string Mail { get; set; }=string.Empty;
       
    }
}
