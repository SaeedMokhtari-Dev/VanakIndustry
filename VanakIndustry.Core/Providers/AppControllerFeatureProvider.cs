using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace VanakIndustry.Core.Providers
{
    public class AppControllerFeatureProvider : ControllerFeatureProvider
    {
        protected override bool IsController(TypeInfo typeInfo)
        {
            var isController = base.IsController(typeInfo) || (!typeInfo.IsAbstract && !typeInfo.IsInterface && typeof(ControllerBase).IsAssignableFrom(typeInfo));

            return isController;
        }
    }
}
