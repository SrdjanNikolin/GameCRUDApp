using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GameCRUDApp.Domain.ViewModels
{
    //Add pattern with regex
    public class UpdateGameViewModel
    {
        public string Name { get; set; }
        [Range(4.99, 80.00, ErrorMessage = "The price must be between 4.99 and 80.00")]
        [RegularExpression(@"^\d{1,2}(\.\d{2})$", ErrorMessage = "The Price field must contain 2 decimals.")]
        public double? Price { get; set; }
        public string Genre { get; set; }
        [JsonIgnore]
        public IFormFile GameImage { get; set; }
        [JsonIgnore]
        [TempData]
        public List<string> Errors { get; set; }
    }
}