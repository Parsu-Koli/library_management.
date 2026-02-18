using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.DAL.Models
{
    public class BorrowRecord
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        [ForeignKey("BookId")] public Book Book { get; set; } = null!;
        public int UserId { get; set; }
        [ForeignKey("UserId")] public User User { get; set; } = null!;
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
