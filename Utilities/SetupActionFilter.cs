using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using RealEstateCatalog.Models;

namespace RealEstateCatalog.Utilities
{
    public class SetupActionFilter : ActionFilterAttribute
    {
        private CosmosSettings CosmosSettings { get; }
        
        public SetupActionFilter(IOptions<CosmosSettings> cosmosSettings)
        {
            CosmosSettings = cosmosSettings.Value;   
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (!CosmosSettings.IsPopulated)
            {
                context.Result = new RedirectToRouteResult("Setup", new {}, false);
            }
        }
    }
}