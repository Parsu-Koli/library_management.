using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; } 
        [Required] public string Email { get; set; } 
        public string Role { get; set; } 
        public DateTime JoinedDate { get; set; } = DateTime.Now;
        public string Phone { get; set; }
    }
}
