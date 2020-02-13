using GameCRUDApp.Domain.Models;
using GameCRUDApp.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCRUDApp.Helpers
{
    public static class Helper
    {
        //Maybe process image file async?
        public static string ProcessImageFile(IFormFile imageFile)
        {
            string imageFileBase64;
            byte[] byteArray;
            using (var stream = new BinaryReader(imageFile.OpenReadStream()))
            {
                byteArray = stream.ReadBytes((int)imageFile.OpenReadStream().Length);
            }
            imageFileBase64 = ConvertToBase64(byteArray);
            return imageFileBase64;
        }
        public static string CheckImageFile(IFormFile imageFile)
        {
            string response = null;
            if (imageFile.ContentType.StartsWith("image"))
            {
                if (imageFile.ContentType.EndsWith("jpeg") || imageFile.ContentType.EndsWith("png"))
                {
                    if (imageFile.Length <= 2000000)
                    {
                        return response;
                    }
                    else
                    {
                        response = "File size too large";
                    }
                }
                else
                {
                    response = "File format needs to be jpg or png";
                }
            }
            else
            {
                response = "Selected file is not an image";
            }
            return response;
        }
        public static string ConvertToBase64(byte[] byteArray)
        {
            string imageData = @"data:image/png;base64," + Convert.ToBase64String(byteArray);
            return imageData;
        }
        public static string ConvertToJson(object givenObject)
        {
            string jsonObject = JsonConvert.SerializeObject(givenObject, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return jsonObject;
        }

        public static string GetJsonPatchOperations(object model)
        {
            List<JsonPatchOperationModel> listOfOperations = new List<JsonPatchOperationModel>();
            var properties = model.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.GetValue(model) != null)
                {
                    listOfOperations.Add(new JsonPatchOperationModel()
                    {
                        Op = "replace", Path = $"/{property.Name}", Value = property.GetValue(model)
                    });
                }
            }
            string jsonPatchOperation = JsonConvert.SerializeObject(listOfOperations, Formatting.Indented);
            return jsonPatchOperation;
        }
        public static void Set<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }
        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            tempData.TryGetValue(key, out object o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }
        public static string RemovePropertyInJson(this string json, string property)
        {
            JObject jsonObject = JObject.Parse(json);
            jsonObject.Property(property).Remove();
            return ConvertToJson(jsonObject);
        }
    }
}