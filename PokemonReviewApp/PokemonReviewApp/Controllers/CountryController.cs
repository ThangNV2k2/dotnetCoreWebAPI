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
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper) 
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        [ProducesResponseType(400)]
        public IActionResult GetAllCountry()
        {
            var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetAllCountry());
            return Ok(countries);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int id)
        {
            if (!_countryRepository.CountryExist(id))
                return NotFound();
            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountry(id));
            return Ok(country);
        }

        [HttpGet("owner/{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountryByOwner(int ownerId) 
        {
            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountryByOwner(ownerId));
            if(!ModelState.IsValid)
                return BadRequest();
            return Ok(country);
        }
        [HttpGet("owners/{countryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        [ProducesResponseType(400)]
        public IActionResult GetOwnersByCountry(int countryId)
        {
            var owneres = _mapper.Map<List<OwnerDto>>(_countryRepository.GetOwnersByCountry(countryId));
            return Ok(owneres);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCountry([FromBody] CountryDto countryCreate)
        {
            if (countryCreate == null)
                return BadRequest(ModelState);
            var country = _countryRepository
                .GetAllCountry()
                .Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.Trim().ToUpper())
                .FirstOrDefault();
            if (country != null)
            {
                ModelState.AddModelError("", "Country already exist!");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var countryMap = _mapper.Map<Country>(countryCreate);
            if (!_countryRepository.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successlly Created");
        }
    }
}
