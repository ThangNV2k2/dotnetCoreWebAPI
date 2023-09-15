using PokemonReviewApp.data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;
        
        public OwnerRepository(DataContext context) 
        {
            _context = context;
        }
        public ICollection<Owner> GetAllOwner()
        {
            return _context.Owners.ToList();
        }

        public Owner GetOwner(int ownerId)
        {
            return _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnersOfPokemon(int pokeId)
        {
            return _context.PokemonOwners.Where(po => po.PokemonId == pokeId).Select(o => o.Owner).ToList();
        }

        public ICollection<Pokemon> GetPokemonsByOwner(int ownerId)
        {
            return _context.PokemonOwners.Where(po => po.OwnerId == ownerId).Select(p => p.Pokemon).ToList();
        }

        public bool OwnerExist(int ownerId)
        {
            return _context.Owners.Any(o => o.Id == ownerId);
        }
        public bool CreateOwner(Owner owner)
        {
            _context.Add(owner);
            return Save();
        }
        public bool UpdateOwner(Owner owner)
        {
            _context.Update(owner);
            return Save();
        }
        public bool DeleteOwner(Owner owner) 
        {
            _context.Remove(owner);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
