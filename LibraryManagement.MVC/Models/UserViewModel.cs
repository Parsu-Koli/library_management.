namespace LibraryManagement.MVC.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Role { get; set; } = "Member";
        public string Phone { get; set; } = "";
        public DateTime JoinedDate { get; set; }
    }

    public class UserCreateViewModel
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Role { get; set; } = "Member";
        public string Phone { get; set; } = "";
    }

    public class UserUpdateViewModel : UserViewModel { }
}
