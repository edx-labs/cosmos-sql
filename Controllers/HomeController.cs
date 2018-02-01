using Microsoft.AspNetCore.Mvc;
using RealEstateCatalog.Models;
using RealEstateCatalog.Services;
using RealEstateCatalog.Utilities;
using RealEstateCatalog.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateCatalog.Controllers
{
    [ServiceFilter(typeof(SetupActionFilter))]
    public class HomeController : Controller
    {
        private IHomesService _homesService;

        public HomeController(IHomesService homesService)
        {
            _homesService = homesService;
        }

        [HttpGet]
        [Route("~/", Name = "Home")]
        public async Task<ActionResult> Index()
        {
            IList<Home> model = await _homesService.GetHomesAsync();
            return View(model);
        }

        [HttpGet]
        [Route("~/home/{id}", Name = "GetHome")]
        public async Task<ActionResult> Get(string id)
        {
            Home model = await _homesService.GetHomeAsync(id);
            return View(model);
        }

        [HttpGet]
        [Route("~/realtor/{id}", Name = "SearchHomes")]
        public async Task<ActionResult> Search(string id)
        {
            SearchViewModel model = new SearchViewModel { Realtor = id };
            model.Homes = await _homesService.GetHomesForRealtorAsync(id);
            return View(model);
        }

        private bool CheckValidConfiguration()
        {
            return true;
        }
    }
}