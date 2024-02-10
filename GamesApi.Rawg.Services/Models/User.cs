namespace GamesApi.Rawg.Services.Models
{
    internal class User
    {
        public int UserId { get; set; }

        public IEnumerable<Game> Games { get; set; } = default!;
    }
}
