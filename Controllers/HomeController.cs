using GameCRUDApp.Domain.Models;
using GameCRUDApp.Domain.Services;
using GameCRUDApp.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public ViewResult Index()
        {
            var games =_gameService.GetAllGames();
            return View(games);
        }
        [HttpGet]
        public ViewResult Details(int? id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Game = _gameService.GetGame(id ?? 1),
                PageTitle = "Game Management Details"
            };
            return View(homeDetailsViewModel);
        }
    }
}