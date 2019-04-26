using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading;

namespace Earth.Ear.Therapy.FanFoot.Pages.web.WeeklyResultsSets
{
    public class CreateModel : PageModel
    {
        private IActionResult CreateResultsSet()
        {
            Thread.Sleep(5000);

            return new JsonResult(new { redirectUrl = "https://www.microsoft.com/" });
        }

        public IActionResult OnGet(bool ajaxCallBack = false)
        {
            IActionResult actionResult = null;

            if (ajaxCallBack)
            {
                actionResult = CreateResultsSet();
            }
            else
            {
                actionResult = Page();
            }

            return actionResult;
        }
    }
}
