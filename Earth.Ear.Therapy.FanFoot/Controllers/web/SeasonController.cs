using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Repositories.FanFootTherapy;
using Earth.Ear.Therapy.FanFoot.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Earth.Ear.Therapy.FanFoot.Controllers.web
{
    public class SeasonController : Controller
    {
        private ISeasonsRepository SeasonsRepository { get; }

        public SeasonController(ISeasonsRepository seasonsRepository)
        {
            SeasonsRepository = seasonsRepository;
        }

        [HttpGet("web/Season")]
        public IActionResult Create()
        {
            var lastSeasons = SeasonsRepository.GetAll().OrderByDescending(entity => entity.SeasonId).ToList();

            var beginDateTimeUtc = DateTime.UtcNow;

            var viewModel = new SeasonViewModel()
            {
                LastSeasons = lastSeasons,
                BeginDateTimeUtc = beginDateTimeUtc,
                EndDateTimeUtc = beginDateTimeUtc.AddMonths(9)
            };

            return View(viewModel);
        }
    }
}
