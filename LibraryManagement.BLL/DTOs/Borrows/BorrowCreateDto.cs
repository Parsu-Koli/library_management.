using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.BLL.DTOs.Borrows
{
    public class BorrowCreateDto
    {
        [Required(ErrorMessage = "Book ID is required")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }
    }
}
