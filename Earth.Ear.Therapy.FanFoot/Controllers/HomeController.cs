using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
'using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Databases;
using Earth.Ear.Therapy.FanFoot.DataAccess.EntityFramework.Repositories.FanFootTherapy;
using Microsoft.AspNetCore.Mvc;
using Earth.Ear.Therapy.FanFoot.Models;

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
