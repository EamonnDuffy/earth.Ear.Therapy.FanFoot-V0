﻿using earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy;
using earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Repositories.FanFootTherapy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace earth.Ear.Therapy.FanFoot.Pages.web.Seasons
{
    public class UpdateModel : PageModel
    {
        private ISeasonsRepository SeasonsRepository { get; }

        public bool IsGet { get; private set; } = true;

        [HiddenInput]
        public int SeasonId { get; set; }

        public SeasonEntity Season { get; private set; }

        [BindProperty]
        public string Description { get; set; }

        [BindProperty]
        public DateTime BeginDateTimeUtc { get; set; }

        [BindProperty]
        public DateTime EndDateTimeUtc { get; set; }

        public UpdateModel(ISeasonsRepository seasonsRepository)
        {
            SeasonsRepository = seasonsRepository;
        }

        public void OnGet(int seasonId)
        {
            SeasonId = seasonId;

            var seasonEntity = SeasonsRepository.Get(seasonId);

            Season = seasonEntity;

            if (seasonEntity != null)
            {
                Description = seasonEntity.Description;
                BeginDateTimeUtc = seasonEntity.BeginDateTimeUtc;
                EndDateTimeUtc = seasonEntity.EndDateTimeUtc;
            }
        }

        public IActionResult OnPost(string action, int seasonId)
        {
            IsGet = false;

            // TODO: Check that the new Season Exists Already and is Active.

            if (action == "Update")
            {
                var seasonEntity = SeasonsRepository.Get(seasonId);

                if (seasonEntity != null)
                {
                    seasonEntity.Description = Description;
                    seasonEntity.EndDateTimeUtc = EndDateTimeUtc.Date.AddDays(1).AddMilliseconds(-1);
                    seasonEntity.UpdatedDateTimeUtc = DateTime.UtcNow;

                    SeasonsRepository.Update(seasonEntity);
                    SeasonsRepository.SaveChanges();
                }
            }

            return RedirectToPage("/web/Index");
        }
    }
}
