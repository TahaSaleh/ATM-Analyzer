using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ATMApi.Models;

namespace ATMApi.Controllers
{
    public class TrunsactionsController : Controller
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
        // GET: Trunsactions
        public async Task< ActionResult> Index()
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:13000/");
            hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", gettoken());
            hc.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await hc.GetAsync("api/trunsaction");
            if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            {
                var t = response.Content.ReadAsAsync<List<Trunsaction>>().Result;
                return View(t);
            }
            else
                return RedirectToAction("login", "home");
        }
    }
}