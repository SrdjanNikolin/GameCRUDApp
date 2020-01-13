using GameCRUDApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameCRUDApp.Domain.ViewModels
{
    public class HomeDetailsViewModel
    {
        public Game Game { get; set; }
        public string PageTitle { get; set; }
    }
}
