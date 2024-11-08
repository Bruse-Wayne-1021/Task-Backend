using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Addressline1 {  get; set; }
        public string? Addressline2 { get; set; }
        public string? City { get; set; }
        public int? Userid { get; set; }


        public User? user { get; set; }

    }
}
