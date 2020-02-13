namespace GameCRUDApp.Domain.Models
{
    public class Game
    {
        public int? GameId { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public string Genre { get; set; }
        public GameImage GameImage { get; set; }
    }
}