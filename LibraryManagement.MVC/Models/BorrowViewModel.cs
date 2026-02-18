namespace LibraryManagement.MVC.Models
{
    public class BorrowViewModel
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; } = "";
        public int UserId { get; set; }
        public string UserName { get; set; } = "";
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status => ReturnDate.HasValue ? "Returned" : "Active";
    }

    public class BorrowCreateViewModel
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}
