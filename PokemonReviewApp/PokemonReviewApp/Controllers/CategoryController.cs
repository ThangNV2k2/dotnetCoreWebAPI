using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Dto;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper) 
        {
            _categoryRepository = categoryRepository; 
            _mapper = mapper;
        
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(400)]
        public IActionResult GetCategories() 
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());
            if(!ModelState.IsValid)
            {
                return BadRequest();
            } return Ok(categories);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int id)
        {
            if(!_categoryRepository.CategoriesExists(id))
            {
                return NotFound();
            }
            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(id));
            return Ok(category);
        }
        [HttpGet("pokemon/{cateId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByCategory(int cateId) 
        {
            var pokemones = _mapper.Map<List<PokemonDto>>(_categoryRepository.GetPokemonByCategory(cateId));
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(pokemones);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if (categoryCreate == null)
                return BadRequest(ModelState);
            var category = _categoryRepository
                .GetCategories()
                .Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.Trim().ToUpper())
                .FirstOrDefault();
            if (category != null)
            {
                ModelState.AddModelError("", "Category already exist!");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var categoryMap = _mapper.Map<Category>(categoryCreate);
            if (!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successlly Created");
        }

        [HttpPut("{cateId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int cateId, [FromBody] CategoryDto categoryUpdate)
        {
            if(categoryUpdate == null)
                return BadRequest(ModelState);
            if(cateId != categoryUpdate.Id)
                return BadRequest(ModelState);

            if(!_categoryRepository.CategoriesExists(cateId))
                return NotFound();
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var category = _mapper.Map<Category>(categoryUpdate);
            if(!_categoryRepository.UpdateCategory(category))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{cateId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int cateId)
        {
            if(!_categoryRepository.CategoriesExists(cateId))
            {
                return NotFound();
            }
            var category = _categoryRepository.GetCategory(cateId);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!_categoryRepository.DeleteCategory(category))
            {
                ModelState.AddModelError("", "Something went wrong delete!");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
