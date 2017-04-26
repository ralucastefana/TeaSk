using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeaSk.Application.Infrastructure;
using TeaSk.Domain.Entities;
using TeaSk.Web.Models.User;

namespace TeaSk.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IService<User> _userService;

        public UserController(IService<User> userService)
        {
            _userService = userService;
        }
        // GET: User
        public ActionResult List()
        {
            var dbUsers = _userService.GetAll();
            var model = dbUsers.Select(user => (UserModel)new UserModel().InjectFrom(user)).ToList();
            return View(model);
        }
    }
}