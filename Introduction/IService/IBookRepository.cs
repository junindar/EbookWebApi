using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Introduction.Entity;

namespace Introduction.IService
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<IEnumerable<Book>> GetAllBooks(string penerbit);
        Task<Book> GetBookById(int bookId);
        //Interface (IBookRepository)
        Task<IEnumerable<Book>> GetAllBooksByCategoryId(int id);
        Task<IEnumerable<Book>> GetAllBooks(string penerbit, string keyword);
    }
}
