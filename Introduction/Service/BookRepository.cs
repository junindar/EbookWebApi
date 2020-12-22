using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Introduction.Entity;
using Introduction.IService;
using Microsoft.EntityFrameworkCore;

namespace Introduction.Service
{
    public class BookRepository : IBookRepository
    {
        private readonly PustakaDbContext _dbContext;

        public BookRepository(PustakaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return _dbContext.Books;
        }

        public async Task<IEnumerable<Book>> GetAllBooks(string penerbit)
        {
            if (string.IsNullOrEmpty(penerbit))
            {
                return await GetAllBooks();
            }

            return await _dbContext.Books.Where(c =>
                c.Penerbit == penerbit).Include(b =>
                b.Category).ToListAsync();
        }

        public async  Task<Book> GetBookById(int bookId)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(b => b.ID ==
                                                        bookId);
        }
        public async Task<IEnumerable<Book>> GetAllBooksByCategoryId(int id)
        {
            return await _dbContext.Books.Where(c => c.CategoryID == id).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAllBooks(string penerbit, string keyword)
        {
            if (string.IsNullOrEmpty(penerbit) && string.IsNullOrEmpty(keyword))
            {
                return await GetAllBooks();
            }

            var books = _dbContext.Books.AsQueryable();

            if (!string.IsNullOrEmpty(penerbit))
            {
                books = books.Where(c => c.Penerbit == penerbit);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                books = books.Where(c => c.Judul.Contains(keyword) ||
                                         c.Penulis.Contains(keyword) ||
                                         c.Penerbit.Contains(keyword) || 
                                         c.Deskripsi.Contains(keyword));
            }

            return books.AsEnumerable();

        }

        public async Task<Book> Insert(Book book)
        {
            _dbContext.Add(book);
            await _dbContext.SaveChangesAsync();
            return book;
        }

        public async Task Update(Book book)
        {
            _dbContext.Update(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int bookId)
        {
            var book = await _dbContext.Books.FindAsync(bookId);
            _dbContext.Remove(book);
            await _dbContext.SaveChangesAsync();
        }
    }

}
