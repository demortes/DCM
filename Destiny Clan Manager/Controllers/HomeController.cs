using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Destiny_Clan_Manager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Login()
        {
            if(Session["OAUTHToken"] != null)
            {
                return RedirectToAction("Index", "Manager");
            } else
            {
                string baseURL = ConfigurationManager.AppSettings["BaseUrl"];
                string scope = ConfigurationManager.AppSettings["scope"];
                string access_type = ConfigurationManager.AppSettings["access_type"];
                string redirect_url = new UriBuilder(Request.Url.AbsoluteUri) { Path = Url.Content("~/Manager/Authorize"), Query = null }.Uri.OriginalString;
                string response_type = ConfigurationManager.AppSettings["response_type"];
                string client_id = ConfigurationManager.AppSettings["client_id"];

                string oauthUrl = baseURL + "?response_type=" + response_type + "&client_id=" + client_id;
                Response.Redirect(oauthUrl);
                //TODO: Redirect to Bungie's OAUTH Page
            }
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact webmaster@demortes.com for any concerns.";

            return View();
        }
    }
}