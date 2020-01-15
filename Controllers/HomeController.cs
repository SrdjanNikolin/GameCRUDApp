using GameCRUDApp.Domain.Models;
using GameCRUDApp.Domain.Services;
using GameCRUDApp.Domain.ViewModels;
using GameCRUDApp.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameCRUDApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameService _gameService;
        public HomeController(IGameService gameService)
        {
            _gameService = gameService;
        }
        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var games = await _gameService.GetAllGames();
            return View(games);
        }
        //TODO: correct all routes.
        [HttpGet]
        public async Task<ViewResult> Details(int id)
        {
            var game = await _gameService.GetGame(id);
            if (game == null)
            {
                return View("Index");
            }
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Game = game,
                PageTitle = $"{game.Name} Details"
            };
            return View(homeDetailsViewModel);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int GameId)
        {
            bool response = await _gameService.DeleteGame(GameId);
            if (response == false)
            {
                //message that operation failed.
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}