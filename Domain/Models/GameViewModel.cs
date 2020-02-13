using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace GameCRUDApp.Domain.Models
{
    public class GameViewModel
    {
        [Required(ErrorMessage ="Name is a required field.")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Price is a required field.")]
        [Range(4.99, 80.00, ErrorMessage ="The price must be between 4.99 and 80.00")]
        [RegularExpression(@"^\d{1,2}(\.\d{2})$", ErrorMessage ="The Price field must contain 2 decimals.")]
        public double? Price { get; set; }
        [Required]
        public string Genre { get; set; }
        [JsonIgnore]
        public IFormFile GameImage { get; set; }
        [JsonIgnore]
        public string Error { get; set; }
    }
}