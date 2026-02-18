

namespace LibraryManagement.MVC.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public string ISBN { get; set; } = "";
        public string Status { get; set; } = "Available";
        public DateTime CreatedDate { get; set; }
    }

    public class BookCreateViewModel
    {
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public string ISBN { get; set; } = "";
    }

    public class BookUpdateViewModel : BookViewModel { }
}
