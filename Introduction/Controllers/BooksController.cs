using System.Collections.Generic;
using Introduction.Entity;
using Introduction.IService;
using Microsoft.AspNetCore.Mvc;

namespace Introduction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            var books = _bookRepository.GetAllBooks();
            return Ok(books);
        }
        [HttpGet("{bookId}")]
        public ActionResult<Book> GetBook(int bookId)
        {
            var book = _bookRepository.GetBookById(bookId);
            return Ok(book);
        }

    }
}
