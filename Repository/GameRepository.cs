using GameCRUDApp.Domain.Models;
using GameCRUDApp.Domain.Repositories;
using System.Collections.Generic;

namespace GameCRUDApp.Repository
{
    public class GameRepository : IGameRepository
    {
        public List<Game> _games;
        public GameRepository()
        {
            _games = new List<Game>()
            {
                new Game { GameId = 1, Name = "Assassins Creed", Price = 59.99, Genre = Genre.Adventure},
                new Game { GameId = 2, Name = "Call of Duty Black Ops 4", Price = 49.99, Genre = Genre.Shooter},
                new Game { GameId = 3, Name = "The Witcher 3", Price = 39.99, Genre = Genre.RPG},
                new Game { GameId = 4, Name = "Forza 4", Price = 29.99, Genre = Genre.Racing},
                new Game { GameId = 5, Name = "CS Global Offensive", Price = 19.99, Genre = Genre.Shooter}
            };
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _games;
        }

        public Game GetGame(int id)
        {
            return _games.Find(Game => Game.GameId == id);
        }
    }
}