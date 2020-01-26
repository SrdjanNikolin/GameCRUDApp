using GameCRUDApp.Domain.Models;
using GameCRUDApp.Domain.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

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
        public async Task<bool> AddGame(string gameToAdd)
        {
            StringContent content = new StringContent(gameToAdd, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"game/addGame", content);
            //string contentHeader = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode == false)
            {
                return false;
            }
            return true;
        }
        public async Task<Game> GetLastGame()
        {
            var response = await _client.GetAsync("game/getLastGame");
            if (response.IsSuccessStatusCode != false)
            {
                string responseStream = await response.Content.ReadAsStringAsync();
                Game lastGame = JsonConvert.DeserializeObject<Game>(responseStream);
                return lastGame;
            }
            return null;
        }
        public async Task AddGameImage(string image)
        {
            var content = new StringContent(image, Encoding.UTF8, "application/json");
            await _client.PostAsync("game/addImage", content);
        }
    }
}