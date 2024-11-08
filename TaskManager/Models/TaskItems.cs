using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class TaskItems
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public int Status { get; set; }
        [Required]
        public string Priority { get; set; }

        public User? Assigee { get; set; }

        public int AssigneeId { get; set; }

        public ICollection<CheckList> CheckList { get; set; }

    }
}
