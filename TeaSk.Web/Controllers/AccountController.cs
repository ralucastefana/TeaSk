using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json.Linq;
using Omu.ValueInjecter;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using TeaSk.Application.Infrastructure;
using TeaSk.Domain.Entities;
using TeaSk.Web.Models;

namespace TeaSk.Web.Controllers
{
    public class AccountController : Controller
    {
        private IService<User> _userService { get; set; }

        public AccountController(IService<User> userService)
        {
            _userService = userService;
        }

        // GET: Account\
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = _userService.GetFirst(x => x.Username == username && x.Password == password);
            if (user != null)
            {
                Session["User"] = user;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult ExternalLogin(string code, string state)
        {
            //Get Accedd Token  
            var client = new RestClient("https://www.linkedin.com/oauth/v2/accessToken");
            var request = new RestRequest(Method.POST);
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("code", code);
            request.AddParameter("redirect_uri", "http://localhost:54100/account/externallogin");
            request.AddParameter("client_id", "86solwux7xapvc");
            request.AddParameter("client_secret", "6Pp5PXP6IVNhIIV9");
            IRestResponse response = client.Execute(request);
            var accessToken = response.Content;

            var clientEmail = new RestClient("https://www.linkedin.com/v1/people/~:(emailAddress)");
            var requestEmail = new RestRequest(Method.POST);
            request.AddParameter("ouath2_access_token", accessToken);
            request.AddParameter("format", "json");
            IRestResponse responseEmail = client.Execute(request);
            var email = responseEmail.Content;

            var user = _userService.GetFirst(x => x.Email == email);
            if (user == null)
                return RedirectToAction("Login", "Account");

            Session["User"] = user;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult GithubCallback(string code, string state)
        {

            var request = (HttpWebRequest)WebRequest.Create("https://github.com/login/oauth/access_token?client_id=393bc52e43ee23613eca&client_secret=7070d0523a2da7420409d3bddace0000e0b6fe1a&code=" + code);
            request.Accept = "application/json";
            var response = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
            var accoessToken = (string)((dynamic)JObject.Parse(response)).access_token;

            var requestUser = (HttpWebRequest)WebRequest.Create("https://api.github.com/user/emails?access_token=" + accoessToken);
            requestUser.UserAgent = "Anything";
            var responseUser = new StreamReader(requestUser.GetResponse().GetResponseStream()).ReadToEnd();
            var gitUser = (string)((dynamic)JArray.Parse(responseUser))[0].email;

            var user = _userService.GetFirst(x => x.Email == gitUser);
            if (user == null)
                return RedirectToAction("Index", "Home");

            var requestRepo = (HttpWebRequest)WebRequest.Create("https://api.github.com/user/repos?access_token=" + accoessToken);
            requestRepo.UserAgent = "Anything";
            var responseRepo = new StreamReader(requestRepo.GetResponse().GetResponseStream()).ReadToEnd();
            var repositories = ((dynamic)JArray.Parse(responseRepo));
            var list = new List<string>();
            foreach (var repo in repositories)
            {
                var language = (string)repo.language;
                if (!list.Contains(language) && !string.IsNullOrEmpty(language))
                    list.Add(language);
            }
            var newSkills = list.Where(x => !user.Skills.ToList().Exists(y => y.Name == x));
            foreach (var skill in newSkills)
            {
                user.Skills.Add(new Skills { Name = skill });
            }
            _userService.Update(user);
            Session["User"] = user;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult StackCallback(string code, string state)
        {
            //Get Accedd Token  
            var client = new RestClient("https://stackexchange.com/oauth/access_token");
            var request = new RestRequest(Method.POST);
            request.AddParameter("code", code);
            request.AddParameter("redirect_uri", "http://localhost:54100/account/stackcallback");
            request.AddParameter("client_id", "10067");
            request.AddParameter("client_secret", "u0u4qfR6veEhRphyw3O0QA((");
            IRestResponse response = client.Execute(request);
            var accessToken = response.Content;

            var clientEmail = new RestClient("https://www.linkedin.com/v1/people/~:(emailAddress)");
            var requestEmail = new RestRequest(Method.POST);
            request.AddParameter("ouath2_access_token", accessToken);
            request.AddParameter("format", "json");
            IRestResponse responseEmail = client.Execute(request);
            var email = responseEmail.Content;

            var user = _userService.GetFirst(x => x.Email == email);
            if (user == null)
                return RedirectToAction("Login", "Account");

            Session["User"] = user;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string name, string surname, DateTime birthDate, string phone, string email, string username, string password)
        {
            var user = _userService.GetFirst(x => x.Email == email && x.Username == username);
            if (user == null)
            {
                _userService.Add(new User
                {
                    Name = name,
                    Surname = surname,
                    BirthDate = birthDate,
                    Email = email,
                    Phone = phone,
                    Username = username,
                    Password = password
                });
                Session["User"] = user;
                return RedirectToAction("Index", "Home");
            }
            else
                ViewBag.Message = "Account already in use!";
            return View();
        }
    }
}