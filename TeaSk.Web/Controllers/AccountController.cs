using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Omu.ValueInjecter;
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
        private ApplicationSignInManager _signInManager;

        public AccountController(IService<User> userService)
        {
            _userService = userService;
            //SignInManager = signInManager;  , ApplicationSignInManager signInManager
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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
                Session["User"]=user;
                return RedirectToAction("Index", "Home");
            }
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