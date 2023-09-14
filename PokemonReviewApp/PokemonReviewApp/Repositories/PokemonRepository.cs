using PokemonReviewApp.data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;

        public PokemonRepository(DataContext context) 
        { 
            _context = context;
        }

        public Pokemon GetPokemonId(int id)
        {
           return _context.Pokemons.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemonName(string name)
        {
            return _context.Pokemons.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokemonId)
        {
            var review = _context.Reviews.Where(p => p.Pokemon.Id == pokemonId);
            if(review.Count() > 0)
            {
                return ((decimal)review.Sum(p => p.Rating) / review.Count());
            } else { return 0; }
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons.OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExist(int pokemonId)
        {
            return _context.Pokemons.Any(p => p.Id == pokemonId);
        }
        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var PokemonOwnerEntity = _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
            var PokemonCategoryEntity = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
            var pokemonOwner = new PokemonOwner()
            {
                Pokemon = pokemon,
                Owner = PokemonOwnerEntity
            };
            _context.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = PokemonCategoryEntity,
                Pokemon = pokemon
            };
            _context.Add(pokemonCategory);
            _context.Add(pokemon);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
