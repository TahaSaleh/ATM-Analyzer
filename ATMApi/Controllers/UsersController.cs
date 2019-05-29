using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using ATMApi.Models;
using Newtonsoft.Json;

namespace ATMApi.Controllers
{
    public class UsersController : Controller
    {
        public string gettoken()
        {
            string token = "not Authorize";
            if (Request.Cookies["token"] != null)
            {
                token = Request.Cookies["token"].Value.ToString();
            }
            return token;
        }
        // GET: Users

        public ActionResult Index()
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:13000/");
            hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", gettoken());
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = hc.GetAsync("api/user").Result;
            if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            {
                var users = response.Content.ReadAsAsync<List<User>>().Result;
                return View(users);
            }
            else
                return RedirectToAction("login", "home");

        }
        [HttpGet]
        public ActionResult Create()
        {
            List<Bank> bank = GetBanks();
            ViewBag.banks = new SelectList(bank,"Id","Name");
            return View();
        }
        public List<Bank> GetBanks()
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:13000/");
            hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", gettoken());
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = hc.GetAsync("api/bank").Result;
            List<Bank> bank = response.Content.ReadAsAsync<List<Bank>>().Result;
            return bank;
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("http://localhost:13000/");
                hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",gettoken());
                hc.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response= hc.PostAsJsonAsync<User>("api/user", user).Result;
                if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
                {
                    if (Request.IsAjaxRequest())
                    {
                        return PartialView("_createuser");

                    }
                    else
                        return RedirectToAction("index");
                }
                else
                {
                    ViewBag.banks =new SelectList( GetBanks(),"Id","Name");
                    return View(user);
                }
            }
                else
                return RedirectToAction("login", "home");

        }

        public ActionResult Delete(int id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:13000/");
            hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", gettoken());
            hc.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response= hc.DeleteAsync("api/user/" + id).Result;
            if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("index");
            }
            else
                return RedirectToAction("login", "home");
        }
        public ActionResult Details(int id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:13000/");
            hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", gettoken());
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = hc.GetAsync("api/user/" + id).Result;
            string json = response.Content.ReadAsStringAsync().Result;
            User user = JsonConvert.DeserializeObject<User>(json);
            ViewBag.banks = new SelectList(GetBanks(), "Id", "Name",user.bank_id);
            return View(user);
        }
        [HttpGet]
        public ActionResult Edite(int id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:13000/");
            hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",gettoken());
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = hc.GetAsync("api/user/" + id).Result;
            if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                User user = JsonConvert.DeserializeObject<User>(json);
                ViewBag.banks = new SelectList(GetBanks(), "Id", "Name",user.bank_id);
                return View(user);
            }
            else
                return RedirectToAction("login", "home");

        }
        [HttpPost]
        public ActionResult Edite(User user)
        {
            //if (ModelState.IsValid)
            //{
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:13000/");
            hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", gettoken());
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response= hc.PutAsJsonAsync<User>("api/user/" + user.id, user).Result;
            if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("index");
            }
            else
                return RedirectToAction("login", "home");
            //}
            //else return View(user);
        }
        public ActionResult getUser(int id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:13000/");
            hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", gettoken());
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = hc.GetAsync("api/user/" + id).Result;
            string json = response.Content.ReadAsStringAsync().Result;
            User user = JsonConvert.DeserializeObject<User>(json);
            return View(user);
        }
    }
}