using System.ComponentModel.DataAnnotations;

namespace ConsumeAuthPractice.DTO
{
    public class RegiResponseDto
    {
        public string Id { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Firstname { get; set; } = string.Empty;
        [Required]
        public string Lastname { get; set; } = string.Empty;
        [Required]
        public string Phonenumber { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}
