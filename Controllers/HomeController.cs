using GameCRUDApp.Domain.Models;
using GameCRUDApp.Domain.Services;
using GameCRUDApp.Domain.ViewModels;
using GameCRUDApp.Helpers;
using GameCRUDApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
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
                    string jsonImage = Helper.ConvertToJson(gameImageToAdd).RemovePropertyInJson("GameImageId");
                    await _gameService.AddGameImage(jsonImage);
                }
            }
            TempData["Success"] = $"{game.Name} has been created!";
            return RedirectToAction("Create");
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var gameToEdit = await _gameService.GetGame(id);
            if (gameToEdit == null)
            {
                return RedirectToAction("Index");
            }
            HomeEditViewModel homeEditViewModel = new HomeEditViewModel()
            {
                Game = gameToEdit,
                PageTitle = $"Edit {gameToEdit.Name}"
            };
            if (TempData["SuccessList"] != null)
            {
                homeEditViewModel.SuccessList = TempData.Get<List<string>>("SuccessList");
            }
            if (TempData["ErrorList"] != null)
            {
                homeEditViewModel.ErrorList = TempData.Get<List<string>>("ErrorList");
            }
            return View(homeEditViewModel);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(HomeEditViewModel model, int id, int? gameImageId)
        {
            if (!ModelState.IsValid)
            {
                //something was wrong with the input
                return RedirectToAction("Edit");
            }
            Messages messages = new Messages();
            if (model.UpdateGameViewModel.Name != null || model.UpdateGameViewModel.Price != null || model.UpdateGameViewModel.Genre != null)
            {
                string jsonPatchOperations = Helper.GetJsonPatchOperations(new Game() {
                    Name = model.UpdateGameViewModel.Name,
                    Price = model.UpdateGameViewModel.Price,
                    Genre = model.UpdateGameViewModel.Genre
                });
                bool updateGameResponse = await _gameService.UpdateGameAsync(jsonPatchOperations, id);
                if (updateGameResponse == false)
                {
                    messages.ErrorList.Add("Could not update game information.");
                }
                else
                {
                    messages.SuccessList.Add($"Game info has been updated.");
                }
            }
            if (model.UpdateGameViewModel.GameImage != null)
            {
                string checkForErrors = Helper.CheckImageFile(model.UpdateGameViewModel.GameImage);
                if (checkForErrors != null)
                {
                    messages.ErrorList.Add(checkForErrors);
                }
                else
                {
                    string gameImageToBase64 = Helper.ProcessImageFile(model.UpdateGameViewModel.GameImage);
                    GameImage gameImage = new GameImage() { GameImageData = gameImageToBase64 };
                    bool updateGameImageResponse;
                    if (gameImageId != null)
                    {
                        string jsonPatchOperation = Helper.GetJsonPatchOperations(gameImage);
                        updateGameImageResponse = await _gameService.UpdateGameImageAsync(jsonPatchOperation, gameImageId);
                    }
                    else
                    {
                        gameImage.GameId = id;
                        string jsonImage = Helper.ConvertToJson(gameImage).RemovePropertyInJson("GameImageId");
                        updateGameImageResponse = await _gameService.AddGameImage(jsonImage);
                    }                         
                    if (updateGameImageResponse == false)
                    {
                        messages.ErrorList.Add("Could not update game image, something went wrong.");
                    }
                    else
                    {
                        messages.SuccessList.Add($"Game image has been updated.");
                    }
                }
            }
            if (messages.ErrorList.Count > 0)
            {
                TempData.Set("ErrorList", messages.ErrorList);
            }
            if (messages.SuccessList.Count > 0)
            {
                TempData.Set("SuccessList", messages.SuccessList);
            }
            return RedirectToAction("Edit");
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