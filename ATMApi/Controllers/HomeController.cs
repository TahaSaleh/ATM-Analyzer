using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using ATMApi.Models;

namespace ATMApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult login(string url)
        {
            ViewBag.url = url;
            return View();
        }
        [HttpPost]
        public ActionResult Login(string UserName,string password,string url)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:13000/");
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpContent content =new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", UserName),
                    new KeyValuePair<string, string>("password", password)
                });
            HttpResponseMessage response = hc.PostAsync("Token", content).Result;
            BearerToken bearerToken = response.Content.ReadAsAsync<BearerToken>().Result;
            HttpCookie token = new HttpCookie("token",bearerToken.AccessToken);
            token.Expires.AddDays(10);
            HttpContext.Response.SetCookie(token);
            return RedirectToLocal(url);
        }
        private ActionResult RedirectToLocal(string url)
        {
            if (Url.IsLocalUrl(url))
            {
                return Redirect(url);
            }
            return RedirectToAction("Index", "atms");
        }

        public ActionResult logout()
        {
            var temp = new HttpCookie("token");
            temp.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(temp);
            return  RedirectToAction("login", "home");
        }
    }
}
