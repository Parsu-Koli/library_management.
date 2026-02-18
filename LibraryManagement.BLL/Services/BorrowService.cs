using LibraryManagement.BLL.DTOs.Borrows;
using LibraryManagement.DAL.Models;
using LibraryManagement.DAL.Repositories.Interfaces;

namespace LibraryManagement.BLL.Services
{
    public class BorrowService
    {
        private readonly IBorrowRepository _borrowRepository;

        public BorrowService(IBorrowRepository borrowRepository)
        {
            _borrowRepository = borrowRepository;
        }

        public async Task<IEnumerable<BorrowRecordDto>> GetAllBorrowsAsync()
        {
            var borrows = await _borrowRepository.GetAllWithDetailsAsync();  // ✅ CHANGED!
            return borrows.Select(b => new BorrowRecordDto
            {
                Id = b.Id,
                BookId = b.BookId,
                BookTitle = b.Book.Title,      // ✅ NOW WORKS!
                UserId = b.UserId,
                UserName = b.User.Name,        // ✅ NOW WORKS!
                BorrowDate = b.BorrowDate,
                ReturnDate = b.ReturnDate,
                IsReturned = b.ReturnDate.HasValue
            });
        }

        public async Task<BorrowRecordDto?> GetBorrowByIdAsync(int id)
        {
            var borrow = await _borrowRepository.GetByIdWithDetailsAsync(id);  // ✅ CHANGED!
            if (borrow == null) return null;

            return new BorrowRecordDto
            {
                Id = borrow.Id,
                BookId = borrow.BookId,
                BookTitle = borrow.Book.Title,  // ✅ NOW WORKS!
                UserId = borrow.UserId,
                UserName = borrow.User.Name,    // ✅ NOW WORKS!
                BorrowDate = borrow.BorrowDate,
                ReturnDate = borrow.ReturnDate,
                IsReturned = borrow.ReturnDate.HasValue
            };
        }

        public async Task<int> BorrowBookAsync(BorrowCreateDto borrowCreateDto)
        {
            var borrowRecord = new BorrowRecord
            {
                BookId = borrowCreateDto.BookId,
                UserId = borrowCreateDto.UserId,
                BorrowDate = DateTime.Now
            };
            await _borrowRepository.AddAsync(borrowRecord);
            await _borrowRepository.SaveAsync();
            return borrowRecord.Id;
        }

        public async Task ReturnBookAsync(int id)
        {
            var borrowRecord = await _borrowRepository.GetByIdAsync(id);
            if (borrowRecord != null)
            {
                borrowRecord.ReturnDate = DateTime.Now;
                _borrowRepository.Update(borrowRecord);
                await _borrowRepository.SaveAsync();
            }
        }
    }
}
