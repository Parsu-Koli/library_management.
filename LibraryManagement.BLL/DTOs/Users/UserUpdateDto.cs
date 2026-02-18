using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.BLL.DTOs.Users
{
    public class UserUpdateDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public string Role { get; set; } = "Student";
    }
}
