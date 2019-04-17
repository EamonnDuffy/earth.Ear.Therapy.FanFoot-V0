using System.Collections.Generic;
using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Repositories.FanFootTherapy;
using Earth.Ear.Therapy.FanFoot.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Entities.FanFootTherapy;

namespace Earth.Ear.Therapy.FanFoot.Controllers
{
    public class HomeController : Controller
    {
        private IDatabaseVersionsRepository DatabaseVersionsRepository { get; }

        public HomeController(IDatabaseVersionsRepository databaseVersionsRepository)
        {
            DatabaseVersionsRepository = databaseVersionsRepository;
        }

        public IActionResult Index()
        {
#if true
            var one = DatabaseVersionsRepository.Get(1);

            List<DatabaseVersionEntity> entities = null;

            entities = DatabaseVersionsRepository.GetAll().ToList();

            entities = DatabaseVersionsRepository.GetMany(entity => entity.Major == 0).ToList();
#endif

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
