using earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy;
using earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Repositories.FanFootTherapy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace earth.Ear.Therapy.FanFoot.Pages.web.Seasons
{
    public class DeleteModel : PageModel
    {
        private ISeasonsRepository SeasonsRepository { get; }

        public bool IsGet { get; private set; } = true;

        [HiddenInput]
        public int SeasonId { get; set; }

        public SeasonEntity Season { get; private set; }

        public DeleteModel(ISeasonsRepository seasonsRepository)
        {
            SeasonsRepository = seasonsRepository;
        }

        public void OnGet(int seasonId)
        {
            SeasonId = seasonId;

            Season = SeasonsRepository.Get(seasonId);
        }

        public IActionResult OnPost(string confirm, int seasonId)
        {
            IsGet = false;

            if (confirm == "Yes")
            {
                var seasonEntity = SeasonsRepository.Get(seasonId);

                if (seasonEntity != null)
                {
                    SeasonsRepository.Delete(seasonEntity);
                    SeasonsRepository.SaveChanges();
                }
            }

            return RedirectToPage("/web/Index");
        }
    }
}