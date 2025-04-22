using System.ComponentModel.DataAnnotations;

namespace SmartReplySystem.Models
{
    public class InquiryDto
    {
        [Required]
        public string Name {get; set;}

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Message must be at least 10 characters long.")]
        public string Message { get; set; }
    }   
}