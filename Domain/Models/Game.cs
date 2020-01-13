namespace GameCRUDApp.Domain.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Genre Genre { get; set; }
    }
}
