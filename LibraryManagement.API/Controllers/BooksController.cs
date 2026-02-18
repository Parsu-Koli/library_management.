using Microsoft.AspNetCore.Mvc;
using LibraryManagement.BLL.Services;
using LibraryManagement.BLL.DTOs.Books;

namespace LibraryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService) => _bookService = bookService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateBook(BookCreateDto bookCreateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var bookId = await _bookService.CreateBookAsync(bookCreateDto);
            return CreatedAtAction(nameof(GetBook), new { id = bookId }, bookId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, BookUpdateDto bookUpdateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _bookService.UpdateBookAsync(id, bookUpdateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
