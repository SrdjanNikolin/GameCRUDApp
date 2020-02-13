using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GameCRUDApp.Domain.Models
{
    public class Messages
    {
        public List<string> ErrorList { get; set; }
        public List<string> SuccessList { get; set; }
        public Messages()
        {
            ErrorList = new List<string>();
            SuccessList = new List<string>();
        }
    }
}