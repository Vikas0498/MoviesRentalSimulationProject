using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models;
using MoviesRentalSimulationProject.Connector;
using MoviesRentalSimulationProject.Models;
using Newtonsoft.Json;

namespace MoviesRentalSimulationProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(HomeController));
        private IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        Customer obj = new Customer();

        public async Task<IActionResult> Index()
        {
           // HttpContext.Session.SetString("token",null);
                List<Movie> movies = new List<Movie>();
                HttpClient client = obj.MoviesPresnet();
                HttpResponseMessage res = await client.GetAsync("api/MoviesApi");
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    movies = JsonConvert.DeserializeObject<List<Movie>>(result);
                }
                return View(movies);
            
                /*List<MovieRent> movies = new List<MovieRent>();
                HttpClient client = obj.MenuItemDetails();
                HttpResponseMessage res = await client.GetAsync("api/MovieRentingApi");
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    movies = JsonConvert.DeserializeObject<List<MovieRent>>(result);
                }
                return View(movies);*/

        }
        [HttpGet]
        public ActionResult Create(int id)
        {
            
            _log4net.Info("Data Arrived");

            MovieRent obj = new MovieRent();
            obj.MovieId = id;

            return View(obj);
            
        }
        public IActionResult GetToken()
        {
            HttpClient client = obj.AuthenticationInfo();
            var contentType = new MediaTypeWithQualityHeaderValue
        ("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

            Authenticate1 userModel = new Authenticate1();
            userModel.Id = 1;
            userModel.Username = "Vikas";
            userModel.Pass = 1234;

            string stringData = JsonConvert.SerializeObject(userModel);
            var contentData = new StringContent(stringData,
        System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync
        ("/api/AuthApi", contentData).Result;

            string stringJWT = response.Content.
        ReadAsStringAsync().Result;

            JWT jwt = JsonConvert.DeserializeObject
        <JWT>(stringJWT);
            if (jwt.Token == null)
                return Content("Wrong Username or Password");

            HttpContext.Session.SetString("token", jwt.Token);

            /*ViewBag.Message = "User logged in successfully!";

            return View("Index");*/
            //return Content("User Logged in Successfully");
            return RedirectToAction("Create");
            
        }
        [HttpPost]
        public ActionResult Insert(MovieRent rent)
        {
            // TempData["Token"];
           

           
            HttpClient client = obj.RentingInfo();
            var postTask = client.PostAsJsonAsync<MovieRent>($"api/MovieRentingApi", rent);
            postTask.Wait();
            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                _log4net.Info("Data Sent");
                return RedirectToAction("Index");
                
            }
            return Content("Not Working");


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
    public class JWT
    {
        public string Token { get; set; }
    }
}
