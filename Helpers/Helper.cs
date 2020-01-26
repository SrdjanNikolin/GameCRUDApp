using GameCRUDApp.Domain.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    }
}