using LibraryManagement.DAL.Data;
using LibraryManagement.DAL.Models;
using LibraryManagement.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DAL.Repositories.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly LibraryDbContext _context;

        public Repository(LibraryDbContext context) => _context = context;

        public async Task<IEnumerable<T>> GetAllAsync() =>
            await _context.Set<T>().AsNoTracking().ToListAsync();

        public async Task<T?> GetByIdAsync(int id) =>
            await _context.Set<T>().FindAsync(id);

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity) => _context.Set<T>().Update(entity);
        public void Delete(T entity) => _context.Set<T>().Remove(entity);
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }

    public class BorrowRepository : Repository<BorrowRecord>, IBorrowRepository
    {
        public BorrowRepository(LibraryDbContext context) : base(context) { }

        public async Task<IEnumerable<BorrowRecord>> GetAllWithDetailsAsync()
        {
            return await _context.BorrowRecords
                .Include(b => b.Book)
                .Include(b => b.User)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<BorrowRecord?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.BorrowRecords
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }

}
