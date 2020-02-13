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

        public Task<bool> AddGame(string gameToAdd)
        {
            throw new NotImplementedException();
        }

        public Task AddGameImage(string image)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteGame(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetAllGames()
        {
            throw new NotImplementedException();
        }

        public Task<Game> GetGame(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Game> GetLastGame()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateGameAsync(string operation, int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateGameImageAsync(string operation, int id)
        {
            throw new NotImplementedException();
        }
    }
}