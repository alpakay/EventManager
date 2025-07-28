namespace EventManager.Entities.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.MinValue;
        public string ImgUrl { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        public int ParticipantCount { get; set; } = 0;
        public int MaxParticipants { get; set; } = 0;
        public int CategoryId { get; set; } = 0;
        public Category Category { get; set; } = null!;
        public ICollection<Participant> Participants { get; set; } = new List<Participant>();
    }
}