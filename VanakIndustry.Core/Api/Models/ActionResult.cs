using Newtonsoft.Json;
using VanakIndustry.Core.Constants;

namespace VanakIndustry.Core.Api.Models
{
    public class ActionResult
    {
        public bool Success { get; private set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; private set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Data { get; private set; }

        public static ActionResult Error()
        {
            return new ActionResult
            {
                Success = false,
                Message = ApiMessages.GenericError
            };
        }

        public static ActionResult Error(string message)
        {
            return new ActionResult
            {
                Success = false,
                Message = message
            };
        }

        public static ActionResult Ok(object data = null, string message = null)
        {
            return new ActionResult
            {
                Success = true,
                Data = data,
                Message = message
            };
        }
        public static ActionResult Ok(string message)
        {
            return new ActionResult
            {
                Success = true,
                Data = null,
                Message = message
            };
        }
    }
}
