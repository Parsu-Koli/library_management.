using Microsoft.EntityFrameworkCore;
using LibraryManagement.DAL.Models;

namespace LibraryManagement.DAL.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BorrowRecord>()
                .HasOne(b => b.Book)
                .WithMany()
                .HasForeignKey(b => b.BookId);

            modelBuilder.Entity<BorrowRecord>()
                .HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId);
        }
    }
}
