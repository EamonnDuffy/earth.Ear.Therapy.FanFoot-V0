﻿using earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy;
using earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Repositories.FanFootTherapy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace earth.Ear.Therapy.FanFoot.Pages.web.Seasons
{
    public class CreateModel : PageModel
    {
        private ISeasonsRepository SeasonsRepository { get; }

        public bool IsGet { get; private set; } = true;

        public List<SeasonEntity> LastSeasons { get; private set; }

        [BindProperty]
        public string Description { get; set; }

        [BindProperty]
        public DateTime BeginDateTimeUtc { get; set; }

        [BindProperty]
        public DateTime EndDateTimeUtc { get; set; }

        public CreateModel(ISeasonsRepository seasonsRepository)
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
            EndDateTimeUtc = beginDateTimeUtc;
        }

        public IActionResult OnPost(string action)
        {
            IsGet = false;

            if (action == "Create")
            {
                // TODO: Check that the new Season does not Exist Already nor is it Active.

                var seasonEntity = new SeasonEntity()
                {
                    Description = Description,
                    BeginDateTimeUtc = BeginDateTimeUtc.Date,
                    EndDateTimeUtc = EndDateTimeUtc.Date.AddDays(1).AddMilliseconds(-1),
                    CreatedDateTimeUtc = DateTime.UtcNow
                };

                SeasonsRepository.Create(seasonEntity);
                SeasonsRepository.SaveChanges();
            }

            return RedirectToPage("/web/Index");
        }
    }
}
