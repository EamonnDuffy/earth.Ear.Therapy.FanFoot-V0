using System;
using Earth.Ear.Therapy.FanFoot.External;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading;

namespace Earth.Ear.Therapy.FanFoot.Pages.web.WeeklyResultsSets
{
    public class CreateModel : PageModel
    {
        private ITeamsWeeklyResults TeamsWeeklyResults { get; }

        public CreateModel(ITeamsWeeklyResults teamsWeeklyResults)
        {
            TeamsWeeklyResults = teamsWeeklyResults;
        }

        private IActionResult CreateResultsSet()
        {
            try
            {
                var fantasyFootballBdo = PremierLeague.GetFantasyFootball().Result;

                TeamsWeeklyResults.Create(1, fantasyFootballBdo);
            }
            catch (Exception exception)
            {
            }

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
