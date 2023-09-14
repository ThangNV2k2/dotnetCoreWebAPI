using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetAllReviewers();
        Reviewer GetReviewer(int id);
        ICollection<Review> GetReviewsByReviewers(int reviewerId);
        bool ReviewerExist(int id);
        bool CreateReviewer(Reviewer reviewer);
        bool Save();
    }
}
