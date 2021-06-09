using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NLog;
using VanakIndustry.Core.Api.Exceptions;
using VanakIndustry.Core.Api.Models;
using ActionResult = VanakIndustry.Core.Api.Models.ActionResult;

namespace VanakIndustry.Core.Api.Handlers
{
    public abstract class ApiRequestHandler<T>: IApiRequestHandler<T>
    {
        // ReSharper disable once StaticMemberInGenericType
        protected static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        static ApiRequestHandler()
        {
            LogManager.AddHiddenAssembly(Assembly.GetExecutingAssembly());
        }

        public async Task<IActionResult> Handle(T request)
        {
            try
            {
                var result = await Execute(request);

                return ApiResponse.Result(result);
            }
            catch (ApiException ex)
            {
                Logger.Error(ex);
                return ApiResponse.Error(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return ApiResponse.Error();
            }
        }

        protected abstract Task<ActionResult> Execute(T request);
    }
}
