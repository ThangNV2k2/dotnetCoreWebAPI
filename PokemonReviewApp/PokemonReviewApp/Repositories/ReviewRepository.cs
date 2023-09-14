using PokemonReviewApp.data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;

        public ReviewRepository(DataContext context) 
        {
            _context = context;
        }
        public ICollection<Review> GetAllReview()
        {
            return _context.Reviews.ToList();
        }

        public Pokemon GetPokemonByReview(int reviewId)
        {
            return _context.Reviews.Where(r => r.Id == reviewId).Select(r => r.Pokemon).FirstOrDefault();
        }

        public Review GetReview(int id)
        {
            return _context.Reviews.Where(r => r.Id == id).FirstOrDefault();
        }

        public Reviewer GetReviewerByReview(int reviewId)
        {
            return _context.Reviews.Where(r => r.Id == reviewId).Select(r => r.Reviewer).FirstOrDefault();
        }

        public bool ReviewExist(int id)
        {
            return (_context.Reviews.Any(r => r.Id == id));
        }
        public bool CreateReview(Review review)
        {
            _context.Add(review);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
