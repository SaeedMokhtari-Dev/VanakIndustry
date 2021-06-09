using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NLog;
using VanakIndustry.Core.Api.Handlers;
using VanakIndustry.Core.Api.Models;
using VanakIndustry.Core.Constants;
using VanakIndustry.Core.Interfaces;

namespace VanakIndustry.Core.Api.Controllers
{
    [ApiController]
    public abstract class ApiController<TRequest>: ControllerBase, IApiController
    {
        // ReSharper disable once StaticMemberInGenericType
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        protected readonly IApiRequestHandler<TRequest> Handler;
        protected readonly IValidator<TRequest> Validator;

        protected ApiController(IApiRequestHandler<TRequest> handler, IValidator<TRequest> validator)
        {
            Validator = validator;
            Handler = handler;
        }

        [HttpPost]
        public virtual async Task<IActionResult> Handle([FromBody] TRequest request)
        {
            try
            {
                if (request == null) return ApiResponse.Error(ApiMessages.InvalidRequest);

                var result = await Validate(request);

                if (result == null)
                {
                    return await Handler.Handle(request);
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return ApiResponse.Error();
            }
        }

        private async Task<IActionResult> Validate(TRequest request)
        {
            if (Validator != null)
            {
                var result = await Validator.ValidateAsync(request);

                if(!result.IsValid)
                {
                    if (result.Errors.Any(x => x.ErrorMessage == ApiMessages.Forbidden))
                    {
                        return Forbid();
                    }
                    else
                    {
                        return ApiResponse.Error(result.Errors.FirstOrDefault()?.ErrorMessage);
                    }
                }
            }

            return null;
        }
    }
}
