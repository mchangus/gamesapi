namespace GamesApi.Rawg.Services.Models
{
    internal class Game
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Added { get; set; }

        public int? Metacritic { get; set; }

        public double Rating { get; set; }

        public string Released { get; set; }

        public DateTime? Updated { get; set; }

    }
}
