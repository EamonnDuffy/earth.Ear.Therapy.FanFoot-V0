using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Earth.Ear.Therapy.FanFoot.Pages.web.WeeklyResultsSets
{
    public class CreateModel : PageModel
    {
        private IActionResult CreateResultsSet()
        {
            return new JsonResult(new { });
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
