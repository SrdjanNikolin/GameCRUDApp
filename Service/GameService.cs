using GameCRUDApp.Domain.Models;
using GameCRUDApp.Domain.Repositories;
using GameCRUDApp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameCRUDApp.Service
{
    public class GameService : IGameService
    {
        public IGameRepository _gameRepository;
        public GameService(IGameRepository gameRepository) //Dependency injection
        {
            _gameRepository = gameRepository;
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _gameRepository.GetAllGames();
        }
        public Game GetGame(int id)
        {
            return _gameRepository.GetGame(id);
        }
    }
}