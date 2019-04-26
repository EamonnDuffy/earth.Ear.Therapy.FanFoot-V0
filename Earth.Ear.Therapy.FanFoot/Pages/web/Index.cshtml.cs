using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy;
using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Repositories.FanFootTherapy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Earth.Ear.Therapy.FanFoot.Pages.web
{
    public class IndexModel : PageModel
    {
        private ISeasonsRepository SeasonsRepository { get; }

        public bool IsGet { get; private set; } = true;

        public List<SeasonEntity> LastSeasons { get; private set; }

        [BindProperty]
        public DateTime BeginDateTimeUtc { get; set; }

        [BindProperty]
        public DateTime EndDateTimeUtc { get; set; }

        public IndexModel(ISeasonsRepository seasonsRepository)
        {
            SeasonsRepository = seasonsRepository;
        }

        public void OnGet()
        {
            var lastSeasons = SeasonsRepository
                    .GetAll()
                    .OrderByDescending(entity => entity.SeasonId)
                    .ToList();

            var beginDateTimeUtc = DateTime.UtcNow;

            LastSeasons = lastSeasons;
            BeginDateTimeUtc = beginDateTimeUtc;
            EndDateTimeUtc = beginDateTimeUtc.AddMonths(9);
        }
    }
}
