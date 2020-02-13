
namespace GameCRUDApp.Domain.Models
{
    public class JsonPatchOperationModel
    {
        public string Op { get; set; }
        public string Path { get; set; }
        public object Value { get; set; }
    }
}