using Domain.Entities.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace Application.Utils
{
    public static class ResponseUtil
    {
        public static IActionResult CustomOkNoSetting<T>(T? data, int status) where T : class
        {
            int totalData = 1;

            // Check if the data parameter is enumerable and get its count
            if (data is IEnumerable<T> enumerableData)
            {
                totalData = enumerableData.Count();
            }

            return new OkObjectResult(new BaseAPIResponse<T>
            {
                Data = data,
                StatusCode = status,
                TotalData = totalData,
                Message = (status == 200) ? "Success" : "Fail"
            });
        }
        public static IActionResult CustomOk<T>(T data, int status) where T : class
        {
            var settingsDe = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All
            };
            var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);

            return new OkObjectResult(new BaseAPIResponse<T>
            {
                Data = JsonConvert.DeserializeObject<T>(jsonData, settingsDe),
                TotalData = 1,
                StatusCode = status,
                Message = (status == 200) ? "Success" : "Fail"
            });
        }
        public static IActionResult CustomOkList<T, TCollection>(TCollection data, int status) 
            where T : class
            where TCollection : IEnumerable<T>
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            var settingsDe = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All
            };
            var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented, settings);

            int totalData = data.Count();

            return new OkObjectResult(new BaseAPIResponse<TCollection>
            {
                Data = JsonConvert.DeserializeObject<TCollection>(jsonData, settingsDe),
                TotalData = totalData,
                StatusCode = status,
                Message = (status == 200) ? "Success" : "Fail"
            });
        }
    }
}
