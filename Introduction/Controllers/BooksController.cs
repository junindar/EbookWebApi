using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Introduction.Entity;
using Introduction.IService;
using Introduction.Models;
using Microsoft.AspNetCore.Mvc;

namespace Introduction.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Book>>>  GetBooks()
        //{
        //    var books = await _bookRepository.GetAllBooks();
        //    return Ok(books);
        //}
        [HttpGet("{bookId}")]
        public async Task<ActionResult<Book>> GetBook(int bookId)
        {
            var book = await _bookRepository.GetBookById(bookId);
            return Ok(book);
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks(string penerbit, string keyword)
        {

            var resultRepo = await _bookRepository.GetAllBooks(penerbit, keyword);
            return Ok(_mapper.Map<IEnumerable<BookDto>>(resultRepo));
        }


        //[HttpGet()]
        //public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks(string penerbit)
        //{

        //    var resultRepo = await _bookRepository.GetAllBooks(penerbit);
        //    return Ok(_mapper.Map<IEnumerable<BookDto>>(resultRepo));
        //}

    }
}
