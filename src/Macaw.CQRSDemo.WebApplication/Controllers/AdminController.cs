using Macaw.CQRSDemo.WebApplication.Application;
using Macaw.CQRSDemo.WebApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Macaw.CQRSDemo.WebApplication.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        public ActionResult Index()
        {
            var model = new ViewModelBase();
            return View(model);
        }

        [HttpPost]
        public ActionResult Action(AdminAction action)
        {
            _adminService.ProcessAction(action);
            return RedirectToAction("index", "home");
        }
    }
}