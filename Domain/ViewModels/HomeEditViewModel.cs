using GameCRUDApp.Domain.Models;
using System.Collections.Generic;

namespace GameCRUDApp.Domain.ViewModels
{
    public class HomeEditViewModel
    {
        public Game Game { get; set; }
        public UpdateGameViewModel UpdateGameViewModel { get; set; }
        public string PageTitle { get; set; }
        public List<string> ErrorList { get; set; }
        public List<string> SuccessList { get; set; }
    }
}