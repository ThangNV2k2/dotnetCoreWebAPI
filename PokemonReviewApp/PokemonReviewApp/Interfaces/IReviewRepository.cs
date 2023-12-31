﻿using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetAllReview();
        Review GetReview(int id);
        Pokemon GetPokemonByReview(int reviewId);
        Reviewer GetReviewerByReview(int reviewId);
        ICollection<Review> GetReviewsByPokemon(int pokeId);
        bool ReviewExist(int id);
        bool CreateReview(Review review);
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);
        bool DeleteReviews(List<Review> reviews);
        bool Save();
    }
}
