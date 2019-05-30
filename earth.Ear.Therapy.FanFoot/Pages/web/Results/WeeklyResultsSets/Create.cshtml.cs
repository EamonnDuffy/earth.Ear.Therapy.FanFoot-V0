using System;
using System.Reflection;
using Earth.Ear.Therapy.FanFoot.External;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Earth.Ear.Therapy.FanFoot.Pages.web.Results.WeeklyResultsSets
{
    public class CreateModel : PageModel
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private ITeamsWeeklyResults TeamsWeeklyResults { get; }

        public CreateModel(ITeamsWeeklyResults teamsWeeklyResults)
        {
            TeamsWeeklyResults = teamsWeeklyResults;
        }

        private IActionResult CreateResultSet()
        {
            string resultMessage = null;

            try
            {
                var fantasyFootballBdo = PremierLeague.GetFantasyFootball().Result;

                TeamsWeeklyResults.Create(fantasyFootballBdo);
            }
            catch (Exception exception)
            {
                resultMessage = exception.Message;

                Log.Error("Exception.", exception);
            }

            return new JsonResult(new { resultMessage = resultMessage, redirectUrl = @Url.Page("/web/Index") });
        }

        public IActionResult OnGet(bool ajaxCallBack = false)
        {
            IActionResult actionResult = null;

            if (ajaxCallBack)
            {
                actionResult = CreateResultSet();
            }
            else
            {
                actionResult = Page();
            }

            return actionResult;
        }
    }
}
