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
    [ApiExplorerSettings(GroupName = "LatihanOpenAPIBook")]
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
        [HttpGet("{bookId}",Name= "GetBook")]
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


        [HttpPost]
        public async Task<ActionResult<BookDto>>
            CreateBook(BookDto book)
        {
            var bookEntity = _mapper.Map<Book>(book);
            bookEntity = await _bookRepository.Insert(bookEntity);

            var bookForReturn = _mapper.Map<BookDto>(bookEntity);

            return CreatedAtRoute("GetBook", new { bookId = bookForReturn.Id },
                bookForReturn);
        }

        [HttpPut]
        public async Task<ActionResult<BookDto>>
            UpdateBook(BookDto bookDto)
        {
            var book = await _bookRepository.GetBookById(bookDto.Id);

            if (book == null)
            {
                return NotFound();
            }
            book.Judul = bookDto.Judul;
            book.Penulis = bookDto.Penulis;
            book.Penerbit = bookDto.Penerbit;
            book.Deskripsi = bookDto.Deskripsi;
            book.Status = bookDto.Status;
            book.Gambar = bookDto.Gambar;
            book.CategoryID = bookDto.CategoryId;
            await _bookRepository.Update(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryDto>>
            DeleteBook(int id)
        {
            var cat = await _bookRepository.GetBookById(id);

            if (cat == null)
            {
                return NotFound();
            }
            await _bookRepository.Delete(id);


            return NoContent();
        }

        //[HttpGet()]
        //public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks(string penerbit)
        //{

        //    var resultRepo = await _bookRepository.GetAllBooks(penerbit);
        //    return Ok(_mapper.Map<IEnumerable<BookDto>>(resultRepo));
        //}

    }
}
