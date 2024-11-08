using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class User 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Nic { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        [Required]
        public string Password { get; set; }


            
        public Address? Address { get; set; }

        public ICollection<TaskItems> TaskItems { get; set; }=new List<TaskItems>();

    }
}
