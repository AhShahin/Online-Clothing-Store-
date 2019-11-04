using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Data.UOW;
using OnlineStore.Dtos;
using OnlineStore.Models;
using OnlineStore.Data.Repos;

namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IRepository<Category> _repository;

        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper, ICategoryRepository categoryRepository, IRepository<Category> repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _repository = repository;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryRepository.GetCategories();

            var categoriesToReturn = _mapper.Map<IEnumerable<CategoryForListDto>>(categories);

            return Ok(categoriesToReturn);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _categoryRepository.GetCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryToReturn = _mapper.Map<CategoryForDetailsDto>(category);

            return Ok(categoryToReturn);
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory([FromRoute] int id, [FromBody] CategoryForUpdateDto categoryForUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != category.Id)
            //{
            //    return BadRequest();
            //}

            var category = await _repository.GetAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            _mapper.Map(categoryForUpdateDto, category);

            if (await _unitOfWork.SaveAsync())
                return NoContent();

            throw new Exception($"Updating category {id} failed on save");
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _repository.GetAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _repository.Remove(category);
            await _unitOfWork.SaveAsync();

            return Ok(category);
        }
    }
}