using PokemonReviewApp.data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context) { _context = context; }
        public bool CategoriesExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemonByCategory(int cateId)
        {
            return _context.PokemonCategories.Where(c => c.CategoryId == cateId).Select(c => c.Pokemon).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            return Save();
        }
        public bool DeleteCategory(Category category) 
        {
            _context.Remove(category);
            return Save();
        }
        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }
    }
}
