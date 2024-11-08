using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class UserRegistration
    {
        public Guid Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public enum UserRole
        {
            Admin,
            User,
            Viewer
        }
        
        public UserRole Role { get; set; }
    }

}
