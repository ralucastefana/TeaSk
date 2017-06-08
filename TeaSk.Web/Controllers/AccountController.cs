using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json.Linq;
using Omu.ValueInjecter;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TeaSk.Application.Infrastructure;
using TeaSk.Domain.Entities;
using TeaSk.Web.Models;

namespace TeaSk.Web.Controllers
{
    public class AccountController : Controller
    {
        private IService<User> _userService { get; set; }

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
                Session["User"]=user;
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
            var content = response.Content;
            //Get Profile Details
            client = new RestClient("https://api.linkedin.com/v1/people/~:(skills)?oauth2_access_token=" + ((dynamic)JObject.Parse(response.Content)).access_token + "&format=json");
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            content = response.Content;
            return View();
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string name, string surname, DateTime birthDate, string phone, string email, string username, string password)
        {
            var user = _userService.GetFirst(x => x.Email == email && x.Username == username);
            if(user == null)
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