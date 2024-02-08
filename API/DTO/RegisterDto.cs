using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class RegisterDto
    {
        [Required]
        [StringLength(15, ErrorMessage = "Username should be only 15 characters")]
        public string UserName { get; set;}
        [Required] public string Address { get; set; }
        [Required] public byte[] Photo { get; set; } 
        [Required] public string VoterId { get; set; }
        [Required] public string FullName { get; set; }
                
        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set;}
    }
}