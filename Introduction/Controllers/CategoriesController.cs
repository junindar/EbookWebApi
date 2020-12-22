using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Introduction.Entity;
using Introduction.IService;
using Introduction.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Introduction.Controllers
{
    [Route("api/categories")]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "LatihanOpenAPICategory")]
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
        [MapToApiVersion("1.0")]
          
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var resultRepo = await _categoryRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(resultRepo));

        }

        //[HttpGet()]
        //[MapToApiVersion("1.1")]
        //public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoriesV_1_1()
        //{
        //    var resultRepo = await _categoryRepository.GetAll();
        //    return Ok(_mapper.Map<IEnumerable<CategoryDto>>(resultRepo));

        //}

        /// <summary>
        /// Untuk mendapatkan Category berdasarkan Category ID
        /// </summary>
        /// <param name="categoryId">ID yang dicari</param>
        /// <returns>Jika ID yang dicari ditemukan, maka akan menampilkan data category </returns>
        [HttpGet("{id}",Name= "GetCategory")]
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

        //[Route("{id}/books/{bookid}", Name = "GetBookByCategory")]
        //public async Task<ActionResult<BookDto>> 
        //    GetBookByCategory(int id, int bookid)
        //{
        //    var cat = await _categoryRepository.GetById(id);
        //    if (cat == null)
        //    {
        //        return NotFound();
        //    }
        //    var resultRepo = await _bookRepository.GetBookById(bookid);
        //    if (resultRepo == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(_mapper.Map<BookDto>(resultRepo));
        //}


        /// <summary>
        /// Tambah Kategori
        /// </summary>
        /// <returns>Data Kategori yang baru dibuat</returns>
        /// <remarks>
        /// Contoh untuk menambah kategori \
        /// {\
        ///     "Id": 0,\
        ///     "Nama" : "Contoh Kategori"\
        /// }
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> 
            CreateCategory(CategoryDto category)
        {
            var categoryEntity = _mapper.Map<Category>(category);
            categoryEntity = await _categoryRepository.Insert(categoryEntity);

            var categoryForReturn = _mapper.Map<CategoryDto>(categoryEntity);

            return CreatedAtRoute("GetCategory", new { id = categoryForReturn.Id },
                categoryForReturn);
        }

        [HttpPost]
        [Route("{id}/books")]
        public async Task<ActionResult<BookDto>> CreateBookForCategory(int id,
            BookDto book)
        {
            var cat = await _categoryRepository.GetById(id);

            if (cat == null)
            {
                return NotFound();
            }
            var bookEntity = _mapper.Map<Book>(book);
            bookEntity.CategoryID = id;
            bookEntity = await _bookRepository.Insert(bookEntity);

            var bookForReturn = _mapper.Map<BookDto>(bookEntity);

            return CreatedAtRoute("GetBookByCategory", new
            {
                id = id,
                bookid = bookForReturn.Id
            }, bookForReturn);
        }
        [HttpPut]
        public async Task<ActionResult<CategoryDto>>
            UpdateCategory(CategoryDto category)
        {
            var cat = await _categoryRepository.GetById(category.Id);

            if (cat == null)
            {
                return NotFound();
            }
            cat.Nama = category.Nama;
            await _categoryRepository.Update(cat);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryDto>>
            DeleteCategory(int id)
        {
            var cat = await _categoryRepository.
                GetById(id);

            if (cat == null)
            {
                return NotFound();
            }
            await _categoryRepository.
                Delete(id);


            return NoContent();
        }

        [HttpDelete]
        [Route("{id}/books/{bookId}")]
        public async Task<ActionResult<BookDto>> DeleteBookForCategory(int id,
            int bookId)
        {
            var cat = await _categoryRepository.GetById(id);

            if (cat == null)
            {
                return NotFound();
            }

            var book = await _bookRepository.GetBookById(bookId);

            if (book == null)
            {
                return NotFound();
            }

            await _bookRepository.Delete(bookId);
            return NoContent();
        }


    }
}
