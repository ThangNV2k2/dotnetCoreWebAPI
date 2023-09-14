using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemonId(int id);
        Pokemon GetPokemonName(String name);
        decimal GetPokemonRating(int pokemonId);
        bool PokemonExist(int pokemonId);
    }
}
