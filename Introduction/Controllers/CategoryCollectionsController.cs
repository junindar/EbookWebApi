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
    [Route("api/categorycollections")]
    [ApiController]
    public class CategoryCollectionsController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryCollectionsController(ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<CategoryDto>>>
            CreateMultiple(IEnumerable<CategoryDto> categoryColl)
        {
            var categoryEntities = _mapper.Map<IEnumerable<Category>>(categoryColl);
            await _categoryRepository.InsertMultiple(categoryEntities);

            return Ok();
        }
    }
}
