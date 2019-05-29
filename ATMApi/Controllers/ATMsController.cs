using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ATMApi.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ATMApi.Controllers
{
    public class ATMsController : Controller
    {
        
        public string gettoken()
        {
            string token="not Authorize";
            if (Request.Cookies["token"] != null)
            {
                token = Request.Cookies["token"].Value.ToString();
            }
            return token;
        }
        // GET: ATMs

        public async Task<ActionResult> Index()
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:13000/");
            hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", gettoken());
            hc.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await hc.GetAsync("api/atm");
            if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            {
                var atms = response.Content.ReadAsAsync<List<ATM>>().Result;
                return View(atms);
            }
            else
               return RedirectToAction("login","home");
        }

        [HttpGet]
        public ActionResult Create()
        {
            //if (Session["login"] == "ok")
            //{

            return View();
            //}
            //else
            //    return RedirectToAction("index", "login");
        }

        [HttpPost]
        public ActionResult Create(ATM atm)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:13000/");
            hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", gettoken());
            hc.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response= hc.PostAsJsonAsync<ATM>("api/atm", atm).Result;
            if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            {
                System.Threading.Thread.Sleep(700);
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("login","home");

        }

        [HttpGet]
        public ActionResult Edite(int id)
        {

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:13000/");
            hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",gettoken());
            hc.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = hc.GetAsync("api/atm/" + id).Result;
            if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                ATM atm = JsonConvert.DeserializeObject<ATM>(json);
                return View(atm);
            }
            else
                return RedirectToAction("login", "home");

        }
        [HttpPost]
        public ActionResult Edite(ATM atm )
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:13000/");
            hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", gettoken());
            hc.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response= hc.PutAsJsonAsync<ATM>("api/atm/" + atm.id, atm).Result;
            if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            {
                System.Threading.Thread.Sleep(700);
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("login", "home");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:13000/");
            hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", gettoken());
            hc.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = hc.GetAsync("api/atm/" + id).Result;
            if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                ATM atm = JsonConvert.DeserializeObject<ATM>(json);
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_patialAtmDetail", atm);

                }
                return View(atm);
            }
            else
                return RedirectToAction("login", "home");
        }
    }
}