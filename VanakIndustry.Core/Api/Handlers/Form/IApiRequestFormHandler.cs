using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VanakIndustry.Core.Api.Handlers.Form
{
    public interface IApiRequestFormHandler { }

    public interface IApiRequestFormHandler<in TRequest> : IApiRequestHandler
    {
        Task<IActionResult> Handle(TRequest request, List<IFormFile> files);
    }
}
