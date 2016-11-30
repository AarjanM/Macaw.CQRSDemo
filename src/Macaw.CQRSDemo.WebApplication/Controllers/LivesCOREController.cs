using Macaw.CQRSDemo.WebApplication.Application;
using Microsoft.AspNetCore.Mvc;

namespace Macaw.CQRSDemo.WebApplication.Controllers
{
    public class LiveScoreController : Controller
    {
        private readonly LiveScoreService _liveScoreService;

        public LiveScoreController(LiveScoreService liveScoreService)
        {
            _liveScoreService = liveScoreService;
        }

        public ActionResult Index()
        {
            return View(_liveScoreService.GetLiveViewModel());
        }

        public PartialViewResult Update()
        {
            return PartialView("_live", _liveScoreService.GetLiveViewModel());
        }
    }
}