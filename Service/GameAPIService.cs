using GameCRUDApp.Domain.Models;
using GameCRUDApp.Domain.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameCRUDApp.Service
{
    public class GameAPIService : IGameService
    {
        private readonly HttpClient _client;
        public GameAPIService(HttpClient httpclient)
        {
            _client = httpclient;
        }    

        public async Task<IEnumerable<Game>> GetAllGames()
        {
            var respone = await _client.GetAsync($"game/all");
            if (respone.IsSuccessStatusCode == false)
            {
                return null;
            }
            string responseStream = await respone.Content.ReadAsStringAsync();
            IEnumerable<Game> allGames = JsonConvert.DeserializeObject<List<Game>>(responseStream);
            return allGames;
        }
        public async Task<Game> GetGame(int id)
        {
            var response = await _client.GetAsync($"game/{id}");
            if (response.IsSuccessStatusCode == false)
            {
                return null;
            }
            
            string responseStream = await response.Content.ReadAsStringAsync();
            Game game = JsonConvert.DeserializeObject<Game>(responseStream);
            return game;
        }
        public async Task<bool> DeleteGame(int id)
        {
            var response = await _client.DeleteAsync($"game/delete/{id}");
            if (response.IsSuccessStatusCode == false)
            {
                return false;
            }
            return true;
        }
    }
}