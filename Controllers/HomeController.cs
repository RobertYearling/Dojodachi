using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DojoDachi.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace DojoDachi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("Count") == null)
            {
                Dachi Adrien = new Dachi();
                // Adrien.Stats();
                HttpContext.Session.SetInt32("Count", 1);
                HttpContext.Session.SetInt32("Happiness", (int)Adrien.Happiness);
                HttpContext.Session.SetInt32("Fullness", (int)Adrien.Fullness);
                HttpContext.Session.SetInt32("Energy", (int)Adrien.Energy);
                HttpContext.Session.SetInt32("Meals", (int)Adrien.Meals);
                HttpContext.Session.SetString("Standing", "Welcome to DojoDachi!!! Please begin by selecting from the bellow and try not to kill your Dachi... Thanks");
            }
            else
            {
                int? count = HttpContext.Session.GetInt32("Count");
                count++;
                HttpContext.Session.SetInt32("Count", (int)count);
            }
            if(HttpContext.Session.GetInt32("Happiness") <= 0 || HttpContext.Session.GetInt32("Fullness") <= 0)
            {
                HttpContext.Session.SetString("Standing", "Yo Dachi Dead, What did you do?!?!?!");
            }
            else if(HttpContext.Session.GetInt32("Energy") >= 100 || HttpContext.Session.GetInt32("Fullness") >= 100 || HttpContext.Session.GetInt32("Happiness") >= 100)
            {
                HttpContext.Session.SetString("Standing", "YOU WIN!! Could you watch my dog this weekend?");
            }
// els)e if yo Dachi dead what did you do?
//If fullness or happiness ever drop to 0, you lose, and a restart button should be displayed.
// If energy, fullness, and happiness are all raised to over 100, you win! a restart button should be displayed.
            ViewBag.Count = HttpContext.Session.GetInt32("Count"); 
            ViewBag.Happiness = HttpContext.Session.GetInt32("Happiness"); 
            ViewBag.Fullness = HttpContext.Session.GetInt32("Fullness"); 
            ViewBag.Energy = HttpContext.Session.GetInt32("Energy"); 
            ViewBag.Meals = HttpContext.Session.GetInt32("Meals"); 
            ViewBag.Standing = HttpContext.Session.GetString("Standing");
            return View();
        }
        //
        public IActionResult Feed()
        {
            Random rand = new Random();
            int Fullness = (int)HttpContext.Session.GetInt32("Fullness");
            int Meals = (int)HttpContext.Session.GetInt32("Meals");
            int ThisRand = rand.Next(5, 10);
            Fullness += ThisRand;
            HttpContext.Session.SetInt32("Fullness", (int)Fullness);
            Meals -= 1;
            HttpContext.Session.SetInt32("Meals", (int)Meals);
            HttpContext.Session.SetString("Standing", $"You Fed your DojoDachi!! Meals -1, Fullness +{ThisRand}");
            return RedirectToAction("Index");
        }
        //
        public IActionResult Play()
        {
            Random rand = new Random();
            int Happiness = (int)HttpContext.Session.GetInt32("Happiness");
            int Energy = (int)HttpContext.Session.GetInt32("Energy");
            int ThisRand = rand.Next(5, 10);
            Happiness += ThisRand;
            Energy -= 5;
            HttpContext.Session.SetInt32("Happiness", (int)Happiness);
            HttpContext.Session.SetInt32("Energy", (int)Energy);
            HttpContext.Session.SetString("Standing", $"You played your DojoDachi!! Energy -5, Happiness +{ThisRand}");
            return RedirectToAction("Index");
        }
        //
        public IActionResult Work()
        {
            int Meals = (int)HttpContext.Session.GetInt32("Meals");
            int Energy = (int)HttpContext.Session.GetInt32("Energy");
            Random rand = new Random();
            int ThisRand = rand.Next(0, 4);
            HttpContext.Session.SetInt32("Meals", Meals += ThisRand);
            HttpContext.Session.SetInt32("Energy", Energy -= 5);
            HttpContext.Session.SetString("Standing", $"Your Dachi did work Son, GET SOME!!    Energy -5, Meals +{ThisRand}");
            return RedirectToAction("Index");
        }
        public IActionResult Sleep()
        {
            int Fullness = (int)HttpContext.Session.GetInt32("Fullness");
            int Happiness = (int)HttpContext.Session.GetInt32("Happiness");
            int Energy = (int)HttpContext.Session.GetInt32("Energy");
            HttpContext.Session.SetInt32("Happiness", Happiness -= 5);
            HttpContext.Session.SetInt32("Fullness", Fullness -= 5);
            HttpContext.Session.SetInt32("Energy", Energy +=15);
            HttpContext.Session.SetString("Standing", $"Your Dachi is well rested and ready to go, but could use a Meal first...    Energy +15, Fullness -5,");
            return RedirectToAction("Index");
        }
        public IActionResult Restart()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
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
