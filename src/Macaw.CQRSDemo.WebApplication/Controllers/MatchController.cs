using System;
using Macaw.CQRSDemo.WebApplication.Application;
using Microsoft.AspNetCore.Mvc;

namespace Macaw.CQRSDemo.WebApplication.Controllers
{
    public class MatchController : Controller
    {
        private readonly MatchService _matchService;

        public MatchController(MatchService matchService)
        {
            _matchService = matchService;
        }

        public ActionResult Index(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
                return RedirectToAction("index", "home");

            var model = _matchService.GetMatchDetails(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Action(string id, RequestedAction action)
        {
            _matchService.ProcessAction(id, action);
            return RedirectToAction("index", new { id });
        }
    }
}