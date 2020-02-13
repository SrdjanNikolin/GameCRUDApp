﻿using GameCRUDApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameCRUDApp.Domain.Services
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetAllGames();
        Task<Game> GetGame(int id);
        Task<Game> GetLastGame();
        Task<bool> DeleteGame(int id);
        Task<bool> AddGame(string gameToAdd);
        Task<bool> AddGameImage(string image);
        Task<bool> UpdateGameAsync(string operation, int id);
        Task<bool> UpdateGameImageAsync(string operation, int? id);
    }
}