using System.ComponentModel.DataAnnotations;
using Entities.Dtos;

namespace EventManagerApp.Areas.Management.ViewModels
{
    public class EventFormViewModel
    {
        public EventFormDto Event { get; set; } = new EventFormDto();
        [RequiredIfEventImgUrlIsNull(ErrorMessage = "Lütfen bir görsel seçin.")]
        public IFormFile? ImageFile { get; set; }
    }

    public class RequiredIfEventImgUrlIsNullAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var model = (EventFormViewModel)validationContext.ObjectInstance;

        if (string.IsNullOrWhiteSpace(model.Event.ImgUrl))
        {
            if (value is not IFormFile file || file.Length == 0)
            {
                return new ValidationResult("Lütfen bir görsel seçin.");
            }
        }

        return ValidationResult.Success;
    }
}
}