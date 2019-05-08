using Earth.Ear.Therapy.FanFoot.External;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Reflection;
using System.Threading;

namespace Earth.Ear.Therapy.FanFoot.Pages.web.WeeklyResultsDocuments
{
    public class CreateModel : PageModel
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private ITeamsWeeklyDocuments TeamsWeeklyDocuments { get; }

        public CreateModel(ITeamsWeeklyDocuments teamsWeeklyDocuments)
        {
            TeamsWeeklyDocuments = teamsWeeklyDocuments;
        }

        private IActionResult CreateResultDocument()
        {
            bool resultSuccess = false;
            string resultMessage = null;
            string resultFile = null;

            try
            {
                resultFile = TeamsWeeklyDocuments.Create();

                if (!string.IsNullOrWhiteSpace(resultFile))
                    resultSuccess = true;

                Thread.Sleep(5000);
            }
            catch (Exception exception)
            {
                resultMessage = exception.Message;

                Log.Error("Exception.", exception);
            }

            return new JsonResult(new { resultSuccess = resultSuccess, resultMessage = resultMessage, resultFile = resultFile, redirectUrl = @Url.Page("/web/Index") });
        }

        public IActionResult OnGet(bool ajaxCallBack = false)
        {
            IActionResult actionResult = null;

            if (ajaxCallBack)
            {
                actionResult = CreateResultDocument();
            }
            else
            {
                actionResult = Page();
            }

            return actionResult;
        }
    }
}
