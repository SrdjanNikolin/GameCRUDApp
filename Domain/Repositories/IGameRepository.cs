using GameCRUDApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameCRUDApp.Domain.Repositories
{
    public interface IGameRepository
    {
        IEnumerable<Game> GetAllGames();
        Game GetGame(int id);
    }
}
