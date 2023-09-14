using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetAllReview();
        Review GetReview(int id);
        Pokemon GetPokemonByReview(int reviewId);
        Reviewer GetReviewerByReview(int reviewId);
        bool ReviewExist(int id);
        bool CreateReview(Review review);
        bool Save();
    }
}
