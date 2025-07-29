namespace Entities.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; } = DateOnly.MinValue;
        public string Password { get; set; } = string.Empty;
    }
}