using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using MoviesRentalSimulationProject.Connector;
using Newtonsoft.Json;

namespace MoviesRentalSimulationProject.Controllers
{
    public class RentalController : Controller
    {
        Customer obj = new Customer();
        // GET: RentalController
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login");

            }
            List<MovieRent> movies = new List<MovieRent>();
                HttpClient client = obj.RentingInfo();
                HttpResponseMessage res = await client.GetAsync("api/MovieRentingApi");
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    movies = JsonConvert.DeserializeObject<List<MovieRent>>(result);
                }
                return View(movies);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Authenticate1 userModel)
        {
            HttpClient client = obj.AuthenticationInfo();
            var contentType = new MediaTypeWithQualityHeaderValue
        ("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
         

            string stringData = JsonConvert.SerializeObject(userModel);
            var contentData = new StringContent(stringData,
        System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync
        ("/api/Authenticate2", contentData).Result;

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
            return RedirectToAction("Index");
         
        }
        // GET: RentalController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RentalController/Create
        public ActionResult Create()
        {
            return View();
        }
       [HttpPost]
     public IActionResult Add(MovieRent rent)
       {
           HttpClient client = obj.RentingInfo();
            var postTask = client.PostAsJsonAsync<MovieRent>($"api/MovieRentingApi", rent);
            postTask.Wait();
            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return Content("Not Working");
      }
        // POST: RentalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RentalController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RentalController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RentalController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RentalController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
