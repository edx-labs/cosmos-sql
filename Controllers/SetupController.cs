using RealEstateCatalog.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RealEstateCatalog.Models;
using Microsoft.Extensions.Options;

namespace RealEstateCatalog.Controllers
{
    public class SetupController : Controller
    {
        private IOptions<CosmosSettings> _cosmosOptions { get; }

        private ISetupService _setupService;

        public SetupController(IOptions<CosmosSettings> cosmosOptions, ISetupService setupService)
        {
            _cosmosOptions = cosmosOptions;
            _setupService = setupService;
        }

        [HttpGet]
        [Route("~/setup", Name = "Setup")]
        public ActionResult Index()
        {
            CosmosSettings model = new CosmosSettings
            {
                DatabaseId = "contoso",
                ContainerId = "realestate"
            };
            return View(model);
        }

        [HttpPost]
        [Route("~/setup", Name = "PostSetup")]
        public ActionResult Index(CosmosSettings model)
        {
            if (ModelState.IsValid)
            {
                _cosmosOptions.Value.EndpointUrl = model.EndpointUrl;
                _cosmosOptions.Value.AuthorizationKey = model.AuthorizationKey;
                _cosmosOptions.Value.DatabaseId = model.DatabaseId;
                _cosmosOptions.Value.ContainerId = model.ContainerId;
                return RedirectToRoute("Processing");
            }
            else
            {
                return View(model);
            }
        }
        
        [HttpGet]
        [Route("~/setup/processing", Name = "Processing")]
        public async Task<ActionResult> Processing()
        { 
            await _setupService.GenerateDataAsync();
            return RedirectToRoute("Home");
        }
    }
}