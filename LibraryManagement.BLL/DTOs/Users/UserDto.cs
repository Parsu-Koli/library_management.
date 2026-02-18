namespace LibraryManagement.BLL.DTOs.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Role { get; set; } = "Student"; // Student, Librarian, Admin
        public DateTime JoinedDate { get; set; }
    }
}
