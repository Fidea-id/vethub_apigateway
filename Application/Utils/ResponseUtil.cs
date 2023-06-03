using Domain.Entities.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utils
{
    public static class ResponseUtil
    {
        public static IActionResult CustomOkNoSetting<T>(T? data, int status) where T : class
        {
            return new OkObjectResult(new BaseAPIResponse<T>
            {
                Data = data,
                StatusCode = status,
                Message = (status == 200) ? "Success" : "Fail"
            });
        }
        public static IActionResult CustomOk<T>(T? data, int status) where T : class
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

            return new OkObjectResult(new BaseAPIResponse<T>
            {
                Data = JsonConvert.DeserializeObject<T>(jsonData, settingsDe),
                StatusCode = status,
                Message = (status == 200) ? "Success" : "Fail"
            });
        }
    }
}
