using GameCRUDApp.Domain.Models;
using GameCRUDApp.Domain.Services;
using GameCRUDApp.Domain.ViewModels;
using GameCRUDApp.Helpers;
using GameCRUDApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
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
        public async Task<ActionResult> Details(int id)
        {
            var game = await _gameService.GetGame(id);
            if (game == null)
            {
                return RedirectToAction("Index");
            }
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Game = game,
                PageTitle = $"{game.Name} Details"
            };
            return View(homeDetailsViewModel);
        }
        [HttpGet]
        public async Task<ViewResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(GameViewModel game)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (game.GameImage!= null)
            {
                string checkForErrors = Helper.CheckImageFile(game.GameImage);
                if (checkForErrors != null)
                {
                    //need client side validation
                    ModelState.AddModelError("GameImage", checkForErrors);
                    return View();
                }
            }
            string jsonGame = Helper.ConvertToJson(game);
            bool response = await _gameService.AddGame(jsonGame);
            if (response == false)
            {
                ModelState.AddModelError("Error", "Could not create game, something went wrong.");
                return View();
            }          
            if (game.GameImage != null)
            {
               var addedGame = await _gameService.GetLastGame();
                if (addedGame != null)
                {
                    string gameImageToBase64 = Helper.ProcessImageFile(game.GameImage);
                    GameImage gameImageToAdd = new GameImage()
                    {
                        GameImageData = gameImageToBase64,
                        GameId = addedGame.GameId
                    };
                    string jsonImage = Helper.ConvertToJson(gameImageToAdd);
                    await _gameService.AddGameImage(jsonImage);
                }
            }
            TempData["Success"] = $"{game.Name} has been created!";
            return RedirectToAction("Create");
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