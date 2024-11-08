using static TaskManager.Models.UserRegistration;

namespace TaskManager.Models
{
    public class Registration
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

    }
}
