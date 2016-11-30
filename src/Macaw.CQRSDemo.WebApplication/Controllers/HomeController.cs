using Macaw.CQRSDemo.WebApplication.Application;
using Microsoft.AspNetCore.Mvc;

namespace Macaw.CQRSDemo.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeService _service;

        public HomeController(HomeService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var viewModel = _service.GetSchedulesMatches();
            if (viewModel == null)
            {
                return NotFound();
            }
            
            return View(viewModel);
        }
    }
}