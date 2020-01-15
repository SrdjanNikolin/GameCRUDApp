using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameCRUDApp.Domain.Models
{
    public class Game
    {
        public int GameId { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1, 80)]
        public double Price { get; set; }
        [Range(1, 10)]
        public Genre Genre { get; set; }
        public GameImage GameImage { get; set; }
    }
}