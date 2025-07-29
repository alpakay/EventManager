using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Entities.Dtos;

public class UserRegisterDto
{
    [Required(ErrorMessage = "Ad Soyad alanı zorunludur.")]
    public required string FullName { get; set; }

    [Required(ErrorMessage = "Email alanı zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Şifre alanı zorunludur.")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "Şifre tekrar alanı zorunludur.")]
    [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
    public required string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Doğum tarihi alanı zorunludur.")]
    [DataType(DataType.Date, ErrorMessage = "Geçerli bir tarih giriniz.")]
    public required DateOnly BirthDate { get; set; }
}