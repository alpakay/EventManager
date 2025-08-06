using System.ComponentModel.DataAnnotations;
using Entities.Models;

namespace Entities.Dtos
{
    public class EventFormDto
    {
        public int EventId { get; set; }
        [Required(ErrorMessage = "Etkinlik adı zorunludur.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Açıklama zorunludur.")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Uzun açıklama zorunludur.")]
        public string LongDescription { get; set; } = string.Empty;
        public string? ImgUrl { get; set; }
        [Required(ErrorMessage = "Başlangıç tarihi zorunludur.")]
        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(1).Date;
        [Required(ErrorMessage = "Bitiş tarihi zorunludur.")]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1).Date.AddHours(1);
        public bool isActive { get; set; } = true;
        public int? CreatorId { get; set; }
        public string CreatorName { get; set; } = string.Empty;
        public bool IsEditMode { get; set; } = false;
    }
}