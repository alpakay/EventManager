using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Email alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        public required string Password { get; set; }
    }
}