using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, 
            IPokemonRepository pokemonRepository, 
            IReviewerRepository reviewerRepository, IMapper mapper) 
        {
            _reviewRepository = reviewRepository;
            _pokemonRepository = pokemonRepository;
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetAllReview()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetAllReview());
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int id)
        {
            if(!_reviewRepository.ReviewExist(id))
            {
                return NotFound();
            }
            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(review);
        }
        [HttpGet("pokemon/{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByReview(int reviewId)
        {
            var pokemon = _mapper.Map<PokemonDto>(_reviewRepository.GetPokemonByReview(reviewId));
            return Ok(pokemon);
        }

        [HttpGet("reviewer/{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewerByReview(int reviewId)
        {
            var reviewer = _mapper.Map<ReviewerDto>(_reviewRepository.GetReviewerByReview(reviewId));
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(reviewer);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromQuery] int pokeId, [FromQuery] int reviewerId, [FromBody] ReviewDto reviewCreate)
        {
            if (reviewCreate == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reviewMap = _mapper.Map<Review>(reviewCreate);
            var reviewer = _reviewerRepository.GetReviewer(reviewerId);
            var pokemon = _pokemonRepository.GetPokemonId(pokeId);
            reviewMap.Pokemon = pokemon;
            reviewMap.Reviewer = reviewer;
            if (!_reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successlly Created");
        }
        [HttpPut("{reviewId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int reviewId, [FromBody] ReviewDto reviewUpdate)
        {
            if (reviewUpdate == null)
                return BadRequest(ModelState);
            if (reviewId != reviewUpdate.Id)
                return BadRequest(ModelState);

            if (!_reviewRepository.ReviewExist(reviewId))
                return NotFound();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var review = _mapper.Map<Review>(reviewUpdate);
            if (!_reviewRepository.UpdateReview(review))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{reviewId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExist(reviewId))
            {
                return NotFound();
            }
            var review = _reviewRepository.GetReview(reviewId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_reviewRepository.DeleteReview(review))
            {
                ModelState.AddModelError("", "Something went wrong delete!");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
