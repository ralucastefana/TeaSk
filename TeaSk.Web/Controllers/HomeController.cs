using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeaSk.Application.Infrastructure;
using TeaSk.Domain.Entities;

namespace TeaSk.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService<Activities> _activitiesService;

        public HomeController(IService<Activities> activitiesService)
        {
            _activitiesService = activitiesService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {


            return View();
        }


        public ActionResult Events()
        {
            ViewBag.Message = "Events you might be interested in.";
            var model = _activitiesService.GetAll();
            return View(model);
        }

        public ActionResult Login()
        {
            
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var model = _activitiesService.GetByID((int)id);
            return View(model);
        }
    }
}