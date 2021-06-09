using Microsoft.AspNetCore.Mvc;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Core.Api.Models
{
    public class ApiResponse
    {
        public static JsonResult Error()
        {
            return new JsonResult(ActionResult.Error());
        }

        public static JsonResult Error(string message)
        {
            return new JsonResult(ActionResult.Error(message));
        }

        public static JsonResult NotFound()
        {
            return new JsonResult(ActionResult.Error(ApiMessages.GenericError));
        }

        public static JsonResult InvalidRequest()
        {
            return new JsonResult(ActionResult.Error(ApiMessages.GenericError));
        }

        public static JsonResult Ok(object data = null, string message = null)
        {
            return new JsonResult(ActionResult.Ok(data, message));
        }

        public static JsonResult Result(ActionResult actionResult)
        {
            return new JsonResult(actionResult);
        }
    }
}
