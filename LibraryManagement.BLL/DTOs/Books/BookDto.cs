namespace LibraryManagement.BLL.DTOs.Books
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Status { get; set; } = "Available"; // Available, Borrowed, Reserved
        public DateTime CreatedDate { get; set; }
    }
}
