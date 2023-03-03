using System.ComponentModel.DataAnnotations;

namespace PultApp.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public int Heard { get; set; }
        public int IsSubscribed { get; set; }
        public string ? Email { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
