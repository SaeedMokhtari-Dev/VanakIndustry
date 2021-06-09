using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VanakIndustry.Core.Api.Handlers
{
    public interface IApiRequestHandler { }

    public interface IApiRequestHandler<in TRequest> : IApiRequestHandler
    {
        Task<IActionResult> Handle(TRequest request);
    }
}
