using System.ComponentModel.DataAnnotations;

namespace MyApiNet6.Models
{
    public class SignUpModel
    {
        [Required]
        public String FristName { get; set; } = null!;
        [Required]
        public String LastName { get; set; } = null!;
        [Required, EmailAddress]
        public String Email { get; set; } = null !;
        [Required]
        public String Password { get; set; } = null!;
        [Required]
        public String ConfirmPassword { get; set; } = null!;
    }
}
