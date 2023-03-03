using System.ComponentModel.DataAnnotations;

namespace PultApp.Models
{
    public class Admin
    {
        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
