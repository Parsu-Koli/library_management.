using LibraryManagement.BLL.DTOs.Books;
using LibraryManagement.DAL.Models;
using LibraryManagement.DAL.Repositories.Interfaces;

namespace LibraryManagement.BLL.Services
{
    public class BookService
    {
        private readonly IRepository<Book> _bookRepository;

        public BookService(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                ISBN = b.ISBN,
                Status = b.Status,
                CreatedDate = b.CreatedDate
            });
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return null;

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                Status = book.Status,
                CreatedDate = book.CreatedDate
            };
        }

        public async Task<int> CreateBookAsync(BookCreateDto bookCreateDto)
        {
            var book = new Book
            {
                Title = bookCreateDto.Title,
                Author = bookCreateDto.Author,
                ISBN = bookCreateDto.ISBN,
                Status = "Available",
                CreatedDate = DateTime.Now
            };
            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveAsync();
            return book.Id;
        }

        public async Task UpdateBookAsync(int id, BookUpdateDto bookUpdateDto)
        {
            var existingBook = await _bookRepository.GetByIdAsync(id);
            if (existingBook != null)
            {
                existingBook.Title = bookUpdateDto.Title;
                existingBook.Author = bookUpdateDto.Author;
                existingBook.ISBN = bookUpdateDto.ISBN;
                existingBook.Status = bookUpdateDto.Status;
                _bookRepository.Update(existingBook);
                await _bookRepository.SaveAsync();
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book != null)
            {
                _bookRepository.Delete(book);
                await _bookRepository.SaveAsync();
            }
        }
    }
}
