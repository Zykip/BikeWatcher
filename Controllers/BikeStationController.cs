using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BikeWatcher.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using BikeWatcher.ViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BikeWatcher.Controllers
{
    public class BikeStationController : Controller
    {

        private readonly Data.DataContext _context;

        public BikeStationController(Data.DataContext context)
        {
            _context = context;
        }


        private static readonly HttpClient client = new HttpClient();

        // GET: /<controller>/
        public async Task<IActionResult> Index(String city = "lyon")
        {
            var bikestation = new LIstBikeStationViewModel()
            {
                LIstBike = new List<BikeStation>(),
            };

            if (city == "bordeaux")
            {
                var stations = new List<BikeStation>();
                stations = await ProcessRepositoriesBdx();
                bikestation.LIstBike = stations.OrderBy(x => x.name).ToList();

            }
            else
            {
                var stations = new List<BikeStation>();
                stations = await ProcessRepositories();
                bikestation.LIstBike = stations.OrderBy(x => x.name).ToList();
            }
            return View(bikestation);

        }

        public async Task<IActionResult> carte()
        {
            var bikestation = new LIstBikeStationViewModel();
            bikestation.LIstBike = await ProcessRepositories();
            return View(bikestation);
        }

        public async Task<IActionResult> Favoris()
        {
            return View(await _context.Favoris.ToListAsync());
        }

        public async Task<IActionResult> AddToFav(int id)
        {

            var favBikeStation = new Favoris();
            favBikeStation.IDStation = id;
            _context.Favoris.Add(favBikeStation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        public IActionResult Connexion()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Connexion(ConnxionViewModel connexion)
        {
            if (_context.User.Any(p => p.Mail == connexion.Mail) && _context.User.Any(p => p.Password == connexion.Password))
            {
                var user = _context.User.FirstOrDefault(p => p.Mail == connexion.Mail);
            }
            else
            {
                ModelState.AddModelError("Bad", "Connexion Invalide");
                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Inscription()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Inscription(InscriptionViewModel newUser)
        {
            if (ModelState.IsValid && !_context.User.Any(p => p.Mail == newUser.Mail))
            {
                var user = new User();
                user.Mail = newUser.Mail;
                user.Password = newUser.Password;
                _context.User.Add(user);
            }
            else
            {
                if (_context.User.Any(p => p.Mail == newUser.Mail))
                {
                    ModelState.AddModelError("Bad", "Le mail est déja utilisé");
                }
                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult SignVelo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignVelo([Bind("IDVelo", "Commentaire", "Email")] Models.SignVelo signVelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(signVelo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }
            return View(signVelo);
        }





        private static async Task<List<BikeStation>> ProcessRepositories()
        {

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask = client.GetStreamAsync("https://download.data.grandlyon.com/ws/rdata/jcd_jcdecaux.jcdvelov/all.json");
            var bikeStations = await JsonSerializer.DeserializeAsync<RootObject>(await streamTask);
            return bikeStations.values;

        }

        private static async Task<List<BikeStation>> ProcessRepositoriesBdx()
        {

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask = client.GetStreamAsync("https://api.alexandredubois.com/vcub-backend/vcub.php");
            var bikeStationsbdx = await JsonSerializer.DeserializeAsync<List<BikeStationBdx>>(await streamTask);
            var listBikestation = new List<BikeStation>();
            foreach (var bikeStation in bikeStationsbdx)
            {
                var bikeStations = new BikeStation(bikeStation);
                listBikestation.Add(bikeStations);

            }
            return listBikestation;

        }




    }

}



