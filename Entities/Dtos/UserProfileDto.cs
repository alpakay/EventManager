using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Entities.Dtos;

public class UserProfileDto
{
    [Required(ErrorMessage = "Ad Soyad alanı zorunludur.")]
    public required string FullName { get; set; }

    [Required(ErrorMessage = "Email alanı zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Şifre alanı zorunludur.")]
    [StringLength(30, ErrorMessage = "Şifre en az {2} ve en fazla {1} karakter uzunluğunda olmalıdır.", MinimumLength = 8)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Şifre en az bir büyük harf, bir küçük harf ve bir sayı içermelidir.")]
    public required string Password { get; set; }

    [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
    public string? ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Doğum tarihi alanı zorunludur.")]
    [DataType(DataType.Date, ErrorMessage = "Geçerli bir tarih giriniz.")]
    public DateOnly BirthDate { get; set; } = DateOnly.FromDateTime(DateTime.Now.AddYears(-18));
    public bool IsEditMode { get; set; } = false;
}