using earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy;
using earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Repositories.FanFootTherapy;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace earth.Ear.Therapy.FanFoot.Pages.web
{
    public class IndexModel : PageModel
    {
        private ISeasonsRepository SeasonsRepository { get; }

        public bool IsGet { get; private set; } = true;

        public List<SeasonEntity> LastSeasons { get; private set; }

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
        }
    }
}
