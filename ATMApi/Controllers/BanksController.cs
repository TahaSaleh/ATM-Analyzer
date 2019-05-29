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
    public class BanksController : Controller
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
        // GET: Banks
        public ActionResult Index()
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:13000/");
            hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", gettoken());
            hc.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = hc.GetAsync("api/bank").Result;
            if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            {
                var banks = response.Content.ReadAsAsync<List<Bank>>().Result;
                System.Threading.Thread.Sleep(700);
                return View(banks);
            }
            else
                return RedirectToAction("login","home");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();  
        }
        [HttpPost]
        public ActionResult Create(Bank bank)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:13000/");
            hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", gettoken());
            hc.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response= hc.PostAsJsonAsync<Bank>("api/bank", bank).Result;
            if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("index");
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
            HttpResponseMessage response= hc.DeleteAsync("api/bank/" + id).Result;
            if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            {
                System.Threading.Thread.Sleep(700);
                return RedirectToAction("index");
            }
            else
                return RedirectToAction("login", "home");

        }
        [HttpGet]
        public ActionResult Edite(int id)
        {

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:13000/");
            hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", gettoken());
            hc.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = hc.GetAsync("api/bank/" + id).Result;
            if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                Bank bank = JsonConvert.DeserializeObject<Bank>(json);
                return View(bank);
            }
            else
                return RedirectToAction("login", "home");

        }
        [HttpPost]
        public ActionResult Edite(Bank bank)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:13000/");
            hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", gettoken());
            hc.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response= hc.PutAsJsonAsync<Bank>("api/bank/" + bank.id, bank).Result;
            if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            {
                System.Threading.Thread.Sleep(700);
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("login", "home");


        }
        public ActionResult Details(int id)
        {

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:13000/");
            hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", gettoken());
            hc.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = hc.GetAsync("api/bank/" + id).Result;
            if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                Bank bank = JsonConvert.DeserializeObject<Bank>(json);
                return View(bank);
            }
            else
                return RedirectToAction("login", "home");
        }
    }
}