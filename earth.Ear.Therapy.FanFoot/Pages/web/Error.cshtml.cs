using System.Diagnostics;
using System.Reflection;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace earth.Ear.Therapy.FanFoot.Pages.web
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public void OnGet()
        {
            Log.Info($"Error: Activity.Current?.Id = {Activity.Current?.Id} : HttpContext.TraceIdentifier = {HttpContext.TraceIdentifier}");

            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }

        public void OnPost()
        {
            Log.Info($"Error: Activity.Current?.Id = {Activity.Current?.Id} : HttpContext.TraceIdentifier = {HttpContext.TraceIdentifier}");

            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
