using Newtonsoft.Json;

namespace GameCRUDApp.Domain.Models
{
    public class GameImage
    {
        public int? GameImageId { get; set; }
        public string GameImageData { get; set; } = @"C:\Users\Srdjan\Desktop\Web Applications\GameCRUDApp\wwwroot\images\notFound404.png";
        //[ForeignKey("GameImageForeignKey")]
        public int? GameId { get; set; }
        [JsonIgnore]
        public Game Game { get; set; }
    }
}