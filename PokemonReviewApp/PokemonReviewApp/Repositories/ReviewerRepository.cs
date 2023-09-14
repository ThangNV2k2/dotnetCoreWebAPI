using PokemonReviewApp.data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;

        public ReviewerRepository(DataContext context) 
        {
            _context = context;
        }
        public ICollection<Reviewer> GetAllReviewers()
        {
            return _context.Reviewers.ToList();
        }

        public Reviewer GetReviewer(int id)
        {
            return _context.Reviewers.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Review> GetReviewsByReviewers(int reviewerId)
        {
            return _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
        }

        public bool ReviewerExist(int id)
        {
            return _context.Reviews.Any(r => r.Id == id);
        }
        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Add(reviewer);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
