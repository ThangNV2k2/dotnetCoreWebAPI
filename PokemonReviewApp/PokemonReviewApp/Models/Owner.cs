namespace PokemonReviewApp.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public String Gym { get; set; }
        public Country Country { get; set; }
        public ICollection<PokemonOwner> PokemonOwner { get; set; }
    }
}
