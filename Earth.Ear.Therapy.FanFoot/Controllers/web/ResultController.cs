using Microsoft.AspNetCore.Mvc;

namespace Earth.Ear.Therapy.FanFoot.Controllers.web
{
    public class ResultController : Controller
    {
        [HttpGet("web/Result/CreateWeeklyResultsSet")]
        public IActionResult CreateWeeklyResultsSet()
        {
            return View();
        }

        [HttpGet("web/Result/CreateWeeklyResultsDocument")]
        public IActionResult CreateWeeklyResultsDocument()
        {
            return View();
        }
    }
}
