using GameCRUDApp.Domain.Models;
using System.Collections.Generic;

namespace GameCRUDApp.Domain.Services
{
    public interface IGameService
    {
        IEnumerable<Game> GetAllGames();
        Game GetGame(int id);
    }
}
