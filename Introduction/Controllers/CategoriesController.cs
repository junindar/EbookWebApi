using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Introduction.IService;
using Introduction.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Introduction.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryRepository categoryRepository,
            IBookRepository bookRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            throw new Exception("Sample Exeption");
            var resultRepo = await _categoryRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(resultRepo));

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var resultRepo = await _categoryRepository.GetById(id);

            if (resultRepo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CategoryDto>(resultRepo));

        }

        [HttpGet()]
        [Route("{id}/books")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksByCategory(int id)
        {
            var cat = await _categoryRepository.GetById(id);
            if (cat == null)
            {
                return NotFound();
            }
            var resultRepo = await _bookRepository.GetAllBooksByCategoryId(id);
            return Ok(_mapper.Map<IEnumerable<BookDto>>(resultRepo));
        }

        [Route("{id}/books/{bookid}")]
        public async Task<ActionResult<BookDto>> 
            GetBookByCategory(int id, int bookid)
        {
            var cat = await _categoryRepository.GetById(id);
            if (cat == null)
            {
                return NotFound();
            }
            var resultRepo = await _bookRepository.GetBookById(bookid);
            if (resultRepo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<BookDto>(resultRepo));
        }

    }
}
