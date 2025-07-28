namespace EventManager.Entities.Models
{
    public class Participant
    {
        public int ParticipantId { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}