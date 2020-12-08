using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Introduction.Entity;
using Introduction.IService;

namespace Introduction.Service
{
    public class BookRepository : IBookRepository
    {
        private readonly PustakaDbContext _dbContext;

        public BookRepository(PustakaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _dbContext.Books;
        }

        public Book GetBookById(int bookId)
        {
            return _dbContext.Books.FirstOrDefault(b => b.ID ==
                                                        bookId);
        }

    }

}
