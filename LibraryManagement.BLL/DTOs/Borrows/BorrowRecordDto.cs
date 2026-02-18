namespace LibraryManagement.BLL.DTOs.Borrows
{
    public class BorrowRecordDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; } = "";
        public int UserId { get; set; }
        public string UserName { get; set; } = "";
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
    }
}
