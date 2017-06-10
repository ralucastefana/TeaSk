using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using TeaSk.Application.Infrastructure;
using TeaSk.Domain.Entities;

namespace TeaSk.Web.Controllers
{
    public class JsonController : Controller
    {
        private readonly IService<Activities> _activitiesService;
        private readonly IService<User> _userService;
        private readonly IService<Skills> _skillsService;
        public JsonController(IService<Activities> activitiesService, IService<User> userService, IService<Skills> skillsService)
        {
            _activitiesService = activitiesService;
            _userService = userService;
            _skillsService = skillsService;
        }

        public string Users()
        {
            return JsonConvert.SerializeObject(_userService.GetAll(), Formatting.Indented,
                                new JsonSerializerSettings
                                {
                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                });
        }

        public string Events()
        {
            return JsonConvert.SerializeObject(_activitiesService.GetAll(), Formatting.Indented,
                                new JsonSerializerSettings
                                {
                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                });
        }

        public string Skills()
        {
            return JsonConvert.SerializeObject(_skillsService.GetAll(), Formatting.Indented,
                                new JsonSerializerSettings
                                {
                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                });
        }


    }
}