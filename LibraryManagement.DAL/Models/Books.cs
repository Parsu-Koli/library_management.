using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.DAL.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required] public string Title { get; set; } = string.Empty;
        [Required] public string Author { get; set; } = string.Empty;
        [Required, MaxLength(13)] public string ISBN { get; set; } = string.Empty;
        public string Status { get; set; } = "Available";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
