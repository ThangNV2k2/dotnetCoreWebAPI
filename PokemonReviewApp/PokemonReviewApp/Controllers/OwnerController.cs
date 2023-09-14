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
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public OwnerController(IOwnerRepository ownerRepository,ICountryRepository countryRepository, IMapper mapper) 
        {
            _ownerRepository = ownerRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        [ProducesResponseType(400)]
        public IActionResult GetAllOwners()
        {
            var owners = _mapper.Map<List<OwnerDto>> (_ownerRepository.GetAllOwner());
            return Ok(owners);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public IActionResult GetOwner(int id)
        {
            if(!_ownerRepository.OwnerExist(id))
                return NotFound();
            var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetOwner(id));
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(owner);
        }
        [HttpGet("owner/{pokeId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        [ProducesResponseType(400)]
        public IActionResult GetOwnersOfPokemon(int pokeId)
        {
            var owners = _mapper.Map<List<OwnerDto>>(_ownerRepository.GetOwnersOfPokemon(pokeId));
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(owners);
        }
        [HttpGet("pokemon/{ownerId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonssByOwner(int ownerId)
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_ownerRepository.GetPokemonsByOwner(ownerId));
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(pokemons);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOwner([FromQuery] int countryId,[FromBody] OwnerDto ownerCreate)
        {
            if (ownerCreate == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ownerMap = _mapper.Map<Owner>(ownerCreate);
            ownerMap.Country = _countryRepository.GetCountry(countryId);
            if (!_ownerRepository.CreateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successlly Created");
        }
    }
}
